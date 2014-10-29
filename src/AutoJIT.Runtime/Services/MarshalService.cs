using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AutoJIT.Infrastructure;
using AutoJITRuntime.Exceptions;
using AutoJITRuntime.Variants;
using IndexOutOfRangeException = AutoJITRuntime.Exceptions.IndexOutOfRangeException;

namespace AutoJITRuntime.Services
{
    public class MarshalService
    {
        private readonly Dictionary<string, Type> _delegateStore = new Dictionary<string, Type>();

        private readonly ModuleBuilder _dynamicMod;

        private readonly Dictionary<string, UnmanagedType> _marshalAttributeMapping = new Dictionary<string, UnmanagedType> {
            {
                "STR", UnmanagedType.LPStr
            }, {
                "WSTR", UnmanagedType.LPWStr
            }
        };
        private readonly Dictionary<string, Type> _structStore = new Dictionary<string, Type>();
        private readonly Dictionary<string, Type> _typeMapping = new Dictionary<string, Type> {
            {
                "NONE", typeof(void)
            }, {
                "BYTE", typeof(byte)
            }, {
                "BOOLEAN", typeof(byte)
            }, {
                "CHAR", typeof(char)
            }, {
                "WCHAR", typeof(char)
            }, {
                "SHORT", typeof(Int16)
            }, {
                "USHORT", typeof(UInt16)
            }, {
                "WORD", typeof(UInt16)
            }, {
                "INT", typeof(Int32)
            }, {
                "LONG", typeof(Int32)
            }, {
                "BOOL", typeof(Int32)
            }, {
                "UINT", typeof(UInt32)
            }, {
                "ULONG", typeof(UInt32)
            }, {
                "DWORD", typeof(UInt32)
            }, {
                "INT64", typeof(Int64)
            }, {
                "UINT64", typeof(UInt64)
            }, {
                "PTR", typeof(IntPtr)
            }, {
                "HWND", typeof(IntPtr)
            }, {
                "HANDLE", typeof(IntPtr)
            }, {
                "FLOAT", typeof(Single)
            }, {
                "DOUBLE", typeof(double)
            }, {
                "INT_PTR", typeof(IntPtr)
            }, {
                "LONG_PTR", typeof(IntPtr)
            }, {
                "LRESULT", typeof(IntPtr)
            }, {
                "LPARAM", typeof(IntPtr)
            }, {
                "UINT_PTR", typeof(UIntPtr)
            }, {
                "ULONG_PTR", typeof(UIntPtr)
            }, {
                "DWORD_PTR", typeof(UIntPtr)
            }, {
                "WPARAM", typeof(UIntPtr)
            }, {
                "WSTR", typeof(StringBuilder)
            }, {
                "STR", typeof(StringBuilder)
            }
        };

        public MarshalService() {
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly( new AssemblyName( "an" ), AssemblyBuilderAccess.Run );
            _dynamicMod = assemblyBuilder.DefineDynamicModule( "MainModule" );
        }

        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern IntPtr LoadLibrary( string dllToLoad );

        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern IntPtr GetProcAddress( IntPtr hModule, string procedureName );

        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern bool FreeLibrary( IntPtr hModule );

