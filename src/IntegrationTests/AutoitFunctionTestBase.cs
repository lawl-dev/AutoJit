using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using AutoJITRuntime;
using Newtonsoft.Json;
using NUnit.Framework;

namespace UnitTests
{
    public abstract class AutoitFunctionTestBase
    {
        private readonly string _au3ExeName = Environment.CurrentDirectory+"/../../..//lib/Unittests.exe";
        private readonly string _resultPath = string.Format( "{0}/", Path.GetTempPath() );

        protected object GetAu3Result( string functionCall, Type expectedType, params object[] parameters ) {
            IEnumerable<string> enumerable = GetFormattedParameter( parameters );

            if ( !functionCall.StartsWith( "@" ) &&
                 !functionCall.StartsWith( "f!" ) ) {
                functionCall += "(";

                if ( parameters.Any() ) {
                    functionCall += string.Join( ",", enumerable );
                }

                functionCall += ")";
            }
            if ( functionCall.StartsWith( "f!" ) ) {
                functionCall = "\""+functionCall.Substring( 2, functionCall.Length-2 )+"\"";
            }
            string filePath = _resultPath+Guid.NewGuid();
            Process process = Process.Start( _au3ExeName, string.Format( "{0} {1}", functionCall, filePath ) );

            if ( process == null ) {
                throw new InvalidOperationException();
            }

            while ( !process.HasExited ) {
                Thread.Sleep( 5 );
            }
            var serializer = new JsonSerializer();
            var reader = new StreamReader( new FileStream( filePath, FileMode.Open, FileAccess.Read ) );
            object o = serializer.Deserialize( reader, expectedType );
            reader.Dispose();
            File.Delete( filePath );
            return o;
        }

        private static IEnumerable<string> GetFormattedParameter( IEnumerable<object> parameters ) {
            var toReturn = new List<string>();

            foreach (object parameter in parameters) {
                if ( parameter is double ) {
                    toReturn.Add( ( (double) parameter ).ToString( CultureInfo.InvariantCulture ) );
                }
                else if ( parameter is int ||
                          parameter is Int64 ||
                          parameter is bool ) {
                    toReturn.Add( ( parameter ).ToString() );
                }
                else {
                    toReturn.Add( string.Format( "'{0}'", parameter ) );
                }
            }
            return toReturn;
        }

        protected void CompareResults( Variant result, object au3Result ) {
            if ( result.IsInt32 ||
                 result.IsInt64 ) {
                Assert.IsTrue( Equals( result, au3Result ) );
                return;
            }

            if ( result.IsDouble ) {
                if ( double.IsPositiveInfinity( result ) ) {
                    Assert.IsTrue( Equals( au3Result, 1.0d ) );
                    return;
                }

                if ( double.IsNegativeInfinity( result ) ) {
                    Assert.IsTrue( Equals( au3Result, -1.0d ) );
                    return;
                }
                double difference = Math.Abs( (double) result * .00000000000001 );
                Assert.IsTrue( Math.Abs( ( (double) result-(double) au3Result ) ) < difference );
                return;
            }

            if ( result.IsString ||
                 result.IsBinary ) {
                Assert.IsTrue( Equals( result, au3Result ) );
                return;
            }

            if ( result.IsBool ) {
                Assert.IsTrue( result.Equals( au3Result ) );
                return;
            }

            throw new NotImplementedException();
        }
    }
}
