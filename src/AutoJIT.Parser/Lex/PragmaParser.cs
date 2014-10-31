using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.Lex
{
	public sealed class PragmaParser : IPragmaParser
	{
		public string IncludeDependenciesAndResolvePragmas( string autoitScript, PragmaOptions pragmaOptions ) {
			autoitScript = GetScriptWithResolvedIncludes( autoitScript );
			string[] lines = autoitScript.Split(
											    new[] {
												    Environment.NewLine
											    },
												StringSplitOptions.None );

			string toReturn = string.Empty;

			for( int index = 0; index < lines.Length; index++ ) {
				string line = lines[index].TrimStart( '	' ).TrimStart( ' ' );
				bool isPragma = line.StartsWith( "#" );
				if( isPragma ) {
					Queue<char> charQueue = line.ToQueue();

					string pragmaType = string.Join( "", charQueue.DequeueWhile( x => char.IsLetterOrDigit( x ) || x == '#' || x == '-' ) ).ToLower();
					switch(pragmaType) {
						case "#include-once":
							pragmaOptions.IncludeOnce = true;
							break;
						case "#include":
							string include = HandleInclude( charQueue );
							if( !pragmaOptions.Includes.Contains( include ) ) {
								pragmaOptions.Includes.Add( include );
							}
							break;
						case "#notrayicon":
							pragmaOptions.NoTrayIcon = true;
							break;
						case "#requireadmin":
							pragmaOptions.RequireAdmin = true;
							break;
						case "#onautoitstartregister":
							pragmaOptions.OnAutoItStartRegister = HandleOnAutoItStartRegister( charQueue );
							break;
						case "#cs":
						case "#comments-start":
							while( !lines[index].TrimStart( '	' ).TrimStart( ' ' ).StartsWith( "#ce" )
								   && !lines[index].TrimStart( '	' ).TrimStart( ' ' ).StartsWith( "#comments-end" ) ) {
								index++;
							}
							break;
						case "#region":
						case "#endregion":
						case "#forceref":
							break;
						default:
							break;
					}
				}
				else {
					toReturn += string.Format( "{0}{1}", line, Environment.NewLine );
				}
			}
			return toReturn;
		}

		private string GetScriptWithResolvedIncludes( string script ) {
			string toReturn = string.Empty;

			var scripts = new Dictionary<string, string> {
				{
					"ToCompiler", script
				}
			};

			IEnumerable<KeyValuePair<string, string>> includes = ResolveIncludesRecursiv( script );
			foreach(var source in includes) {
				if( !scripts.ContainsKey( source.Key ) ) {
					scripts.Add( source.Key, source.Value );
				}
			}
			foreach(string s in scripts.Select( x => x.Value )) {
				toReturn = string.Format( "{0}{1}{2}", s, Environment.NewLine, toReturn );
			}
			return toReturn;
		}

		private string ExtractIncludes( string script, PragmaOptions pragmaOptions ) {
			string[] lines = script.Split(
										  new[] {
											  Environment.NewLine
										  },
										  StringSplitOptions.None );
			string toReturn = string.Empty;
			foreach(string line in lines) {
				bool isPragma = line.StartsWith( "#" );
				if( isPragma ) {
					Queue<char> charQueue = line.ToQueue();

					string pragmaType = string.Join( "", charQueue.DequeueWhile( x => char.IsLetterOrDigit( x ) || x == '#' || x == '-' ) ).ToLower();
					switch(pragmaType) {
						case "#include":
							string include = HandleInclude( charQueue );
							pragmaOptions.Includes.Add( include );
							break;
					}
				}
				else {
					toReturn += string.Format( "{0}{1}", line, Environment.NewLine );
				}
			}
			return toReturn;
		}

		private string HandleOnAutoItStartRegister( Queue<char> charQueue ) {
			charQueue.DequeueWhile( x => x == ' ' ).ToList();
			ConsumeAndEnsure( charQueue, '"' );
			string toReturn = string.Join( "", charQueue.DequeueWhile( x => char.IsLetterOrDigit( x ) || x == '_' ) );
			ConsumeAndEnsure( charQueue, '"' );
			return toReturn;
		}

		private string HandleInclude( Queue<char> charQueue ) {
			string toReturn;
			charQueue.DequeueWhile( x => x == ' ' ).ToList();

			if( Consume( charQueue, '"' ) ) {
				toReturn = string.Join( "", charQueue.DequeueWhile( x => x != '"' ) );
				ConsumeAndEnsure( charQueue, '"' );
				return toReturn;
			}
			ConsumeAndEnsure( charQueue, '<' );
			toReturn = string.Join( "", charQueue.DequeueWhile( x => x != '>' ) );
			ConsumeAndEnsure( charQueue, '>' );
			return toReturn;
		}

		private void ConsumeAndEnsure( Queue<char> queue, char c ) {
			if( queue.Peek() != c ) {
				throw new InvalidOperationException();
			}
			queue.Dequeue();
		}

		private bool Consume( Queue<char> queue, char c ) {
			if( queue.Peek() == c ) {
				queue.Dequeue();
				return true;
			}
			return false;
		}

		private IEnumerable<KeyValuePair<string, string>> ResolveIncludesRecursiv( string currentScript ) {
			var toReturn = new List<KeyValuePair<string, string>>();

			IEnumerable<string> includes = GetIncludes( currentScript );
			foreach(string include in includes) {
				string includeScript = File.ReadAllText( string.Format( @"C:\Program Files (x86)\AutoIt3\Include\{0}", include ) );
				toReturn.Add( new KeyValuePair<string, string>( include, includeScript ) );
				toReturn.AddRange( ResolveIncludesRecursiv( includeScript ) );
			}
			return toReturn;
		}

		private IEnumerable<string> GetIncludes( string script ) {
			var options = new PragmaOptions();
			ExtractIncludes( script, options );
			return options.Includes;
		}
	}
}
