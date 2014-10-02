using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    public sealed class StructVariant : Variant
    {
        private readonly IRuntimeStruct _value;
        private IntPtr _ptr = IntPtr.Zero;
        private readonly Dictionary<string, FieldInfo> _fieldInfos = new Dictionary<string, FieldInfo>();

        public StructVariant(IRuntimeStruct value) {
            _value = value;
            foreach (var fieldInfo in value.GetType().GetFields()) {
                _fieldInfos.Add( fieldInfo.Name.ToLower(), fieldInfo );
            }
        }

        protected override DataType DataType {
            get { return DataType.Struct; }
        }

        public override bool IsStruct {
            get { return true; }
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
            if ( _ptr == IntPtr.Zero ) {
                var size = Marshal.SizeOf( _value );
                var ptr = Marshal.AllocHGlobal( size );
                Marshal.StructureToPtr( _value, ptr, false );
                _ptr = ptr;
            }
            return _ptr;
        }

        public object GetStruct() {
            if ( _ptr != IntPtr.Zero ) {
                Marshal.PtrToStructure( _ptr, _value );
            }
            return _value;
        }

        public object GetElement( string name ) {
            if ( _ptr != IntPtr.Zero ) {
                Marshal.PtrToStructure( _ptr, _value );
            }
            if ( _fieldInfos.ContainsKey( name.ToLower() ) ) {
                return _fieldInfos[name.ToLower()].GetValue( _value );
            }
            return null;
        }
        
        public object GetElement( int index ) {
            if ( _ptr != IntPtr.Zero ) {
                Marshal.PtrToStructure( _ptr, _value );
            }
            if ( index >= 0 && index <= _fieldInfos.Count) {
                return _fieldInfos.Values.ElementAt( index ).GetValue( _value );
            }
            return null;
        }

        public void SetElement(string name, object value)
        {
            if (_ptr != IntPtr.Zero)
            {
                Marshal.PtrToStructure(_ptr, _value);
            }
            if (_fieldInfos.ContainsKey(name.ToLower()))
            {
                _fieldInfos[name.ToLower()].SetValue(_value, value);
                if (_ptr != IntPtr.Zero)
                {
                    Marshal.StructureToPtr(_value, _ptr, false);
                }
            }
        }

        public void SetElement(int index, object value)
        {
            if (_ptr != IntPtr.Zero)
            {
                Marshal.PtrToStructure(_ptr, _value);
            }
            if (index >= 0 && index <= _fieldInfos.Count)
            {
                _fieldInfos.Values.ElementAt(index).SetValue(_value, value);
                if ( _ptr != IntPtr.Zero ) {
                    Marshal.StructureToPtr( _value, _ptr, false );
                }
            }
        }

        

        public override byte[] GetBinary() {
            return new byte[0];
        }

        public override Type GetRealType() {
            return typeof (IRuntimeStruct);
        }
    }
}