using System.Collections;

namespace AutoJITRuntime
{
    internal class VariantEnumerator : IEnumerator
    {
        private readonly IEnumerator _innerEnumerator;

        public VariantEnumerator( IEnumerable enumerable ) {
            _innerEnumerator = enumerable.GetEnumerator();
        }

        public bool MoveNext() {
            return _innerEnumerator.MoveNext();
        }

        public void Reset() {
            _innerEnumerator.Reset();
        }

        public object Current {
            get { return Variant.Create( _innerEnumerator.Current ); }
        }
    }
}
