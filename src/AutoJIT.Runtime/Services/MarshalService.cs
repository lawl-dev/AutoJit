using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class MarshalService
    {
        public MarshalService() {
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName( "an" ), AssemblyBuilderAccess.Run );
            _dynamicMod = assemblyBuilder.DefineDynamicModule( "MainModule" );
        }

        private readonly Dictionary<string, Type> _delegateStore = new Dictionary<string, Type>(); 
        private readonly Dictionary<string, Type> _structStore = new Dictionary<string, Type>();

        private readonly ModuleBuilder _dynamicMod;

        private readonly Dictionary<string, Type> _typeMapping = new Dictionary<string, Type>() {
            { "NONE", typeof (void) },
            { "BYTE", typeof (byte) },
            { "BOOLEAN", typeof (byte) },
            { "CHAR", typeof (char) },
            { "WCHAR", typeof (char) },
            { "SHORT", typeof (Int16) },
            { "USHORT", typeof (UInt16) },
            { "WORD", typeof (UInt16) },
            { "INT", typeof (Int32) },
            { "LONG", typeof (Int32) },
            { "BOOL", typeof (Int32) },
            { "UINT", typeof (UInt32) },
            { "ULONG", typeof (UInt32) },
            { "DWORD", typeof (UInt32) },
            { "INT64", typeof (Int64) },
            { "UINT64", typeof (UInt64) },
            { "PTR", typeof (IntPtr) },
            { "HWND", typeof (IntPtr) },
            { "HANDLE", typeof (IntPtr) },
            { "FLOAT", typeof (Single) },
            { "DOUBLE", typeof (double) },
            { "INT_PTR", typeof (IntPtr) },
            { "LONG_PTR", typeof (IntPtr) },
            { "LRESULT", typeof (IntPtr) },
            { "LPARAM", typeof (IntPtr) },
            { "UINT_PTR", typeof (UIntPtr) },
            { "ULONG_PTR", typeof (UIntPtr) },
            { "DWORD_PTR", typeof (UIntPtr) },
            { "WPARAM", typeof (UIntPtr) },
            { "WSTR", typeof (StringBuilder) },
            { "STR", typeof (StringBuilder) }
        };

        private readonly Dictionary<string, UnmanagedType> _marshalAttributeMapping = new Dictionary<string, UnmanagedType>() {
            { "STR", UnmanagedType.LPStr },
            { "WSTR", UnmanagedType.LPWStr }
        };
            
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

        public Variant DllCall(IntPtr dll, string returnType, string function, Variant[] paramtypen)
        {
            var procAddress = GetProcAddress(dll, function);

            if (procAddress == IntPtr.Zero) {
                throw new ProcAddressZeroException();
            }

            var parameterMarshalInfo = GetParameterInfo( paramtypen );

            var callingConvention = typeof(CallConvStdcall);

            if ( returnType.Contains( ":" ) ) {
                var split = returnType.Split( ':' );

                var customCallingConvention = split[1];
                returnType = split[0];
                
                callingConvention = GetCallingConvention( customCallingConvention );
            }

            var returnMarshalInfo = GetReturnTypeInfo( returnType );
            
            
            var @delegate = GetFunctionDelegate( returnMarshalInfo, parameterMarshalInfo, callingConvention, procAddress );

            var args = parameterMarshalInfo.Select(x => x.Parameter).ToArray();
            
            var result = @delegate.DynamicInvoke(args);
            
            var toReturn = MapReturnValues( args, result );
            return toReturn;
        }

        private static Variant[] MapReturnValues( object[] args, object result ) {
            var toReturn = new Variant[args.Length+1];
            toReturn[0] = Variant.Create( result );
            Array.Copy( args.Select( Variant.Create ).ToArray(), 0, toReturn, 1, args.Length );
            return toReturn;
        }

        private Delegate GetFunctionDelegate( MarshalInfo returnMarshalInfo, List<MarshalInfo> parameterMarshalInfo, Type callingConvention, IntPtr procAddress ) {
            var delegateType = CreateDelegate( returnMarshalInfo, parameterMarshalInfo, callingConvention );

            Delegate @delegate;
            try {
                @delegate = Marshal.GetDelegateForFunctionPointer( procAddress, delegateType );
            }
            catch (Exception ex) {
                throw new BadNumberOfParameterException();
            }
            return @delegate;
        }

        private MarshalInfo GetReturnTypeInfo( string returnType ) {
            MarshalInfo returnMarshalInfo;
            try {
                returnMarshalInfo = GetMarshalInfo( returnType, null );
            }
            catch (UnknowTypeNameException) {
                throw new BadReturnTypeException( returnType );
            }
            return returnMarshalInfo;
        }

        private List<MarshalInfo> GetParameterInfo( Variant[] paramtypen ) {
            var parameterMarshalInfo = new List<MarshalInfo>();
            for ( int i = 0; i < paramtypen.Length; i += 2 ) {
                var typePart = paramtypen[i];
                var value = paramtypen[i+1];

                MarshalInfo marshalInfo;
                try {
                    marshalInfo = GetMarshalInfo( typePart, value );
                }
                catch (UnknowTypeNameException) {
                    throw new BadParameterException( typePart.GetString(), value.GetValue() );
                }

                parameterMarshalInfo.Add( marshalInfo );
            }
            return parameterMarshalInfo;
        }

        private Type GetCallingConvention(string customCallingConvention)
        {
            switch (customCallingConvention.ToUpper())
            {
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
            var cacheKey = String.Format( "Delegate_{0}{1}{2}", returntype.Type, String.Join( String.Empty, paramtypes.Select( x=>x.Type ) ), callingConvention );

            if ( _delegateStore.ContainsKey( cacheKey ) ) {
                return _delegateStore[cacheKey];
            }
            
            var tb = _dynamicMod.DefineType(
                String.Format( "_{0}", Guid.NewGuid().ToString( "N" ) ), TypeAttributes.Public|TypeAttributes.Sealed, typeof (MulticastDelegate) );

            tb.DefineConstructor(
                MethodAttributes.RTSpecialName|
                MethodAttributes.SpecialName|MethodAttributes.Public|
                MethodAttributes.HideBySig, CallingConventions.Standard,
                new Type[] { typeof (object), typeof (IntPtr) } ).
                SetImplementationFlags( MethodImplAttributes.Runtime );

            var inv = tb.DefineMethod(
                "Invoke", MethodAttributes.Public|MethodAttributes.Virtual|MethodAttributes.NewSlot|MethodAttributes.HideBySig,
                CallingConventions.Standard, returntype.Type, null,
                new[] {
                    callingConvention
                }, paramtypes.Select( x => x.Type ).ToArray(), null, null );

            for ( int index = 0; index < paramtypes.Count; index++ ) {
                var paramtype = paramtypes[index];


                var parameterAttributes = paramtype.IsRef
                    ? ParameterAttributes.Out
                    : ParameterAttributes.In;

                if ( paramtype.Type == typeof (StringBuilder) ) {
                    parameterAttributes |= ParameterAttributes.Out;
                }

                if ( typeof (IRuntimeStruct).IsAssignableFrom( paramtype.Type.GetElementType() ) ) {
                    parameterAttributes |= ParameterAttributes.In;
                }

                var parameterBuilder = inv.DefineParameter( index+1, parameterAttributes, null );

                if ( paramtype.MarshalAttribute.HasValue ) {
                    var constructorInfo = typeof (MarshalAsAttribute).GetConstructor( new[] { typeof (UnmanagedType) } );

                    var customAttributeBuilder = new CustomAttributeBuilder( constructorInfo, new object[] { paramtype.MarshalAttribute } );

                    parameterBuilder.SetCustomAttribute( customAttributeBuilder );
                }
            }

            inv.SetImplementationFlags( MethodImplAttributes.Runtime );

            var t = tb.CreateType();


            _delegateStore.Add( cacheKey, t );
            return t;
        }


        public MarshalInfo GetMarshalInfo(string typePart, Variant value)
        {
            var isRef = typePart.EndsWith("*");
            if (isRef)
            {
                typePart = typePart.TrimEnd('*');
            }

            var managedType = typePart.Equals("struct", StringComparison.InvariantCultureIgnoreCase)
                ? value.GetValue().GetType()
                : GetManagedType(typePart);

            var marshalAttribute = GetMarshalAttribute(typePart);

            object changeType = null;
            if (value != null)
            {
                changeType = ConvertAutoitTypeToMarshalType(value, managedType);
            }

            var marshalInfo = new MarshalInfo(changeType, managedType, marshalAttribute, isRef);
            return marshalInfo;
        }

        private Type GetManagedType( string typeName ) {
            var upperTypeName = typeName.ToUpper();
            if ( _typeMapping.ContainsKey( upperTypeName ) ) {
                return _typeMapping[upperTypeName];
            }

            throw new UnknowTypeNameException( typeName );
        }

        private object ConvertAutoitTypeToMarshalType(Variant variant, Type targetType)
        {
            object changeType;

            if (variant.GetRealType() == targetType)
            {
                changeType = variant.GetValue();
            }
            else if (targetType == typeof(IntPtr))
            {
                changeType = new IntPtr(variant.GetInt());
            }
            else if (targetType == typeof(UIntPtr))
            {
                changeType = new UIntPtr((uint)variant.GetInt());
            }
            else if (variant.IsInt32 &&
                      targetType == typeof(uint))
            {
                changeType = unchecked((uint)variant.GetInt());
            }
            else if (targetType == typeof(StringBuilder))
            {
                var s = variant.GetString();
                changeType = new StringBuilder(s, 0, s.Length, UInt16.MaxValue);
            }
            else
            {
                changeType = Convert.ChangeType(variant.GetValue(), targetType);
            }
            return changeType;
        }


        public UnmanagedType? GetMarshalAttribute(string typeName) {
            var upperTypeName = typeName.ToUpper();
            if ( _marshalAttributeMapping.ContainsKey( upperTypeName ) ) {
                return _marshalAttributeMapping[upperTypeName];
            }
            return null;
        }

        public Type CreateRuntimeStruct(string @struct)
        {
            var cacheKey = String.Format( "Struct_{0}", @struct );

            if ( _structStore.ContainsKey( cacheKey ) ) {
                return _structStore[cacheKey];
            }


            var typeInfos = GetTypeInfo(@struct);

            var res = CreateStruct(typeInfos);

            _structStore.Add( cacheKey, res );

            return res;
        }

        private Type CreateStruct(IEnumerable<StructTypeInfo> typeInfos)
        {
            var constructorInfo = typeof(StructLayoutAttribute).GetConstructor(new[] { typeof(LayoutKind) });
            var customAttributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[] { LayoutKind.Sequential });

            var tb = _dynamicMod.DefineType(
                "_" + Guid.NewGuid().ToString("N"), TypeAttributes.Public, typeof(object), new[] { typeof(IRuntimeStruct) });
            tb.SetCustomAttribute(customAttributeBuilder);

            var constructorBuilder =
                tb.DefineConstructor(
                    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard,
                    Type.EmptyTypes);

            var ilGenerator = constructorBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            var superConstructor = typeof(Object).GetConstructor(Type.EmptyTypes);
            ilGenerator.Emit(OpCodes.Call, superConstructor);
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Nop);

            foreach (var typeInfo in typeInfos)
            {
                
                var fieldBuilder = tb.DefineField(typeInfo.VariableName, typeInfo.ManagedType, FieldAttributes.Public);



                if (typeInfo.ArraySize > 0)
                {
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldc_I4, typeInfo.ArraySize);
                    ilGenerator.Emit(OpCodes.Newarr, typeInfo.ManagedType.GetElementType());
                    ilGenerator.Emit(OpCodes.Stfld, fieldBuilder);
                }

                
                
                var attributesToApply = GetCustomAttributes( typeInfo );
                
                foreach (var builder in attributesToApply) {
                    fieldBuilder.SetCustomAttribute( builder );
                }
            }

            ilGenerator.Emit(OpCodes.Ret);

            var t = tb.CreateType();
            return t;
        }

        private static IEnumerable<CustomAttributeBuilder> GetCustomAttributes( StructTypeInfo typeInfo ) {
            var attributesToApply = new List<CustomAttributeBuilder>();

            if ( typeInfo.MarshalAs.HasValue ) {
                var customAttributeConstructorInfoMarshalAs = typeof (MarshalAsAttribute).GetConstructor( new[] { typeof (UnmanagedType) } );
                var customAttributeBuilderMarshalAs = new CustomAttributeBuilder( customAttributeConstructorInfoMarshalAs, new object[] { typeInfo.MarshalAs.Value } );

                attributesToApply.Add( customAttributeBuilderMarshalAs );
            }

            if ( typeInfo.ArraySize > 0 ) {
                var customAttributeConstructorMarshalAsArray = typeof (MarshalAsAttribute).GetConstructor( new[] { typeof (UnmanagedType) } );
                var propertyInfoSizeConst = typeof (MarshalAsAttribute).GetFields().Single( x => x.Name.Equals( "SizeConst" ) );

                var customAttributeBuilderMarshalAsArray = new CustomAttributeBuilder(
                    customAttributeConstructorMarshalAsArray, new object[] { UnmanagedType.ByValArray }, new[] { propertyInfoSizeConst },
                    new object[] { typeInfo.ArraySize } );

                attributesToApply.Add( customAttributeBuilderMarshalAsArray );
            }
            return attributesToApply;
        }

        private IEnumerable<StructTypeInfo> GetTypeInfo(string @struct) {
            return GetTypeInfo( @struct.Split( ';' ) );
        }

        private IEnumerable<StructTypeInfo> GetTypeInfo( string[] fragments ) {
            var isSingleStruct = fragments.First().Equals( "STRUCT", StringComparison.InvariantCultureIgnoreCase ) &&
                    fragments.Last().Equals( "ENDSTRUCT", StringComparison.InvariantCultureIgnoreCase ) &&
                    fragments.Count( x => x.Equals( "STRUCT", StringComparison.InvariantCultureIgnoreCase ) ) == 1 &&
                    fragments.Count( x => x.Equals( "ENDSTRUCT", StringComparison.InvariantCultureIgnoreCase ) ) == 1;

            if ( isSingleStruct ) {
                fragments = fragments.Skip( 1 ).Take( fragments.Length-2 ).ToArray();
            }



            var toReturn = new List<StructTypeInfo>();
            
            for (int index = 0; index < fragments.Length; index++)
            {
                var fragment = fragments[index];


                var nametypeFragments = fragment.Split(' ');
                if (nametypeFragments.Length == 1)
                {
                    var typeFragmanet = nametypeFragments[0];
                    var typeArraySizeFragments = typeFragmanet.Split(new[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                    var typePart = typeArraySizeFragments[0];


                    var marshalAttribute = GetMarshalAttribute(typePart);
                    int arraySize = 0;
                    if (typeArraySizeFragments.Length == 2)
                    {
                        arraySize = Int32.Parse(typeArraySizeFragments[1]);
                    }

                    Type managedType;
                    if (typePart.Equals("STRUCT", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var count = 0;
                        var structPart = new List<string>();
                        do
                        {
                            var isEndStruct = fragments[index].Equals("ENDSTRUCT", StringComparison.InvariantCultureIgnoreCase);

                            if (isEndStruct)
                            {
                                count--;
                            }
                            else {
                                var isStruct = fragments[index].Equals("STRUCT", StringComparison.InvariantCultureIgnoreCase);
                                if (isStruct)
                                {
                                    count++;
                                }
                                else
                                {
                                    structPart.Add(fragments[index]);
                                }
                            }
                            index++;
                        } while (count != 0);

                        var structTypeInfos = GetTypeInfo( structPart.ToArray() );

                        var innerStructType = CreateStruct( structTypeInfos );
                        managedType = innerStructType;
                    }
                    else {
                        managedType = GetManagedType(typePart);

                        if (arraySize > 0)
                        {
                            managedType = managedType.MakeArrayType();
                        }
                    }

                    toReturn.Add(new StructTypeInfo("_" + Guid.NewGuid().ToString("N"), managedType, marshalAttribute, arraySize));
                    continue;
                }

                if (nametypeFragments.Length == 2)
                {
                    var typeFragment = nametypeFragments[0];
                    var nameArraySizeFragment = nametypeFragments[1];

                    var nameArraySizeFragments = nameArraySizeFragment.Split(new[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                    var managedType = GetManagedType(typeFragment);
                    var marshalAttribute = GetMarshalAttribute(typeFragment);

                    int arraySize = 0;
                    if (nameArraySizeFragments.Length == 2)
                    {
                        arraySize = Int32.Parse(nameArraySizeFragments[1]);
                    }
                    if (arraySize > 0)
                    {
                        managedType = managedType.MakeArrayType();
                    }

                    var name = nameArraySizeFragments[0];

                    toReturn.Add(new StructTypeInfo(name, managedType, marshalAttribute, arraySize));
                    continue;
                }
                throw new InvalidOperationException();
            }
            return toReturn;
        }
    }
}
