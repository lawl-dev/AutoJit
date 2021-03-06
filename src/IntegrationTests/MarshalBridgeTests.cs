using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using AutoJIT.Contrib;
using AutoJITRuntime.Services;
using NUnit.Framework;

namespace IntegrationTests
{
    public class MarshalBridgeTests
    {
        private readonly MarshalService _marshalService;

        public MarshalBridgeTests() {
            _marshalService = new MarshalService();
        }

        [Test]
        public void Test_StructCreation() {
            const string complexStruct = "BYTE;BOOLEAN;SHORT;USHORT;WORD;INT;LONG;BOOL;UINT;ULONG;DWORD;INT64;UINT64;PTR;HWND;HANDLE;FLOAT;DOUBLE;INT_PTR;LONG_PTR;LRESULT;LPARAM;UINT_PTR;ULONG_PTR;DWORD_PTR;WPARAM;STR;WSTR";
            string[] typeNames = complexStruct.Split( ';' );

            Type runtimeStruct = _marshalService.CreateRuntimeStruct( complexStruct );
            Assert.DoesNotThrow( () => runtimeStruct.GetConstructors()[0].Invoke( Constants.Array<object>.Empty ) );
            Type type = runtimeStruct;
            for ( int i = 0; i < typeNames.Length; i++ ) {
                string typeName = typeNames[i];
                MarshalAsAttribute attributeType = GetMarshalAttribute( typeName );
                FieldInfo fieldInfo = type.GetFields()[i];
                Assert.IsTrue( GetTypeByName( typeName ) == fieldInfo.FieldType );
                if ( attributeType != null ) {
                    Assert.IsTrue( fieldInfo.CustomAttributes.Any( x => x.AttributeType == typeof (MarshalAsAttribute) ) );
                    CustomAttributeData customAttributeData = fieldInfo.CustomAttributes.Single( x => x.AttributeType == typeof (MarshalAsAttribute) );
                    Assert.IsTrue( customAttributeData.ConstructorArguments.Any( x => x.Value.Equals( (int) attributeType.Value ) ) );
                }
            }
        }

        [Test]
        public void Test_StructCreationNamed() {
            const string complexStruct = "BYTE var0;BOOLEAN var1;SHORT var2;USHORT var3;WORD var4;INT var5;LONG var6;BOOL var7;UINT var8;ULONG var9;DWORD var10;INT64 var11;UINT64 var12;PTR var13;HWND var14;HANDLE var15;FLOAT var16;DOUBLE var17;INT_PTR var18;LONG_PTR var19;LRESULT var20;LPARAM var21;UINT_PTR var22;ULONG_PTR var23;DWORD_PTR var24;WPARAM var25;STR var26;WSTR var27";
            string[] typeNames = complexStruct.Split( ';' );

            Type runtimeStruct = _marshalService.CreateRuntimeStruct( complexStruct );
            Assert.DoesNotThrow( () => runtimeStruct.GetConstructors()[0].Invoke( Constants.Array<object>.Empty ) );
            Type type = runtimeStruct;
            for ( int i = 0; i < typeNames.Length; i++ ) {
                string typeName = typeNames[i].Split( ' ' )[0];
                string varName = typeNames[i].Split( ' ' )[1];
                MarshalAsAttribute attributeType = GetMarshalAttribute( typeName );
                FieldInfo fieldInfo = type.GetFields()[i];
                Assert.AreEqual( fieldInfo.Name, varName );
                Assert.IsTrue( GetTypeByName( typeName ) == fieldInfo.FieldType );
                if ( attributeType != null ) {
                    Assert.IsTrue( fieldInfo.CustomAttributes.Any( x => x.AttributeType == typeof (MarshalAsAttribute) ) );
                    CustomAttributeData customAttributeData = fieldInfo.CustomAttributes.Single( x => x.AttributeType == typeof (MarshalAsAttribute) );
                    Assert.IsTrue( customAttributeData.ConstructorArguments.Any( x => x.Value.Equals( (int) attributeType.Value ) ) );
                }
            }
        }

