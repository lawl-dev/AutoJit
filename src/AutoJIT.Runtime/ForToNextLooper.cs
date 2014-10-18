using System;
using System.Windows.Forms.VisualStyles;

namespace AutoJITRuntime
{
    public class ForToNextLooper
    {
        private double _start;
        private readonly double _to;
        private readonly double _step;
        private readonly Func<double, double, bool> _condition;

        public Variant Index {
            get { return _start; }
        }

        public ForToNextLooper( Variant start, Variant to, Variant step = null ) {
            _start = start;
            _to = to;
            _step = step ?? 1;
            _condition = GetCondition();
        }

        public bool MoveNext() {
            var res = _condition( _start, _to );
            _start += _step;
            return res;
        }

        private Func<double, double, bool> GetCondition() {
            if ( _start > _to &&
                 _step > 0 ) {
                return ( a, b ) => false;
            }
            if ( _start > _to ) {
                return ( a, b ) => a >= _to;
            }

            if ( _start < _to &&
                 _step < 0 ) {
                return ( a, b ) => false;
            }
            if ( _start < _to ) {
                return ( a, b ) => a <= _to;
            }
            if ( _start == _to ) {
                return ( a, b ) => a == b;
            }
            throw new NotImplementedException( "_start "+_start+"to "+_to+"step "+_step );
        }
    }
}
