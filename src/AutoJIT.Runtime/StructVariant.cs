using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    public sealed class StructVariant : Variant, IDisposable
    {
        private readonly IRuntimeStruct _value;
        public IntPtr Ptr = IntPtr.Zero;

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
            return new IntPtr();
        }

        public IRuntimeStruct GetStruct() {
            return _value;
        }

        private void SyntToUnmanaged() {
            InitUnmanaged();
            Marshal.StructureToPtr(_value, Ptr, true);
        }
        
        private void SyntToManaged() {
            if ( Ptr != IntPtr.Zero ) {
                Marshal.PtrToStructure( Ptr, _value );
            }
        }

        public object GetElement( string name ) {
            SyntToManaged();
            if ( _fieldInfos.ContainsKey( name.ToLower() ) ) {
                return _fieldInfos[name.ToLower()].GetValue( _value );
            }
            return null;
        }
        
        public object GetElement( int index ) {
            SyntToManaged();
            if ( index >= 0 && index <= _fieldInfos.Count) {
                return _fieldInfos.Values.ElementAt( index ).GetValue( _value );
            }
            return null;
        }

        public bool SetElement(string name, object value)
        {
            SyntToUnmanaged();
            if (_fieldInfos.ContainsKey(name.ToLower()))
            {
                _fieldInfos[name.ToLower()].SetValue(_value, value);
                return true;
            }
            return false;
        }

        public bool SetElement(int index, object value)
        {
            SyntToUnmanaged();
            if (index >= 0 && index <= _fieldInfos.Count)
            {
                _fieldInfos.Values.ElementAt(index).SetValue(_value, value);
                return true;
            }
            return false;
        }

        public void InitUnmanaged() {
            if (Ptr == IntPtr.Zero)
            {
                var sizeOf = Marshal.SizeOf(_value);
                var intPtr = Marshal.AllocHGlobal(sizeOf);
                Ptr = intPtr;
                Marshal.StructureToPtr( _value, Ptr, false );
            }
        }

        public override byte[] GetBinary() {
            return new byte[0];
        }

        public override Type GetRealType() {
            return typeof (IRuntimeStruct);
        }

        public void Dispose() {
            if ( Ptr != IntPtr.Zero ) {
                Marshal.FreeHGlobal( Ptr );
            }
        }
    }
}