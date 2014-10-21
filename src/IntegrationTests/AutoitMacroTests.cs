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
            Variant dotnetresult = _autoitContext.AppDataCommonDir;
            object au3Result = base.GetAu3Result( "@AppDataCommonDir", dotnetresult.GetType(), new object[0] );
            Assert.IsTrue( au3Result == dotnetresult );
        }

        [Test]
        public void Test_CommonFilesDir() {
            Variant dotnetresult = _autoitContext.@CommonFilesDir;
            object au3Result = base.GetAu3Result( "@CommonFilesDir", dotnetresult.GetType(), new object[0] );
            Assert.IsTrue( au3Result == dotnetresult );
        }

        [Test]
        public void Test_AppDataDir() {
            Variant dotnetresult = _autoitContext.AppDataDir;
            object au3Result = base.GetAu3Result( "@AppDataDir", dotnetresult.GetType(), new object[0] );
            Assert.IsTrue( au3Result == dotnetresult );
        }
    }
}
