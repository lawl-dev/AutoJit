using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Lawl.Reflection;

namespace AutoJITRuntime.Variants
{
    public sealed class StructVariant : Variant, IDisposable
    {
        private readonly Dictionary<string, KeyValuePair<object, FieldInfo>> _fieldInfos = new Dictionary<string, KeyValuePair<object, FieldInfo>>();
        private readonly IRuntimeStruct _value;
        public IntPtr Ptr = IntPtr.Zero;

        public StructVariant( IRuntimeStruct value ) {
            _value = value;
            InitFieldInfoRecursiv( value );
        }

        protected override DataType DataType {
            get { return DataType.Struct; }
        }

        public override bool IsStruct {
            get { return true; }
        }

        public void Dispose() {
            if ( Ptr != IntPtr.Zero ) {
                Marshal.FreeHGlobal( Ptr );
            }
        }

        private void InitFieldInfoRecursiv( IRuntimeStruct value ) {
            foreach (FieldInfo fieldInfo in value.GetType().GetFields()) {
                if ( typeof (IRuntimeStruct).IsAssignableFrom( fieldInfo.FieldType ) ) {
                    var instance = fieldInfo.FieldType.CreateInstance<object>();
                    fieldInfo.SetValue( value, instance );
                    InitFieldInfoRecursiv( (IRuntimeStruct) instance );
                }
                else {
                    _fieldInfos.Add( fieldInfo.Name.ToLower(), new KeyValuePair<object, FieldInfo>( value, fieldInfo ) );
                }
            }
        }

        public override object GetValue() {
            return _value;
        }

        public override string GetString() {
            return string.Empty;
        }

        public override bool GetBool() {
            return false;
        }

        public override double GetDouble() {
            return 0.0;
        }

        public override long GetInt64() {
            return 0;
        }

        public override int GetInt() {
            return 0;
        }

        public override IntPtr GetIntPtr() {
            return new IntPtr();
        }

        public IRuntimeStruct GetStruct() {
            return _value;
        }

        public object GetElement( string name ) {
            SyntToManaged();
            if ( _fieldInfos.ContainsKey( name.ToLower() ) ) {
                KeyValuePair<object, FieldInfo> info = _fieldInfos[name.ToLower()];

                return info.Value.GetValue( info.Key );
            }
            return null;
        }

        public object GetElement( int index ) {
            SyntToManaged();
            if ( index >= 0 &&
                 index <= _fieldInfos.Count ) {
                KeyValuePair<object, FieldInfo> info = _fieldInfos.Values.ElementAt( index );

                return info.Value.GetValue( info.Key );
            }
            return null;
        }

        public bool SetElement( string name, object value ) {
            if ( _fieldInfos.ContainsKey( name.ToLower() ) ) {
                KeyValuePair<object, FieldInfo> info = _fieldInfos[name.ToLower()];

                info.Value.SetValue( info.Key, value );
                SyntToUnmanaged();
                return true;
            }
            return false;
        }

        public bool SetElement( int index, object value ) {
            if ( index >= 0 &&
                 index <= _fieldInfos.Count ) {
                KeyValuePair<object, FieldInfo> fieldInfo = _fieldInfos.Values.ElementAt( index );
                object valueToSet;

                if ( value.GetType() != fieldInfo.Value.FieldType ) {
                    valueToSet = Convert.ChangeType( value, fieldInfo.Value.FieldType );
                }
                else {
                    valueToSet = value;
                }

                fieldInfo.Value.SetValue( fieldInfo.Key, valueToSet );
                SyntToUnmanaged();

                return true;
            }
            return false;
        }

        public override byte[] GetBinary() {
            return new byte[0];
        }

        public override Type GetRealType() {
            return _value.GetType();
        }

        public void InitUnmanaged( IntPtr ptr ) {
            Marshal.StructureToPtr( _value, ptr, false );
            Ptr = ptr;
        }

        public void InitUnmanaged() {
            if ( Ptr == IntPtr.Zero ) {
                int sizeOf = Marshal.SizeOf( _value );
                IntPtr intPtr = Marshal.AllocHGlobal( sizeOf );
                Ptr = intPtr;
                Marshal.StructureToPtr( _value, Ptr, false );
            }
        }

        private void SyntToUnmanaged() {
            if ( Ptr == IntPtr.Zero ) {
                return;
            }

            Marshal.StructureToPtr( _value, Ptr, true );
        }

        private void SyntToManaged() {
            if ( Ptr == IntPtr.Zero ) {
                return;
            }

            Marshal.PtrToStructure( Ptr, _value );
        }
    }
}