        [DllImport( "user32.dll" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool IsWindow( IntPtr hWnd );

        public Variant DllCall( Variant dll, string returnType, string function, Variant[] paramtypen ) {
            Variant handle;
            if( dll.IsPtr ) {
                handle = dll.GetIntPtr();
            }
            else {
                handle = DllOpen( dll );
                if( !handle.IsPtr ) {
                    throw new UnableToUseTheDllFileException( 1, null, string.Empty );
                }
            }

            IntPtr procAddress = GetProcAddress( handle, function );

            Variant toReturn = DllCallAddressInternal( returnType, procAddress, paramtypen );

            if( dll.IsPtr ) {
                return toReturn;
            }

            DllClose( handle );

            return toReturn;
        }

        public Variant DllCallAddress( Variant returntype, Variant address, Variant[] paramtypen ) {
            if( !address.IsPtr ) {
                throw new AddressParameterIsNotAPointerException( 1, null, string.Empty );
            }
            IntPtr ptr = address.GetIntPtr();
            string returnType = returntype.GetString();

            return DllCallAddressInternal( returnType, ptr, paramtypen );
        }

        private Variant DllCallAddressInternal( string returnType, IntPtr ptr, Variant[] paramtypen ) {
            if( ptr == IntPtr.Zero ) {
                throw new ProcAddressZeroException( 3, null, string.Empty );
            }

            List<MarshalInfo> parameterMarshalInfo = GetParameterInfo( paramtypen );

            Type callingConvention = typeof(CallConvStdcall);

            if( returnType.Contains( ":" ) ) {
                string[] split = returnType.Split( ':' );

                string customCallingConvention = split[1];
                returnType = split[0];

                callingConvention = GetCallingConvention( customCallingConvention );
            }

            MarshalInfo returnMarshalInfo = GetReturnTypeInfo( returnType );

            Delegate @delegate = GetFunctionDelegate( returnMarshalInfo, parameterMarshalInfo, callingConvention, ptr );

            object[] args = parameterMarshalInfo.Select( x => x.Parameter ).ToArray();

            object result = @delegate.DynamicInvoke( args );

            Variant[] toReturn = MapReturnValues( args, result );
            return toReturn;
        }

        public Variant DllOpen( Variant dll ) {
            try {
                IntPtr library = LoadLibrary( dll.GetString() );
                if( library == IntPtr.Zero ) {
                    int error = Marshal.GetLastWin32Error();
                }
                return library;
            }
            catch(Exception) {
                return -1;
            }
        }

        private static Variant[] MapReturnValues( object[] args, object result ) {
            var toReturn = new Variant[args.Length+1];
            toReturn[0] = Variant.Create( result );
            Array.Copy( args.Select( Variant.Create ).ToArray(), 0, toReturn, 1, args.Length );
            return toReturn;
        }

        private Delegate GetFunctionDelegate(
        MarshalInfo returnMarshalInfo,
        List<MarshalInfo> parameterMarshalInfo,
        Type callingConvention,
        IntPtr procAddress ) {
            Type delegateType = CreateDelegate( returnMarshalInfo, parameterMarshalInfo, callingConvention );

            Delegate @delegate;
            try {
                @delegate = Marshal.GetDelegateForFunctionPointer( procAddress, delegateType );
            }
            catch(Exception ex) {
                throw new BadNumberOfParameterException( 4, null, string.Empty );
            }
            return @delegate;
        }

        private MarshalInfo GetReturnTypeInfo( string returnType ) {
            MarshalInfo returnMarshalInfo;
            try {
                returnMarshalInfo = GetMarshalInfo( returnType, null );
            }
            catch(UnknowTypeNameException) {
                throw new BadReturnTypeException( 2, null, string.Empty );
            }
            return returnMarshalInfo;
        }

        private List<MarshalInfo> GetParameterInfo( Variant[] paramtypen ) {
            var parameterMarshalInfo = new List<MarshalInfo>();
            for( int i = 0; i < paramtypen.Length; i += 2 ) {
                Variant typePart = paramtypen[i];
                Variant value = paramtypen[i+1];

                MarshalInfo marshalInfo;
                try {
                    marshalInfo = GetMarshalInfo( typePart, value );
                }
                catch(UnknowTypeNameException) {
                    throw new BadParameterException( 5, null, string.Empty );
                }

                parameterMarshalInfo.Add( marshalInfo );
            }
            return parameterMarshalInfo;
        }

        private Type GetCallingConvention( string customCallingConvention ) {
            switch(customCallingConvention.ToUpper()) {
                case "CDECL":
                    return typeof(CallConvCdecl);
                case "STDCALL":
                    return typeof(CallConvStdcall);
                case "FASTCALL":
                    return typeof(CallConvFastcall);
                case "THISCALL":
                    return typeof(CallConvThiscall);
                case "WINAPI":
                    return typeof(CallConvStdcall);
                default:
                    throw new UnknowCallConvException( customCallingConvention );
            }
        }

        private Type CreateDelegate( MarshalInfo returntype, List<MarshalInfo> paramtypes, Type callingConvention ) {
            string cacheKey = String.Format(
                                            "Delegate_{0}{1}{2}",
                                            returntype.Type,
                                            String.Join( String.Empty, paramtypes.Select( x => x.Type ) ),
                                            callingConvention );

            if( _delegateStore.ContainsKey( cacheKey ) ) {
                return _delegateStore[cacheKey];
            }

            TypeBuilder tb = _dynamicMod.DefineType(
                                                    String.Format( "_{0}", Guid.NewGuid().ToString( "N" ) ),
                                                    TypeAttributes.Public|TypeAttributes.Sealed,
                                                    typeof(MulticastDelegate) );

            tb.DefineConstructor(
                                 MethodAttributes.RTSpecialName|MethodAttributes.SpecialName|MethodAttributes.Public|MethodAttributes.HideBySig,
                                 CallingConventions.Standard,
                                 new[] {
                                     typeof(object),
                                     typeof(IntPtr)
                                 } ).SetImplementationFlags( MethodImplAttributes.Runtime );

            MethodBuilder inv = tb.DefineMethod(
                                                "Invoke",
                                                MethodAttributes.Public|MethodAttributes.Virtual|MethodAttributes.NewSlot|MethodAttributes.HideBySig,
                                                CallingConventions.Standard,
                                                returntype.Type,
                                                null,
                                                new[] {
                                                    callingConvention
                                                },
                                                paramtypes.Select( x => x.Type ).ToArray(),
                                                null,
                                                null );

            for( int index = 0; index < paramtypes.Count; index++ ) {
                MarshalInfo paramtype = paramtypes[index];

                ParameterAttributes parameterAttributes = paramtype.IsRef
                ? ParameterAttributes.Out
                : ParameterAttributes.In;

                if( paramtype.Type == typeof(StringBuilder) ) {
                    parameterAttributes |= ParameterAttributes.Out;
                }

                if( typeof(IRuntimeStruct).IsAssignableFrom( paramtype.Type.GetElementType() ) ) {
                    parameterAttributes |= ParameterAttributes.In;
                }

                ParameterBuilder parameterBuilder = inv.DefineParameter( index+1, parameterAttributes, null );

                if( paramtype.MarshalAttribute.HasValue ) {
                    ConstructorInfo constructorInfo = typeof(MarshalAsAttribute).GetConstructor(
                                                                                                new[] {
                                                                                                    typeof(UnmanagedType)
                                                                                                } );

                    var customAttributeBuilder = new CustomAttributeBuilder(
                    constructorInfo,
                    new object[] {
                        paramtype.MarshalAttribute
                    } );

                    parameterBuilder.SetCustomAttribute( customAttributeBuilder );
                }
            }

            inv.SetImplementationFlags( MethodImplAttributes.Runtime );

            Type t = tb.CreateType();

            _delegateStore.Add( cacheKey, t );
            return t;
        }

        public MarshalInfo GetMarshalInfo( string typePart, Variant value ) {
            bool isRef = typePart.EndsWith( "*" );
            if( isRef ) {
                typePart = typePart.TrimEnd( '*' );
            }

            Type managedType = typePart.Equals( "struct", StringComparison.InvariantCultureIgnoreCase )
            ? value.GetValue().GetType()
            : GetManagedType( typePart );

            UnmanagedType? marshalAttribute = GetMarshalAttribute( typePart );

            object changeType = null;
            if( value != null ) {
                changeType = ConvertAutoitTypeToMarshalType( value, managedType );
            }

            var marshalInfo = new MarshalInfo( changeType, managedType, marshalAttribute, isRef );
            return marshalInfo;
        }

        private Type GetManagedType( string typeName ) {
            string upperTypeName = typeName.ToUpper();
            if( _typeMapping.ContainsKey( upperTypeName ) ) {
                return _typeMapping[upperTypeName];
            }

            throw new UnknowTypeNameException( typeName );
        }

        private object ConvertAutoitTypeToMarshalType( Variant variant, Type targetType ) {
            object changeType;

            if( variant.GetRealType() == targetType ) {
                changeType = variant.GetValue();
            }
            else if( targetType == typeof(IntPtr) ) {
                changeType = new IntPtr( variant.GetInt() );
            }
            else if( targetType == typeof(UIntPtr) ) {
                changeType = new UIntPtr( (uint)variant.GetInt() );
            }
            else if( variant.IsInt32
                     && targetType == typeof(uint) ) {
                changeType = unchecked( (uint)variant.GetInt() );
            }
            else if( targetType == typeof(StringBuilder) ) {
                string s = variant.GetString();
                changeType = new StringBuilder( s, 0, s.Length, UInt16.MaxValue );
            }
            else {
                changeType = Convert.ChangeType( variant.GetValue(), targetType );
            }
            return changeType;
        }

        public UnmanagedType? GetMarshalAttribute( string typeName ) {
            string upperTypeName = typeName.ToUpper();
            if( _marshalAttributeMapping.ContainsKey( upperTypeName ) ) {
                return _marshalAttributeMapping[upperTypeName];
            }
            return null;
        }

        public Type CreateRuntimeStruct( string @struct ) {
            string cacheKey = String.Format( "Struct_{0}", @struct );

            if( _structStore.ContainsKey( cacheKey ) ) {
                return _structStore[cacheKey];
            }

            IEnumerable<StructTypeInfo> typeInfos = GetTypeInfo( @struct );

            Type res = CreateStruct( typeInfos );

            _structStore.Add( cacheKey, res );

            return res;
        }

        private Type CreateStruct( IEnumerable<StructTypeInfo> typeInfos ) {
            ConstructorInfo constructorInfo = typeof(StructLayoutAttribute).GetConstructor(
                                                                                           new[] {
                                                                                               typeof(LayoutKind)
                                                                                           } );
            var customAttributeBuilder = new CustomAttributeBuilder(
            constructorInfo,
            new object[] {
                LayoutKind.Sequential
            } );

            TypeBuilder tb = _dynamicMod.DefineType(
                                                    "_"+Guid.NewGuid().ToString( "N" ),
                                                    TypeAttributes.Public,
                                                    typeof(object),
                                                    new[] {
                                                        typeof(IRuntimeStruct)
                                                    } );
            tb.SetCustomAttribute( customAttributeBuilder );

            ConstructorBuilder constructorBuilder =
            tb.DefineConstructor(
                                 MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.RTSpecialName,
                                 CallingConventions.Standard,
                                 Type.EmptyTypes );

            ILGenerator ilGenerator = constructorBuilder.GetILGenerator();
            ilGenerator.Emit( OpCodes.Ldarg_0 );
            ConstructorInfo superConstructor = typeof(Object).GetConstructor( Type.EmptyTypes );
            ilGenerator.Emit( OpCodes.Call, superConstructor );
            ilGenerator.Emit( OpCodes.Nop );
            ilGenerator.Emit( OpCodes.Nop );

            foreach(StructTypeInfo typeInfo in typeInfos) {
                FieldBuilder fieldBuilder = tb.DefineField( typeInfo.VariableName, typeInfo.ManagedType, FieldAttributes.Public );

                if( typeInfo.ArraySize > 0 ) {
                    ilGenerator.Emit( OpCodes.Ldarg_0 );
                    ilGenerator.Emit( OpCodes.Ldc_I4, typeInfo.ArraySize );
                    ilGenerator.Emit( OpCodes.Newarr, typeInfo.ManagedType.GetElementType() );
                    ilGenerator.Emit( OpCodes.Stfld, fieldBuilder );
                }

                IEnumerable<CustomAttributeBuilder> attributesToApply = GetCustomAttributes( typeInfo );

                foreach(CustomAttributeBuilder builder in attributesToApply) {
                    fieldBuilder.SetCustomAttribute( builder );
                }
            }

            ilGenerator.Emit( OpCodes.Ret );

            Type t = tb.CreateType();
            return t;
        }

        private static IEnumerable<CustomAttributeBuilder> GetCustomAttributes( StructTypeInfo typeInfo ) {
            var attributesToApply = new List<CustomAttributeBuilder>();

            if( typeInfo.MarshalAs.HasValue ) {
                ConstructorInfo customAttributeConstructorInfoMarshalAs = typeof(MarshalAsAttribute).GetConstructor(
                                                                                                                    new[] {
                                                                                                                        typeof(UnmanagedType)
                                                                                                                    } );
                var customAttributeBuilderMarshalAs = new CustomAttributeBuilder(
                customAttributeConstructorInfoMarshalAs,
                new object[] {
                    typeInfo.MarshalAs.Value
                } );

                attributesToApply.Add( customAttributeBuilderMarshalAs );
            }

            if( typeInfo.ArraySize > 0 ) {
                ConstructorInfo customAttributeConstructorMarshalAsArray = typeof(MarshalAsAttribute).GetConstructor(
                                                                                                                     new[] {
                                                                                                                         typeof(UnmanagedType)
                                                                                                                     } );
                FieldInfo propertyInfoSizeConst = typeof(MarshalAsAttribute).GetFields().Single( x => x.Name.Equals( "SizeConst" ) );

                var customAttributeBuilderMarshalAsArray = new CustomAttributeBuilder(
                customAttributeConstructorMarshalAsArray,
                new object[] {
                    UnmanagedType.ByValArray
                },
                new[] {
                    propertyInfoSizeConst
                },
                new object[] {
                    typeInfo.ArraySize
                } );

                attributesToApply.Add( customAttributeBuilderMarshalAsArray );
            }
            return attributesToApply;
        }

        private IEnumerable<StructTypeInfo> GetTypeInfo( string @struct ) {
            return GetTypeInfo( @struct.Split( ';' ) );
        }

        private IEnumerable<StructTypeInfo> GetTypeInfo( string[] fragments ) {
            bool isSingleStruct = fragments.First().Equals( "STRUCT", StringComparison.InvariantCultureIgnoreCase )
                                  && fragments.Last().Equals( "ENDSTRUCT", StringComparison.InvariantCultureIgnoreCase )
                                  && fragments.Count( x => x.Equals( "STRUCT", StringComparison.InvariantCultureIgnoreCase ) ) == 1
                                  && fragments.Count( x => x.Equals( "ENDSTRUCT", StringComparison.InvariantCultureIgnoreCase ) ) == 1;

            if( isSingleStruct ) {
                fragments = fragments.Skip( 1 ).Take( fragments.Length-2 ).ToArray();
            }

            var toReturn = new List<StructTypeInfo>();

            for( int index = 0; index < fragments.Length; index++ ) {
                string fragment = fragments[index];

                string[] nametypeFragments = fragment.Split( ' ' );
                if( nametypeFragments.Length == 1 ) {
                    string typeFragmanet = nametypeFragments[0];
                    string[] typeArraySizeFragments = typeFragmanet.Split(
                                                                          new[] {
                                                                              "[",
                                                                              "]"
                                                                          },
                                                                          StringSplitOptions.RemoveEmptyEntries );
                    string typePart = typeArraySizeFragments[0];

                    UnmanagedType? marshalAttribute = GetMarshalAttribute( typePart );
                    int arraySize = 0;
                    if( typeArraySizeFragments.Length == 2 ) {
                        arraySize = Int32.Parse( typeArraySizeFragments[1] );
                    }

                    Type managedType;
                    if( typePart.Equals( "STRUCT", StringComparison.InvariantCultureIgnoreCase ) ) {
                        int count = 0;
                        var structPart = new List<string>();
                        do {
                            bool isEndStruct = fragments[index].Equals( "ENDSTRUCT", StringComparison.InvariantCultureIgnoreCase );

                            if( isEndStruct ) {
                                count--;
                            }
                            else {
                                bool isStruct = fragments[index].Equals( "STRUCT", StringComparison.InvariantCultureIgnoreCase );
                                if( isStruct ) {
                                    count++;
                                }
                                else {
                                    structPart.Add( fragments[index] );
                                }
                            }
                            index++;
                        } while( count != 0 );

                        IEnumerable<StructTypeInfo> structTypeInfos = GetTypeInfo( structPart.ToArray() );

                        Type innerStructType = CreateStruct( structTypeInfos );
                        managedType = innerStructType;
                    }
                    else {
                        managedType = GetManagedType( typePart );

                        if( arraySize > 0 ) {
                            managedType = managedType.MakeArrayType();
                        }
                    }

                    toReturn.Add( new StructTypeInfo( "_"+Guid.NewGuid().ToString( "N" ), managedType, marshalAttribute, arraySize ) );
                    continue;
                }

                if( nametypeFragments.Length == 2 ) {
                    string typeFragment = nametypeFragments[0];
                    string nameArraySizeFragment = nametypeFragments[1];

                    string[] nameArraySizeFragments = nameArraySizeFragment.Split(
                                                                                  new[] {
                                                                                      "[",
                                                                                      "]"
                                                                                  },
                                                                                  StringSplitOptions.RemoveEmptyEntries );
                    Type managedType = GetManagedType( typeFragment );
                    UnmanagedType? marshalAttribute = GetMarshalAttribute( typeFragment );

                    int arraySize = 0;
                    if( nameArraySizeFragments.Length == 2 ) {
                        arraySize = Int32.Parse( nameArraySizeFragments[1] );
                    }
                    if( arraySize > 0 ) {
                        managedType = managedType.MakeArrayType();
                    }

                    string name = nameArraySizeFragments[0];

                    toReturn.Add( new StructTypeInfo( name, managedType, marshalAttribute, arraySize ) );
                    continue;
                }
                throw new InvalidOperationException();
            }
            return toReturn;
        }

        public Variant DllClose( Variant dllhandle ) {
            FreeLibrary( dllhandle );
            return 0;
        }

        public Variant DllStructCreate( Variant structString, Variant pointer ) {
            if( !structString.IsString ) {
                throw new VariablePassedToDllStructCreateWasNotAStringException( 1, null, string.Empty );
            }

            Type runtimeStruct;
            try {
                runtimeStruct = CreateRuntimeStruct( structString.GetString() );
            }
            catch(UnknowTypeNameException) {
                throw new UnknowTypeException( 2, null, string.Empty );
            }
            var instance = (IRuntimeStruct)runtimeStruct.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );

            var @struct = (StructVariant)Variant.Create( instance );

            if( pointer != null ) {
                @struct.InitUnmanaged( pointer.GetIntPtr() );
            }

            return @struct;
        }

        public Variant DllStructSetData( StructVariant runtimeStruct, Variant elementVariant, Variant value, Variant index ) {
            if( runtimeStruct == null ) {
                throw new StructNotAValidStructReturnedByDllStructCreateException( 1, null, string.Empty );
            }

            if( index <= 0 ) {
                throw new IndexSmallerEqualNullException( 5, 0, string.Empty );
            }

            if( elementVariant.IsInt32 ) {
                object val = value.GetValue();
                if( val is IEnumerable ) {
                    object element = runtimeStruct.GetElement( elementVariant.GetInt()-1 );
                    if( element == null ) {
                        throw new ElementOutOfRangeException( 2, 0, string.Empty );
                    }

                    if( element is Array ) {
                        int i = index.GetInt()-1;
                        var array = ( (Array)element );

                        foreach(object o in (IEnumerable)val) {
                            Type elementType = array.GetType().GetElementType();
                            try {
                                array.SetValue( Convert.ChangeType( o, elementType ), i );
                            }
                            catch(Exception) {
                                throw new IndexOutOfRangeException( 3, 0, string.Empty );
                            }
                            i++;
                        }
                        runtimeStruct.SetElement( elementVariant.GetInt()-1, element );
                        return Variant.Create( runtimeStruct.GetElement( elementVariant.GetInt()-1 ) );
                    }
                }
                else {
                    runtimeStruct.SetElement( elementVariant.GetInt()-1, value.GetValue() );
                    return Variant.Create( runtimeStruct.GetElement( elementVariant.GetInt()-1 ) );
                }
            }
            else {
                object val = value.GetValue();
                if( val is IEnumerable ) {
                    object element = runtimeStruct.GetElement( elementVariant.GetString() );
                    if( element is Array ) {
                        int i = index.GetInt()-1;
                        var array = ( (Array)element );

                        foreach(object o in (IEnumerable)val) {
                            Type elementType = array.GetType().GetElementType();
                            array.SetValue( Convert.ChangeType( o, elementType ), i );
                            i++;
                        }
                        runtimeStruct.SetElement( elementVariant.GetString(), element );
                        return Variant.Create( runtimeStruct.GetElement( elementVariant.GetInt()-1 ) );
                    }
                }
                else {
                    runtimeStruct.SetElement( elementVariant.GetInt()-1, value.GetValue() );
                    return Variant.Create( runtimeStruct.GetElement( elementVariant.GetInt()-1 ) );
                }
            }
            throw new NotImplementedException();
        }

        public Variant DllStructGetSize( IRuntimeStruct runtimeStruct ) {
            if( runtimeStruct == null ) {
                throw new StructNotAValidStructReturnedByDllStructCreateException( 1, 0, string.Empty );
            }

            return Marshal.SizeOf( runtimeStruct );
        }

        public Variant DllStructGetPtr( StructVariant structVariant, Variant element ) {
            if( structVariant == null ) {
                throw new StructNotAValidStructReturnedByDllStructCreateException( 1, 0, string.Empty );
            }

            if( element != null ) {
                throw new NotImplementedException();
            }

            structVariant.InitUnmanaged();
            return structVariant.Ptr;
        }

        public Variant DllStructGetData( StructVariant runtimeStruct, Variant elementVariant, Variant index ) {
            if( runtimeStruct == null ) {
                throw new StructNotAValidStructReturnedByDllStructCreateException( 1, 0, string.Empty );
            }

            if( index <= 0 ) {
                throw new IndexSmallerEqualNullException( 5, 0, string.Empty );
            }

            object element;
            if( elementVariant.IsInt32 ) {
                element = runtimeStruct.GetElement( elementVariant.GetInt() );
            }
            else {
                element = runtimeStruct.GetElement( elementVariant.GetString() );
            }

            if( element == null ) {
                throw new ElementOutOfRangeException( 2, 0, string.Empty );
            }

            if( index.IsDefault
                || !( element is IEnumerable ) ) {
                return Variant.Create( element );
            }

            List<object> list = ( (IEnumerable)element ).Cast<object>().ToList();
            try {
                return Variant.Create( list[index-1] );
            }
            catch(System.IndexOutOfRangeException) {
                throw new IndexOutOfRangeException( 3, 0, string.Empty );
            }
        }
    }
}
