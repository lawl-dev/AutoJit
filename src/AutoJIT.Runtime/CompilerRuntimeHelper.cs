using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    internal static class CompilerRuntimeHelper
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);


        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        public static Type MakeDelegateType(Type returntype, Type callConv, params string[] paramtypes)
        {
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("an"), AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicMod = assemblyBuilder.DefineDynamicModule("MainModule");

            TypeBuilder tb = dynamicMod.DefineType(string.Format("delegate-maker{0}", Guid.NewGuid()), TypeAttributes.Public | TypeAttributes.Sealed, typeof(MulticastDelegate));

            tb.DefineConstructor(
                MethodAttributes.RTSpecialName |
                MethodAttributes.SpecialName | MethodAttributes.Public |
                MethodAttributes.HideBySig, CallingConventions.Standard,
                new Type[] { typeof(object), typeof(IntPtr) }).
                SetImplementationFlags(MethodImplAttributes.Runtime);

            var inv = tb.DefineMethod("Invoke", MethodAttributes.Public |MethodAttributes.Virtual | MethodAttributes.NewSlot |MethodAttributes.HideBySig,
                CallingConventions.Standard, returntype, null,
                new[] {
                    typeof (CallConvCdecl)
                }, paramtypes.Select( GetManagedType ).ToArray(), null, null);

            for ( int index = 0; index < paramtypes.Length; index++ ) {
                var paramtype = paramtypes[index];
                var parameterBuilder = inv.DefineParameter( index+1, ParameterAttributes.In, null );
                
                if ( paramtype.ToLower() == "wstr" ) {
                    var constructorInfo = typeof(MarshalAsAttribute).GetConstructor(new[] { typeof(UnmanagedType) });
                    var customAttributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[] { UnmanagedType.LPWStr });
                    parameterBuilder.SetCustomAttribute(customAttributeBuilder);  
                }
            }

            inv.SetImplementationFlags(MethodImplAttributes.Runtime);

            var t = tb.CreateType();
            return t;
        }
        

        public static Type CreateStruct(params KeyValuePair<string, TypeInfo>[] TypesInformation)
        {
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("an"), AssemblyBuilderAccess.RunAndSave);
            var dynamicMod = assemblyBuilder.DefineDynamicModule("MainModule");


            var constructorInfo = typeof(StructLayoutAttribute).GetConstructor( new[] { typeof(LayoutKind) } );
            var customAttributeBuilder = new CustomAttributeBuilder( constructorInfo, new object[] { LayoutKind.Sequential} );

            TypeBuilder tb = dynamicMod.DefineType(Guid.NewGuid().ToString(), TypeAttributes.Public, typeof(object), new[]{typeof(IRuntimeStruct)});
            tb.SetCustomAttribute(customAttributeBuilder);

            var constructorBuilder =
                tb.DefineConstructor(
                    MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.RTSpecialName, CallingConventions.Standard,
                    Type.EmptyTypes );
            
            var ilGenerator = constructorBuilder.GetILGenerator();
            ilGenerator.Emit( OpCodes.Ldarg_0 );        

            
            foreach (var typeInfo in TypesInformation) {
                var fieldBuilder = tb.DefineField(typeInfo.Key, typeInfo.Value.Type, FieldAttributes.Public);
                if ( typeInfo.Value.IsArray ) {
                    ilGenerator.Emit(OpCodes.Ldc_I4_S, typeInfo.Value.ArrayLength);
                    ilGenerator.Emit(OpCodes.Newarr, typeInfo.Value.Type.GetElementType());   
                    ilGenerator.Emit(OpCodes.Stfld, fieldBuilder);
                }
            }
            ilGenerator.Emit( OpCodes.Ldarg_0 );
            var superConstructor = typeof(Object).GetConstructor(Type.EmptyTypes);
            ilGenerator.Emit(OpCodes.Call, superConstructor);
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit( OpCodes.Ret );
           
            var t = tb.CreateType();
            return t;
        }

        public static Type GetManagedType(string typeName)
        {
            switch (typeName.ToUpper())
            {
                case "BYTE":
                    return typeof(System.Byte);
                case "BOOLEAN":
                    return typeof(System.Boolean);
                case "CHAR":
                    return typeof(System.Char);
                case "WCHAR":
                    return typeof(System.Char);
                case "SHORT":
                    return typeof(System.Int16);
                case "USHORT":
                    return typeof(System.UInt16);
                case "WORD":
                    return typeof(System.UInt16);
                case "INT":
                    return typeof(System.Int32);
                case "LONG":
                    return typeof(System.Int32);
                case "BOOL":
                    return typeof(System.Int32);
                case "UINT":
                    return typeof(System.UInt32);
                case "ULONG":
                    return typeof(System.UInt32);
                case "DWORD":
                    return typeof(System.UInt32);
                case "INT64":
                    return typeof(System.Int64);
                case "UINT64":
                    return typeof(System.UInt64);
                case "PTR":
                    return typeof(System.IntPtr);
                case "HWND":
                    return typeof(System.IntPtr);
                case "HANDLE":
                    return typeof(System.IntPtr);
                case "FLOAT":
                    return typeof(System.Single);
                case "DOUBLE":
                    return typeof(System.Double);
                case "INT_PTR":
                case "LONG_PTR":
                case "LRESULT":
                case "LPARAM":
                    return typeof(System.IntPtr);
                case "UINT_PTR":
                case "ULONG_PTR":
                case "DWORD_PTR":
                case "WPARAM":
                    return typeof(System.UIntPtr);
                case "WSTR":
                    return typeof(System.String);
            }
            throw new NotImplementedException();
        }
        
        public static object ConverToManagedType( string typeName, Variant value ) {
            if ( typeName.EndsWith( "*" ) ) {
                //var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(value.GetValue()));
                //Marshal.StructureToPtr(value.GetValue(), ptr, true);
                //return ptr;
                typeName = typeName.TrimEnd('*');
            }

            switch (typeName.ToUpper()) {
                case "BYTE":
                    return (Byte)value;
                case "BOOLEAN":
                    return (System.Boolean) value;
                case "CHAR":
                case "WCHAR":
                    return ((System.String)value).First();
                case "SHORT":
                    return (System.Int16)(int)value;
                case "USHORT":
                    return (System.UInt16)(int)value;
                case "WORD":
                    return (System.UInt16)(int)value;
                case "INT":
                    return (Int32)value;
                case "LONG":
                    return (Int32)value;
                case "BOOL":
                    return (Int32)value;
                case "UINT":
                    return (UInt32)(int)value;
                case "ULONG":
                    return (System.UInt32)(int)value;
                case "DWORD":
                    return (System.UInt32)(int)value;
                case "INT64":
                    return (System.Int64)value;
                case "UINT64":
                    return (System.UInt64)(Int64)value;
                case "PTR":
                case "HWND":
                case "HANDLE":
                    if ( value.IsPtr ) {
                        return value.GetIntPtr();
                    }
                return new IntPtr(value);
                case "FLOAT":
                    return (System.Single)(double)value;
                case "DOUBLE":
                    return (System.Double)value;
                case "INT_PTR":
                case "LONG_PTR":
                case "LRESULT":
                case "LPARAM":
                    return new IntPtr((int)value);
                case "UINT_PTR":
                case "ULONG_PTR":
                case "DWORD_PTR":
                case "WPARAM":
                    return new UIntPtr((uint)(int)value);
                case "WSTR":
                    return value.GetString();
            }
            throw new NotImplementedException();
        }

        public static T FromByteArray<T>(byte[] rawValue)
        {
            GCHandle handle = GCHandle.Alloc(rawValue, GCHandleType.Pinned);
            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structure;
        }

        public static byte[] ToByteArray(object value, int maxLength)
        {
            int rawsize = Marshal.SizeOf(value);
            byte[] rawdata = new byte[rawsize];
            GCHandle handle =
                GCHandle.Alloc(rawdata,
                GCHandleType.Pinned);
            Marshal.StructureToPtr(value,
                handle.AddrOfPinnedObject(),
                false);
            handle.Free();
            if (maxLength < rawdata.Length)
            {
                byte[] temp = new byte[maxLength];
                Array.Copy(rawdata, temp, maxLength);
                return temp;
            }
            else
            {
                return rawdata;
            }
        }
    }
}