using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AutoJITRuntime;
using NUnit.Framework;

namespace UnitTests
{
    public class AutoitFunctionTests : AutoitFunctionTestBase
    {
        private readonly AutoitRuntime<object> _autoitRuntime;

        public AutoitFunctionTests() {
            _autoitRuntime = new AutoitRuntime<object>( new AutoitContext<object>( new object() ) );
        }

        [TestCase( "Hallo", 2, "Ha" )]
        [TestCase( "AHAHAHA", 1, "A" )]
        [TestCase( "VERY sonderzeichen ! dumdadumm lol xD", 21, "VERY sonderzeichen ! " )]
        [TestCase( "1237831823", 4, "1237" )]
        [TestCase( "*''*=)", 2, "*'" )]
        [TestCase( "######", 3, "###" )]
        [TestCase( "1--_", 4, "1--_" )]
        [TestCase( "Hallo", 5, "Hallo" )]
        public void Test_StringLeft( object @string, object count, object result ) {
            Variant dotnetResult = _autoitRuntime.StringLeft( Variant.Create( @string ), Variant.Create( count ) );
            object autoitResult = GetAu3Result( "StringLeft", dotnetResult.GetRealType(), @string, count );
            CompareResults( dotnetResult, autoitResult );
            CompareResults( dotnetResult, result );
        }

