using AutoJITRuntime;
using NUnit.Framework;

namespace UnitTests
{
    public class AutoitMacroTests : AutoitFunctionTestBase
    {
        private readonly AutoitContext<object> _autoitContext;

        public AutoitMacroTests() {
            _autoitContext = new AutoitContext<object>( null );
        }

        [Test]
        public void Test_AppDataCommonDir() {
            var dotnetresult = _autoitContext.AppDataCommonDir;
            var au3Result = base.GetAu3Result( "@AppDataCommonDir", dotnetresult.GetType(), new object[0] );
            Assert.IsTrue( au3Result == dotnetresult );
        }

        [Test]
        public void Test_CommonFilesDir() {
            var dotnetresult = _autoitContext.@CommonFilesDir;
            var au3Result = base.GetAu3Result( "@CommonFilesDir", dotnetresult.GetType(), new object[0] );
            Assert.IsTrue( au3Result == dotnetresult );
        }

        [Test]
        public void Test_AppDataDir() {
            var dotnetresult = _autoitContext.AppDataDir;
            var au3Result = base.GetAu3Result( "@AppDataDir", dotnetresult.GetType(), new object[0] );
            Assert.IsTrue( au3Result == dotnetresult );
        }
    }
}