        [Test]
        public void Test_StructCreationNamedArray() {
            const string complexStruct = "BYTE var0[10];BOOLEAN var1[10];SHORT var2[10];USHORT var3[10];WORD var4[10];INT var5[10];LONG var6[10];BOOL var7[10];UINT var8[10];ULONG var9[10];DWORD var10[10];INT64 var11[10];UINT64 var12[10];PTR var13[10];HWND var14[10];HANDLE var15[10];FLOAT var16[10];DOUBLE var17[10];INT_PTR var18[10];LONG_PTR var19[10];LRESULT var20[10];LPARAM var21[10];UINT_PTR var22[10];ULONG_PTR var23[10];DWORD_PTR var24[10];WPARAM var25[10];STR var26[10];WSTR var27[10]";
            string[] typeNames = complexStruct.Split( ';' );

            Type runtimeStruct = _marshalService.CreateRuntimeStruct( complexStruct );
            Assert.DoesNotThrow( () => runtimeStruct.GetConstructors()[0].Invoke( Constants.Array<object>.Empty ) );
            Type type = runtimeStruct;
            for ( int i = 0; i < typeNames.Length; i++ ) {
                string typeName = typeNames[i].Split( ' ' )[0];
                string varName = typeNames[i].Split( ' ' )[1].Split( '[' )[0];
                MarshalAsAttribute attributeType = GetMarshalAttribute( typeName );
                FieldInfo fieldInfo = type.GetFields()[i];
                Assert.AreEqual( fieldInfo.Name, varName );
                Assert.IsTrue( GetTypeByName( typeName ).MakeArrayType() == fieldInfo.FieldType );
                Assert.IsTrue( fieldInfo.FieldType.IsArray );
                if ( attributeType != null ) {
                    Assert.IsTrue( fieldInfo.CustomAttributes.Any( x => x.AttributeType == typeof (MarshalAsAttribute) ) );
                    CustomAttributeData customAttributeData = fieldInfo.CustomAttributes.Single( x => x.AttributeType == typeof (MarshalAsAttribute) );
                    Assert.IsTrue( customAttributeData.ConstructorArguments.Any( x => x.Value.Equals( (int) attributeType.Value ) ) );
                }
            }
        }

        [Test]
        public void Test_StructCreationArray() {
            const string complexStruct = "BYTE[10];BOOLEAN[10];SHORT[10];USHORT[10];WORD[10];INT[10];LONG[10];BOOL[10];UINT[10];ULONG[10];DWORD[10];INT64[10];UINT64[10];PTR[10];HWND[10];HANDLE[10];FLOAT[10];DOUBLE[10];INT_PTR[10];LONG_PTR[10];LRESULT[10];LPARAM[10];UINT_PTR[10];ULONG_PTR[10];DWORD_PTR[10];WPARAM[10];STR[10];WSTR[10]";
            string[] typeNames = complexStruct.Split( ';' ).Select( x => x.Split( '[' )[0] ).ToArray();

            Type runtimeStruct = _marshalService.CreateRuntimeStruct( complexStruct );
            Assert.DoesNotThrow( () => runtimeStruct.GetConstructors()[0].Invoke( Constants.Array<object>.Empty ) );
            Type type = runtimeStruct;
            for ( int i = 0; i < typeNames.Length; i++ ) {
                string typeName = typeNames[i];
                MarshalAsAttribute attributeType = GetMarshalAttribute( typeName );
                FieldInfo fieldInfo = type.GetFields()[i];
                Assert.IsTrue( GetTypeByName( typeName ).MakeArrayType() == fieldInfo.FieldType );
                if ( attributeType != null ) {
                    Assert.IsTrue( fieldInfo.CustomAttributes.Any( x => x.AttributeType == typeof (MarshalAsAttribute) ) );
                    CustomAttributeData customAttributeData = fieldInfo.CustomAttributes.Single( x => x.AttributeType == typeof (MarshalAsAttribute) );
                    Assert.IsTrue( customAttributeData.ConstructorArguments.Any( x => x.Value.Equals( (int) attributeType.Value ) ) );
                }
            }
        }

        private MarshalAsAttribute GetMarshalAttribute( string typeName ) {
            switch (typeName.ToUpper()) {
                case "STR":
                    return new MarshalAsAttribute( UnmanagedType.LPStr );
                case "WSTR":
                    return new MarshalAsAttribute( UnmanagedType.LPWStr );
            }
            return null;
        }

        public Type GetTypeByName( string typeName ) {
            switch (typeName.ToUpper()) {
                case "BYTE":
                    return typeof (Byte);
                case "BOOLEAN":
                    return typeof (Boolean);
                case "CHAR":
                    return typeof (Char);
                case "WCHAR":
                    return typeof (Char);
                case "SHORT":
                    return typeof (Int16);
                case "USHORT":
                    return typeof (UInt16);
                case "WORD":
                    return typeof (UInt16);
                case "INT":
                    return typeof (Int32);
                case "LONG":
                    return typeof (Int32);
                case "BOOL":
                    return typeof (Int32);
                case "UINT":
                    return typeof (UInt32);
                case "ULONG":
                    return typeof (UInt32);
                case "DWORD":
                    return typeof (UInt32);
                case "INT64":
                    return typeof (Int64);
                case "UINT64":
                    return typeof (UInt64);
                case "PTR":
                    return typeof (IntPtr);
                case "HWND":
                    return typeof (IntPtr);
                case "HANDLE":
                    return typeof (IntPtr);
                case "FLOAT":
                    return typeof (Single);
                case "DOUBLE":
                    return typeof (Double);
                case "INT_PTR":
                case "LONG_PTR":
                case "LRESULT":
                case "LPARAM":
                    return typeof (IntPtr);
                case "UINT_PTR":
                case "ULONG_PTR":
                case "DWORD_PTR":
                case "WPARAM":
                    return typeof (UIntPtr);
                case "WSTR":
                case "STR":
                    return typeof (String);
            }
            throw new NotImplementedException();
        }
    }
}