        [TestCase( "0.123" )]
        [TestCase( 0.123 )]
        public void Test_ACos( object dec ) {
            Variant dotnetResult = _autoitRuntime.ACos( Variant.Create( dec ) );
            object autoitResult = GetAu3Result( "ACos", dotnetResult.GetRealType(), dec );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "0.123" )]
        [TestCase( 0.123 )]
        [TestCase( "a0.123" )]
        public void Test_ASin( object dec ) {
            Variant dotnetResult = _autoitRuntime.ASin( Variant.Create( dec ) );
            object autoitResult = GetAu3Result( "ASin", dotnetResult.GetRealType(), dec );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "123" )]
        [TestCase( 123 )]
        [TestCase( "123.123" )]
        [TestCase( 123.123 )]
        public void Test_Abs( object dec ) {
            Variant dotnetResult = _autoitRuntime.Abs( Variant.Create( dec ) );
            object autoitResult = GetAu3Result( "Abs", dotnetResult.GetRealType(), dec );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "A" )]
        [TestCase( "B" )]
        [TestCase( "AB" )]
        [TestCase( "awd" )]
        public void Test_Asc( object src ) {
            Variant dotnetResult = _autoitRuntime.Asc( Variant.Create( src ) );
            object autoitResult = GetAu3Result( "Asc", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "A" )]
        [TestCase( "B" )]
        [TestCase( "AB" )]
        [TestCase( "awd" )]
        public void Test_AscW( object src ) {
            Variant dotnetResult = _autoitRuntime.AscW( Variant.Create( src ) );
            object autoitResult = GetAu3Result( "AscW", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_Binary() {
            Variant src = "test";
            Variant dotnetResult = _autoitRuntime.Binary( src );
            var autoitResult = (Variant) GetAu3Result( "Binary", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_BinaryLen() {
            Variant src = "0x10203040";
            Variant dotnetResult = _autoitRuntime.BinaryLen( src );
            object autoitResult = GetAu3Result( "BinaryLen", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_BinaryToString() {
            Variant src = "0x68656c6c6f";
            Variant dotnetResult = _autoitRuntime.BinaryToString( src );
            object autoitResult = GetAu3Result( "BinaryToString", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_BitXOR() {
            Variant num1 = 123;
            Variant num2 = 312;
            Variant numN = 1233;

            Variant dotnetresult = _autoitRuntime.BitXOR( num1, num2, numN );
            object autoitResult = GetAu3Result( "BitXOR", dotnetresult.GetRealType(), num1, num2, numN );
            CompareResults( dotnetresult, autoitResult );
        }

        [Test]
        public void Test_BitAND() {
            Variant num1 = 123;
            Variant num2 = 312;
            Variant numN = 1233;

            Variant dotnetresult = _autoitRuntime.BitAND( num1, num2, numN );
            object autoitResult = GetAu3Result( "BitAND", dotnetresult.GetRealType(), num1, num2, numN );
            CompareResults( dotnetresult, autoitResult );
        }

        [TestCase( "123" )]
        [TestCase( 123 )]
        [TestCase( "123.123" )]
        [TestCase( 123.123 )]
        public void Test_Chr( object num1 ) {
            Variant dotnetresult = _autoitRuntime.Chr( Variant.Create( num1 ) );
            object autoitResult = GetAu3Result( "Chr", dotnetresult.GetRealType(), num1 );
            CompareResults( dotnetresult, autoitResult );
        }

        [TestCase( "123" )]
        [TestCase( 123 )]
        [TestCase( "123.123" )]
        [TestCase( 123.123 )]
        public void Test_BitNOT( object src ) {
            Variant dotnetResult = _autoitRuntime.BitNOT( Variant.Create( src ) );
            object autoitResult = GetAu3Result( "BitNOT", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_BitOR() {
            Variant num1 = 123;
            Variant num2 = 312;
            Variant numN = 1233;

            Variant dotnetresult = _autoitRuntime.BitOR( num1, num2, numN );
            object autoitResult = GetAu3Result( "BitOR", dotnetresult.GetRealType(), num1, num2, numN );
            CompareResults( dotnetresult, autoitResult );
        }

        [TestCase( 5, -2 )]
        [TestCase( 222, 3 )]
        [TestCase( 1337, -3 )]
        public void Test_BitShift( object src, object shift ) {
            Variant dotnetresult = _autoitRuntime.BitShift( Variant.Create( src ), Variant.Create( shift ) );
            object autoitResult = GetAu3Result( "BitShift", dotnetresult.GetRealType(), src, shift );
            CompareResults( dotnetresult, autoitResult );
        }

        [Test]
        public void Test_Call() {
            Variant function = "StringLeft";
            Variant @string = "hallo";
            Variant count = 2;

            Variant dotnetresult = _autoitRuntime.Call( function, @string, count );
            object autoitResult = GetAu3Result( "Call", dotnetresult.GetRealType(), function, @string, count );
            CompareResults( dotnetresult, autoitResult );
        }

        [Test]
        public void Test_ClipGet() {
            var staThread = new Thread(
                () => Clipboard.SetText( Guid.NewGuid().ToString() ) );

            staThread.SetApartmentState( ApartmentState.STA );
            staThread.Start();
            staThread.Join();

            Variant dotnetresult = _autoitRuntime.ClipGet();
            object autoitResult = GetAu3Result( "ClipGet", typeof (string) );
            CompareResults( dotnetresult, autoitResult );
        }

        [TestCase( 123, true )]
        [TestCase( "123", false )]
        [TestCase( "123ka", false )]
        public void Test_IsNumber( object possibleNumber, bool isNumber ) {
            Variant dotnetresult = _autoitRuntime.IsNumber( Variant.Create( possibleNumber ) );
            object autoitresult = GetAu3Result( "IsNumber", dotnetresult.GetRealType(), possibleNumber );
            CompareResults( dotnetresult, autoitresult );
            CompareResults( dotnetresult, isNumber );
        }

        [TestCase( "123" )]
        [TestCase( 123 )]
        [TestCase( "123.123" )]
        [TestCase( 123.123 )]
        public void Test_Ceiling( object src ) {
            Variant dotnetResult = _autoitRuntime.Ceiling( Variant.Create( src ) );
            object autoitResult = GetAu3Result( "Ceiling", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "123" )]
        [TestCase( 123 )]
        [TestCase( "123.123" )]
        [TestCase( 123.123 )]
        public void Test_ChrW( object src ) {
            Variant dotnetResult = _autoitRuntime.ChrW( Variant.Create( src ) );
            object autoitResult = GetAu3Result( "ChrW", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "123" )]
        [TestCase( 123 )]
        [TestCase( "123.123" )]
        [TestCase( 123.123 )]
        public void Test_Cos( object src ) {
            Variant dotnetResult = _autoitRuntime.Cos( Variant.Create( src ) );
            object autoitResult = GetAu3Result( "Cos", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "FFF" )]
        [TestCase( "0AA" )]
        [TestCase( "123" )]
        [TestCase( "321" )]
        [TestCase( "F" )]
        public void Test_Dec( object hex ) {
            Variant dotnetResult = _autoitRuntime.Dec( Variant.Create( hex ) );
            object autoitResult = GetAu3Result( "Dec", dotnetResult.GetRealType(), hex );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "FFF", 0 )]
        [TestCase( "FFF", 1 )]
        [TestCase( "FFF", 2 )]
        [TestCase( "FFF", 3 )]
        [TestCase( "0AA", 0 )]
        [TestCase( "0AA", 1 )]
        [TestCase( "0AA", 2 )]
        [TestCase( "0AA", 3 )]
        [TestCase( "123", 0 )]
        [TestCase( "123", 1 )]
        [TestCase( "123", 2 )]
        [TestCase( "123", 3 )]
        [TestCase( "321", 0 )]
        [TestCase( "321", 1 )]
        [TestCase( "321", 2 )]
        [TestCase( "321", 3 )]
        [TestCase( "F", 0 )]
        [TestCase( "F", 1 )]
        [TestCase( "F", 2 )]
        [TestCase( "F", 3 )]
        public void Test_DecExtended( object hex, object flag ) {
            Variant dotnetResult = _autoitRuntime.Dec( Variant.Create( hex ), Variant.Create( flag ) );
            object autoitResult = GetAu3Result( "Dec", dotnetResult.GetRealType(), hex, flag );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DirGetSize() {
            Variant src = Path.GetTempPath();

            Variant dotnetResult = _autoitRuntime.DirGetSize( src );
            object autoitResult = GetAu3Result( "DirGetSize", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DirGetSize_Extended() {
            Variant src = Path.GetTempPath();

            Variant dotnetResult = _autoitRuntime.DirGetSize( src, true );
            object autoitResult = GetAu3Result( "DirGetSize", dotnetResult.GetRealType(), src, true );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DriveGetDrive() {
            Variant type = "ALL";

            Variant dotnetResult = _autoitRuntime.DriveGetDrive( type );
            object autoitResult = GetAu3Result( "DriveGetDrive", dotnetResult.GetRealType(), type );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DriveGetFileSystem() {
            Variant path = @"C:\";

            Variant dotnetResult = _autoitRuntime.DriveGetFileSystem( path );
            object autoitResult = GetAu3Result( "DriveGetFileSystem", dotnetResult.GetRealType(), path );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DriveGetLabel() {
            Variant path = @"C:\";

            Variant dotnetResult = _autoitRuntime.DriveGetLabel( path );
            object autoitResult = GetAu3Result( "DriveGetLabel", dotnetResult.GetRealType(), path );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DriveGetSerial() {
            Variant path = @"C:\";

            Variant dotnetResult = _autoitRuntime.DriveGetSerial( path );
            object autoitResult = GetAu3Result( "DriveGetSerial", dotnetResult.GetRealType(), path );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DriveGetType() {
            Variant path = @"C:\";

            Variant dotnetResult = _autoitRuntime.DriveGetType( path );
            object autoitResult = GetAu3Result( "DriveGetType", dotnetResult.GetRealType(), path );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DriveSpaceFree() {
            Variant path = @"C:\";

            Variant dotnetResult = _autoitRuntime.DriveSpaceFree( path );
            object autoitResult = GetAu3Result( "DriveSpaceFree", dotnetResult.GetRealType(), path );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_DriveSpaceTotal() {
            Variant path = @"C:\";

            Variant dotnetResult = _autoitRuntime.DriveSpaceTotal( path );
            object autoitResult = GetAu3Result( "DriveSpaceTotal", dotnetResult.GetRealType(), path );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( @"C:\", "READY" )]
        [TestCase( @"A:\", "INVALID" )]
        public void Test_DriveStatus( object path, object result ) {
            Variant dotnetResult = _autoitRuntime.DriveStatus( Variant.Create( path ) );
            object autoitResult = GetAu3Result( "DriveStatus", dotnetResult.GetRealType(), path );
            CompareResults( dotnetResult, autoitResult );
            CompareResults( dotnetResult, result );
        }

        [Test]
        public void Test_DirCopy() {
            //given
            Variant srcPath = Path.GetTempPath()+@"\Test\";
            Variant destPath = Path.GetTempPath()+@"\Result\";
            Directory.CreateDirectory( srcPath );

            //when
            _autoitRuntime.DirCopy( srcPath, destPath );

            //then
            Assert.AreEqual( true, Directory.Exists( srcPath ) );
            Assert.AreEqual( true, Directory.Exists( destPath ) );

            //teardown
            Directory.Delete( srcPath );
            Directory.Delete( destPath );
        }

        [Test]
        public void Test_DirCreate() {
            //given
            Variant srcPath = Path.GetTempPath()+@"\Test\";

            //when
            _autoitRuntime.DirCreate( srcPath );

            //then
            Assert.AreEqual( true, Directory.Exists( srcPath ) );

            //teardown
            Directory.Delete( srcPath );
        }

        [Test]
        public void Test_EnvGet() {
            Variant path = @"TMP";

            Variant dotnetResult = _autoitRuntime.EnvGet( path );
            object autoitResult = GetAu3Result( "EnvGet", dotnetResult.GetRealType(), path );
            CompareResults( dotnetResult, autoitResult );
        }

        [Test]
        public void Test_Exp() {
            Variant src = 5;

            Variant dotnetResult = _autoitRuntime.Exp( src );
            object autoitResult = GetAu3Result( "Exp", dotnetResult.GetRealType(), src );
            CompareResults( dotnetResult, autoitResult );
        }

        [TestCase( "123", 123 )]
        [TestCase( 123, 123 )]
        [TestCase( 123.123, 123.123 )]
        [TestCase( 123.0123, 123.0123 )]
        [TestCase( "123.3", 123.3 )]
        [TestCase( "0.33123", 0.33123 )]
        [TestCase( "123.3", 123.3 )]
        [TestCase( "123awd2", 123 )]
        [TestCase( "a123awd2", 0 )]
        public void Test_Number( object src, object result ) {
            Variant dotnetresult = _autoitRuntime.Number( Variant.Create( src ) );
            CompareResults( dotnetresult, result );
        }
    }
}
