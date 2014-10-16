using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    public class MarshalBridge
    {
        public static object Win32ValueToAutoitValue( object arg ) {
            if ( arg is IntPtr ) {
                return ( (IntPtr) arg ).ToInt32();
            }
            if (arg is UIntPtr) {
                return (int)( (UIntPtr) arg ).ToUInt32();
            }
            return arg;
        }

        public static MarshalInfo GetMarshalInfo( string typePart, Variant value ) {
            var isRef = false;
            Type managedType;
            UnmanagedType? marshalAttribute = null;
            if ( typePart.EndsWith( "*" ) ) {
                isRef = true;
                var typeName = typePart.TrimEnd( '*' );
                managedType = GetManagedType( typeName );
                marshalAttribute = GetMarshalAttribute( typeName );
            }
            else {
                managedType = GetManagedType( typePart );
            }
            
            object changeType = null;
            if ( value != null ) {
                if ( managedType == typeof (IntPtr) ) {
                    changeType = new IntPtr( value.GetInt() );
                }
                else if ( managedType == typeof (UIntPtr) ) {
                    changeType = new UIntPtr( (uint) value.GetInt() );
                }
                else {
                    changeType = Convert.ChangeType(value.GetValue(), managedType);   
                }
            }

            var marshalInfo = new MarshalInfo( changeType, managedType, marshalAttribute, isRef );
            return marshalInfo;
        }

        public static Type CreateMarshalDelegate( MarshalInfo returnType, List<MarshalInfo> paramtypes, Type callingConvention )
        {
            return MakeDelegateType(returnType, paramtypes, callingConvention);
        }

        public static Type MakeDelegateType( MarshalInfo returntype, List<MarshalInfo> paramtypes, Type callingConvention )
        {
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("an"), AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicMod = assemblyBuilder.DefineDynamicModule("MainModule");

            TypeBuilder tb = dynamicMod.DefineType(String.Format("_{0}", Guid.NewGuid().ToString("N")), TypeAttributes.Public | TypeAttributes.Sealed, typeof(MulticastDelegate));

            tb.DefineConstructor(
                MethodAttributes.RTSpecialName |
                MethodAttributes.SpecialName | MethodAttributes.Public |
                MethodAttributes.HideBySig, CallingConventions.Standard,
                new Type[] { typeof(object), typeof(IntPtr) }).
                SetImplementationFlags(MethodImplAttributes.Runtime);

            var inv = tb.DefineMethod("Invoke", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig,
                CallingConventions.Standard, returntype.Type, null,
                new[] {
                    callingConvention
                }, paramtypes.Select(x => x.Type).ToArray(), null, null);


            for (int index = 0; index < paramtypes.Count; index++)
            {
                var paramtype = paramtypes[index];
                var parameterBuilder = inv.DefineParameter
                    (
                        index+1, paramtype.IsRef
                            ? ParameterAttributes.Out
                            : ParameterAttributes.In, null );
                
                if (paramtype.MarshalAttribute.HasValue)
                {
                    var constructorInfo = typeof(MarshalAsAttribute).GetConstructor(new[] { typeof(UnmanagedType) });
                    var customAttributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[] { paramtype.MarshalAttribute });
                    parameterBuilder.SetCustomAttribute(customAttributeBuilder);
                }
            }

            inv.SetImplementationFlags(MethodImplAttributes.Runtime);

            var t = tb.CreateType();
            return t;
        }

        public static UnmanagedType? GetMarshalAttribute(string typeName)
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

        public static Type GetManagedType(string typeName)
        {
            Type toRetur = null;
            switch (typeName.ToUpper())
            {
                case "NONE":
                    toRetur = typeof (void);
                    break;
                case "BYTE":
                    toRetur = typeof(System.Byte);
                    break;
                case "BOOLEAN":
                    toRetur = typeof(System.Byte);
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
            return toRetur;
        }
    }
}