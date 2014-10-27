using AutoJITRuntime;
using NUnit.Framework;

namespace UnitTests
{
    internal class ForToNextLooperTests
    {
        [Test]
        public void Test_A() {
            var looper = new ForToNextLooper( 0, 10 );
            Variant result = 0;
            for( Variant i = looper.Index; looper.MoveNext(); i = looper.Index ) {
                result += i;
            }
            Assert.IsTrue( result == 55 );
        }

        [Test]
        public void Test_B() {
            var looper = new ForToNextLooper( 110, 33, -3 );
            Variant result = 0;
            for( Variant i = looper.Index; looper.MoveNext(); i = looper.Index ) {
                result += i;
            }
            Assert.IsTrue( result == 1885 );
        }
    }
}
