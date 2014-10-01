using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    public static class AutoitTypeBridge
    {
        public static Type CreateRuntimeStruct( string @struct ) {
            IEnumerable<ParsedTypeInfo> typeInfos = GetTypeInfos( @struct );
            return CreateStruct( typeInfos );
        }

        public static Type CreateMarshalDelegate( string returnType, params string[] paramtypes ) {
            var parsedReturnType = GetTypeInfos( returnType ).Single();
            var parameter = paramtypes.SelectMany( GetTypeInfos ).ToArray();
            return MakeDelegateType( parsedReturnType, typeof (CallConvStdcall), parameter );
        }

        private static Type MakeDelegateType(ParsedTypeInfo returntype, Type callConv, ParsedTypeInfo[] paramtypes)
        {
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("an"), AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicMod = assemblyBuilder.DefineDynamicModule("MainModule");

            TypeBuilder tb = dynamicMod.DefineType(string.Format("_{0}", Guid.NewGuid().ToString("N")), TypeAttributes.Public | TypeAttributes.Sealed, typeof(MulticastDelegate));

            tb.DefineConstructor(
                MethodAttributes.RTSpecialName |
                MethodAttributes.SpecialName | MethodAttributes.Public |
                MethodAttributes.HideBySig, CallingConventions.Standard,
                new Type[] { typeof(object), typeof(IntPtr) }).
                SetImplementationFlags(MethodImplAttributes.Runtime);

            var inv = tb.DefineMethod("Invoke", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig,
                CallingConventions.Standard, returntype.ManagedType, null,
                new[] {
                    typeof (CallConvCdecl)
                }, paramtypes.Select(x=>x.ManagedType).ToArray(), null, null);
            

            for (int index = 0; index < paramtypes.Length; index++)
            {
                var paramtype = paramtypes[index];
                var parameterBuilder = inv.DefineParameter(
                    index+1, ParameterAttributes.In, null );

                if (paramtype.MarshalAs.HasValue)
                {
                    var constructorInfo = typeof(MarshalAsAttribute).GetConstructor(new[] { typeof(UnmanagedType) });
                    var customAttributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[] { paramtype.MarshalAs });
                    parameterBuilder.SetCustomAttribute(customAttributeBuilder);   
                }
            }

            inv.SetImplementationFlags(MethodImplAttributes.Runtime);

            var t = tb.CreateType();
            return t;
        }


        private static IEnumerable<ParsedTypeInfo> GetTypeInfos( string @struct ) {
            var toReturn = new List<ParsedTypeInfo>();
            foreach (var fragment in @struct.Split( ';' )) {
                var nametypeFragments = fragment.Split(' ');
                if ( nametypeFragments.Length == 1 ) {
                    var typeFragmanet = nametypeFragments[0];
                    var typeArraySizeFragments = typeFragmanet.Split( new[]{"[", "]"}, StringSplitOptions.RemoveEmptyEntries );
                    var marshalAttribute = GetMarshalAttribute( typeArraySizeFragments[0] );
                    int arraySize = 0;
                    if ( typeArraySizeFragments.Length == 2 ) {
                        arraySize = int.Parse(typeArraySizeFragments[1]);
                    }

                    var managedType = GetManagedType(typeArraySizeFragments[0]);

                    if ( arraySize > 0 ) {
                        managedType = managedType.MakeArrayType();
                    }

                    toReturn.Add( new ParsedTypeInfo( "_" + Guid.NewGuid().ToString("N"), managedType, marshalAttribute, arraySize ) );
                    continue;
                }

                if ( nametypeFragments.Length == 2 ) {
                    var typeFragment = nametypeFragments[0];
                    var nameArraySizeFragment = nametypeFragments[1];


                    var nameArraySizeFragments = nameArraySizeFragment.Split(new[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                    var managedType = GetManagedType(typeFragment);
                    var marshalAttribute = GetMarshalAttribute(typeFragment);

                    int arraySize = 0;
                    if (nameArraySizeFragments.Length == 2)
                    {
                        arraySize = int.Parse(nameArraySizeFragments[1]);
                    } 
                    if ( arraySize > 0 ) {
                        managedType = managedType.MakeArrayType();
                    }
                    

                    var name = nameArraySizeFragments[0];


                    toReturn.Add( new ParsedTypeInfo( name, managedType, marshalAttribute, arraySize ) );
                    continue;
                }

                throw new InvalidOperationException();
            }
            return toReturn;
        }

        private static UnmanagedType? GetMarshalAttribute(string typeName)
        {
            switch (typeName.ToUpper())
            {
                case "STR":
                    return UnmanagedType.LPStr;
                case "WSTR":
                    return UnmanagedType.LPWStr;
            }
            return null;
        }

        private static Type GetManagedType(string typeName) {
            bool isRef = false;
            if ( typeName.EndsWith( "*" ) ) {
                isRef = true;
                typeName = typeName.TrimEnd('*');
            }
            Type toRetur = null;
            switch (typeName.ToUpper())
            {
                case "BYTE":
                    toRetur = typeof(System.Byte);
                    break;
                case "BOOLEAN":
                    toRetur = typeof(System.Boolean);
                    break;
                case "CHAR":
                    toRetur = typeof(System.Char);
                    break;
                case "WCHAR":
                    toRetur = typeof(System.Char);
                    break;
                case "SHORT":
                    toRetur = typeof(System.Int16);
                    break;
                case "USHORT":
                    toRetur = typeof(System.UInt16);
                    break;
                case "WORD":
                    toRetur = typeof(System.UInt16);
                    break;
                case "INT":
                    toRetur = typeof(System.Int32);
                    break;
                case "LONG":
                    toRetur = typeof(System.Int32);
                    break;
                case "BOOL":
                    toRetur = typeof(System.Int32);
                    break;
                case "UINT":
                    toRetur = typeof(System.UInt32);
                    break;
                case "ULONG":
                    toRetur = typeof(System.UInt32);
                    break;
                case "DWORD":
                    toRetur = typeof(System.UInt32);
                    break;
                case "INT64":
                    toRetur = typeof(System.Int64);
                    break;
                case "UINT64":
                    toRetur = typeof(System.UInt64);
                    break;
                case "PTR":
                    toRetur = typeof(System.IntPtr);
                    break;
                case "HWND":
                    toRetur = typeof(System.IntPtr);
                    break;
                case "HANDLE":
                    toRetur = typeof(System.IntPtr);
                    break;
                case "FLOAT":
                    toRetur = typeof(System.Single);
                    break;
                case "DOUBLE":
                    toRetur = typeof(System.Double);
                    break;
                case "INT_PTR":
                case "LONG_PTR":
                case "LRESULT":
                case "LPARAM":
                    toRetur = typeof(System.IntPtr);
                    break;
                case "UINT_PTR":
                case "ULONG_PTR":
                case "DWORD_PTR":
                case "WPARAM":
                    toRetur = typeof(System.UIntPtr);
                    break;
                case "WSTR":
                case "STR":
                    toRetur = typeof(System.String);
                    break;
            }
            return isRef
                ? toRetur.MakeByRefType()
                : toRetur;

        }
        

        private static Type CreateStruct(IEnumerable<ParsedTypeInfo> typeInfos)
        {
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("an"), AssemblyBuilderAccess.Run);
            var dynamicMod = assemblyBuilder.DefineDynamicModule("MainModule");


            var constructorInfo = typeof(StructLayoutAttribute).GetConstructor(new[] { typeof(LayoutKind) });
            var customAttributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[] { LayoutKind.Sequential });

            TypeBuilder tb = dynamicMod.DefineType("_" + Guid.NewGuid().ToString("N"), TypeAttributes.Public, typeof(object), new[] { typeof(IRuntimeStruct) });
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
                if ( typeInfo.MarshalAs.HasValue ) {
                    var constructorInfo2 = typeof (MarshalAsAttribute).GetConstructor( new[] { typeof (UnmanagedType) } );
                    var customAttributeBuilder2 = new CustomAttributeBuilder( constructorInfo2, new object[] { typeInfo.MarshalAs.Value } );
                    fieldBuilder.SetCustomAttribute( customAttributeBuilder2 );
                }

                if (typeInfo.ArraySize > 0)
                {
                    ilGenerator.Emit( OpCodes.Ldarg_0 );
                    ilGenerator.Emit(OpCodes.Ldc_I4_S, typeInfo.ArraySize);
                    ilGenerator.Emit(OpCodes.Newarr, typeInfo.ManagedType.GetElementType());
                    ilGenerator.Emit(OpCodes.Stfld, fieldBuilder);
                }
            }

            ilGenerator.Emit(OpCodes.Ret);

            var t = tb.CreateType();
            return t;
        }

        class ParsedTypeInfo
        {
            public readonly string VariableName;
            public readonly Type ManagedType;
            public readonly UnmanagedType? MarshalAs;
            public readonly int ArraySize;
            
            public ParsedTypeInfo( string variableName, Type managedType, UnmanagedType? marshalAs, int arraySize ) {
                VariableName = variableName;
                ManagedType = managedType;
                MarshalAs = marshalAs;
                ArraySize = arraySize;
            }
        }
    }
}