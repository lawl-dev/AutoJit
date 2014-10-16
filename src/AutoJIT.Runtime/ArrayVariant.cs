using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    public sealed class ArrayVariant : Variant
    {
        private Array _value;

        public ArrayVariant(Variant[] value) {
            _value = value;
        }
        
        public ArrayVariant(Variant[,] value) {
            _value = value;
        }
        
        public ArrayVariant(Variant[,,] value) {
            _value = value;
        }

        protected override DataType DataType {
            get { return DataType.Array;}
        }

        public override object GetValue() {
            return _value;
        }

        public override bool IsArray {
            get { return true; }
        }

        public override Variant this[ params int[] index ] {
            get {
                return (Variant) _value.GetValue( index );
            }
            set { _value.SetValue( value, index ); }
        }

        public override string GetString() {
            return string.Empty;
        }

        public override bool GetBool() {
            return false;
        }

        public override double GetDouble() {
            return 0.0d;
        }

        public override long GetInt64() {
            return 0;
        }

        public override int GetInt() {
            return 0;
        }

        public override IntPtr GetIntPtr() {
            throw new NotImplementedException();
        }

        public override byte[] GetBinary() {
            return new byte[0];
        }

        public override Type GetRealType() {
            return typeof (Variant[]);
        }


        public override void InitArray(Variant[] variants)
        {
            switch (_value.Rank)
            {
                case 1:
                    for (int i = 0; i < variants.Length; i++)
                    {
                        this[i] = variants[i];
                    }
                    break;
                case 2:
                    for (int i = 0; i < variants.Length; i++)
                    {
                        for (int j = 0; j < ((Array)variants[i].GetValue()).Length; j++)
                        {
                            this[i, j] = variants[i][j];
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < variants.Length; i++)
                    {
                        for (int j = 0; j < ((Array)variants[i]).Length; j++)
                        {
                            for (int k = 0; k < ((Array)variants[i][j]).Length; k++)
                            {
                                this[i, j, k] = (variants[i][j])[k];
                            }
                        }
                    }
                    break;
            }
        }

        public override void ReDim( params Variant[] indexs ) {
            var newInstance = Array.CreateInstance( typeof (Variant), indexs.Select( x => x.GetInt() ).ToArray() );
            if ( _value.Rank == indexs.Length ) {
                Init(newInstance, _value);
            }
            _value = newInstance;
        }

        private static void Init(Array newArr, Array oldArr)
        {
            var indicies = new int[newArr.Rank];
            SetDimension(newArr, indicies, 0, oldArr);
        }

        private static void SetDimension( Array array, int[] indicies, int dimension, Array oldArr )
        {
            for (int i = 0; i <= array.GetUpperBound(dimension); i++)
            {
                indicies[dimension] = i;

                if ( dimension < array.Rank-1 ) {
                    SetDimension( array, indicies, dimension+1, oldArr );
                }
                else {
                    try {
                        array.SetValue( oldArr.GetValue( indicies ), indicies );
                    }
                    catch (IndexOutOfRangeException exception) {
                        array.SetValue(new NullVariant(), indicies);   
                    }
                }
            }
        }
    }
}