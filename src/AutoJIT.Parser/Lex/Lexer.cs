using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex.Interface;
using AutoJITRuntime;

namespace AutoJIT.Parser.Lex
{
    public sealed class Lexer : ILexer
    {
        private readonly ITokenFactory _tokenFactory;

        public Lexer( ITokenFactory tokenFactory ) {
            _tokenFactory = tokenFactory;
        }

        public TokenCollection Lex( string autoitScript ) {
            string[] lines = Regex.Split( autoitScript, string.Format( "({0})", Environment.NewLine ) );

            var tokenizedLines = lines.Select( ( line, index ) => LexLine( index, line ).ToList() ).ToList();

            for ( int i = tokenizedLines.Count-1; i >= 0; i-- ) {
                if ( tokenizedLines[i].Any( x => x.Type == TokenType.ContinueLine ) ) {
                    tokenizedLines[i].RemoveAt( tokenizedLines[i].Count-1 );
                    tokenizedLines[i].RemoveAt( tokenizedLines[i].Count-1 );
                    tokenizedLines[i].AddRange( tokenizedLines[i+1] );
                    tokenizedLines[i+1].Clear();
                }
            }

            return new TokenCollection( tokenizedLines.SelectMany( x => x ) );
        }

        private IEnumerable<Token> LexLine( int lineNum, string line ) {
            var toReturn = new TokenCollection();

            var tokenQueue = new Queue<char>( line );
            int pos = 0;

            while ( tokenQueue.Any() ) {
                List<char> list = tokenQueue.DequeueWhile( x => x == ' ' || x == '\t' ).ToList();
                pos += list.Count+1;

                if ( !tokenQueue.Any() ) {
                    break;
                }

                char currentChar = tokenQueue.Peek();

                if ( ( char.IsNumber( currentChar ) || currentChar == '.' )
                     &&
                     LexNumber( tokenQueue, toReturn, pos, lineNum ) ) {
                    continue;
                }

                if ( !IsSpecialTokenType( tokenQueue )
                     &&
                     ( char.IsLetter( currentChar ) || currentChar == '_' ) ) {
                    if ( LexKeywordOrFunction( tokenQueue, toReturn, pos, lineNum ) ) {
                        continue;
                    }
                }

                switch (currentChar) {
                    case ';':
                        toReturn.Add( _tokenFactory.CreateEndline( pos, lineNum ) );
                        return toReturn;
                    case '$':
                        HandleVariable( tokenQueue, toReturn, pos, lineNum );
                        break;
                    case '@':
                        HandleMacro( tokenQueue, toReturn, pos, lineNum );
                        break;
                    case '"':
                    case '\'':
                    case '\\':
                        HandleString( tokenQueue, toReturn, pos, lineNum );
                        break;
                    case '+':
                        tokenQueue.Dequeue();
                        switch (tokenQueue.Peek()) {
                            case '=':
                                toReturn.Add( _tokenFactory.CreatePlusAssign( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            default:
                                toReturn.Add( _tokenFactory.CreatePlus( pos, lineNum ) );
                                break;
                        }
                        break;
                    case '-':
                        tokenQueue.Dequeue();
                        switch (tokenQueue.Peek()) {
                            case '=':
                                toReturn.Add( _tokenFactory.CreateMinusAssign( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            default:
                                toReturn.Add( _tokenFactory.CreateMinus( pos, lineNum ) );
                                break;
                        }
                        break;
                    case '/':
                        tokenQueue.Dequeue();
                        switch (tokenQueue.Peek()) {
                            case '=':
                                toReturn.Add( _tokenFactory.CreateDivAssign( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            default:
                                toReturn.Add( _tokenFactory.CreateDiv( pos, lineNum ) );
                                break;
                        }
                        break;
                    case '^':
                        tokenQueue.Dequeue();
                        switch (tokenQueue.Peek()) {
                            case '=':
                                toReturn.Add( _tokenFactory.CreatePowAssign( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            default:
                                toReturn.Add( _tokenFactory.CreatePow( pos, lineNum ) );
                                break;
                        }
                        break;
                    case '*':
                        tokenQueue.Dequeue();
                        switch (tokenQueue.Peek()) {
                            case '=':
                                toReturn.Add( _tokenFactory.CreateMultAssign( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            default:
                                toReturn.Add( _tokenFactory.CreateMult( pos, lineNum ) );
                                break;
                        }
                        break;
                    case '(':
                        tokenQueue.Dequeue();
                        toReturn.Add( _tokenFactory.CreateLeftparen( pos, lineNum ) );
                        break;
                    case ')':
                        tokenQueue.Dequeue();
                        toReturn.Add( _tokenFactory.CreateRightparen( pos, lineNum ) );
                        break;
                    case '=':
                        tokenQueue.Dequeue();
                        switch (tokenQueue.Peek()) {
                            case '=':
                                toReturn.Add( _tokenFactory.CreateStringEqual( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            default:
                                toReturn.Add( _tokenFactory.CreateEqual( pos, lineNum ) );
                                break;
                        }
                        break;
                    case ',':
                        tokenQueue.Dequeue();
                        toReturn.Add( _tokenFactory.CreateComma( pos, lineNum ) );
                        break;
                    case '&':
                        tokenQueue.Dequeue();
                        char currentCar = tokenQueue.Peek();
                        if ( currentCar == '=' ) {
                            toReturn.Add( _tokenFactory.CreateConcatJoin( pos, lineNum ) );
                            tokenQueue.Dequeue();
                        }
                        else {
                            toReturn.Add( _tokenFactory.CreateConcat( pos, lineNum ) );
                        }
                        break;
                    case '[':
                        tokenQueue.Dequeue();
                        toReturn.Add( _tokenFactory.CreateLeftsubscript( pos, lineNum ) );
                        break;
                    case ']':
                        tokenQueue.Dequeue();
                        toReturn.Add( _tokenFactory.CreateRightsubscript( pos, lineNum ) );
                        break;
                    case '<':
                        tokenQueue.Dequeue();
                        currentChar = tokenQueue.Peek();
                        switch (currentChar) {
                            case '>':
                                toReturn.Add( _tokenFactory.CreateNotEqual( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            case '=':
                                toReturn.Add( _tokenFactory.CreateLessEqual( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            default:
                                toReturn.Add( _tokenFactory.CreateLess( pos, lineNum ) );
                                break;
                        }
                        break;
                    case '>':
                        tokenQueue.Dequeue();
                        switch (tokenQueue.Peek()) {
                            case '=':
                                toReturn.Add( _tokenFactory.CreateGreaterEqual( pos, lineNum ) );
                                tokenQueue.Dequeue();
                                break;
                            default:
                                toReturn.Add( _tokenFactory.CreateGreater( pos, lineNum ) );
                                break;
                        }
                        break;
                    case 'O':
                    case 'o':
                        tokenQueue.Dequeue();
                        if ( tokenQueue.Peek() == 'R'
                             ||
                             tokenQueue.Peek() == 'r' ) {
                            toReturn.Add( _tokenFactory.CreateOr( pos, lineNum ) );
                            tokenQueue.Dequeue();
                        }
                        else {
                            throw new InvalidParseException( 0, 0, "R" );
                        }
                        break;
                    case 'A':
                    case 'a':
                        tokenQueue.Dequeue();
                        if ( ( tokenQueue.Peek() == 'N' || ( tokenQueue.Peek() == 'n' ) )
                             &&
                             ( tokenQueue.Skip( 1 ).First() == 'D' || tokenQueue.Skip( 1 ).First() == 'd' ) ) {
                            toReturn.Add( _tokenFactory.CreateAnd( pos, lineNum ) );
                            tokenQueue.Dequeue();
                            tokenQueue.Dequeue();
                        }
                        else {
                            throw new InvalidParseException( 0, 0, "ND" );
                        }
                        break;
                    case 'N':
                    case 'n':
                        tokenQueue.Dequeue();
                        if ( ( tokenQueue.Peek() == 'O' || ( tokenQueue.Peek() == 'o' ) )
                             &&
                             ( tokenQueue.Skip( 1 ).First() == 'T' || tokenQueue.Skip( 1 ).First() == 't' ) ) {
                            toReturn.Add( _tokenFactory.CreateNot( pos, lineNum ) );
                            tokenQueue.Dequeue();
                            tokenQueue.Dequeue();
                        }
                        else if ( ( tokenQueue.Peek() == 'U' || tokenQueue.Peek() == 'u' )
                                  &&
                                  ( tokenQueue.Skip( 1 ).First() == 'L' || tokenQueue.Skip( 1 ).First() == 'l' )
                                  &&
                                  ( tokenQueue.Skip( 2 ).First() == 'L' || tokenQueue.Skip( 2 ).First() == 'l' ) ) {
                            tokenQueue.Dequeue();
                            tokenQueue.Dequeue();
                            tokenQueue.Dequeue();
                            toReturn.Add( _tokenFactory.CreateNull( pos, lineNum ) );
                        }
                        else {
                            throw new NotImplementedException( "OT" );
                        }
                        break;
                    case '?':
                        tokenQueue.Dequeue();
                        toReturn.Add( _tokenFactory.CreateQuestionMark( pos, lineNum ) );
                        break;
                    case ':':
                        tokenQueue.Dequeue();
                        toReturn.Add( _tokenFactory.CreateDoubleDot( pos, lineNum ) );
                        break;
                    case '_':
                        tokenQueue.Dequeue();
                        toReturn.Add( _tokenFactory.CreateContinueLine( pos, lineNum ) );
                        break;
                    case '\r':
                        tokenQueue.Dequeue();
                        if ( tokenQueue.Peek() == '\n' ) {
                            tokenQueue.Dequeue();
                            toReturn.Add( _tokenFactory.CreateEndline( pos, lineNum ) );
                        }
                        else {
                            throw new InvalidOperationException(string.Format("Error: Line: {0}, Pos {1}", lineNum, pos));
                        }
                        break;
                    default:
                        throw new InvalidOperationException( string.Format( "Error: Line: {0}, Pos {1}", lineNum, pos ) );
                }
            }
            return toReturn;
        }

        private bool IsSpecialTokenType( IEnumerable<char> line ) {
            var specialKeywords = new List<TokenType> {
                TokenType.AND,
                TokenType.OR,
                TokenType.NOT,
                TokenType.Null
            };

            return specialKeywords.Any( suit => new String( line.Take( suit.ToString().Length ).ToArray() ).Equals( suit.ToString(), StringComparison.InvariantCultureIgnoreCase ) );
        }

        private void HandleMacro( Queue<char> tokenQueue, IList<Token> lineTokens, int pos, int lineNum ) {
            tokenQueue.Dequeue();
            var tokenString = new string( tokenQueue.DequeueWhile( x => char.IsLetterOrDigit( x ) || x == '_' ).ToArray() );
            lineTokens.Add( _tokenFactory.CreateMacro( tokenString, pos, lineNum ) );
        }

        private void HandleVariable( Queue<char> tokenQueue, IList<Token> lineTokens, int pos, int lineNum ) {
            tokenQueue.Dequeue();
            var tokenString = new string( tokenQueue.DequeueWhile( x => char.IsLetterOrDigit( x ) || x == '_' ).ToArray() );
            lineTokens.Add( _tokenFactory.CreateVariable( tokenString, pos, lineNum ) );
        }

        private void HandleString( Queue<char> tokenQueue, IList<Token> lineTokens, int pos, int lineNum ) {
            char start = tokenQueue.Dequeue();
            var tokenString = new string( tokenQueue.DequeueWhile( x => x != start ).ToArray() );
            tokenQueue.Dequeue();
            lineTokens.Add( _tokenFactory.CreateString( tokenString, pos, lineNum ) );
        }

        private bool LexKeywordOrFunction( Queue<char> line, IList<Token> token, int pos, int lineNum ) {
            string functionOrKeyword = string.Join( "", line.TakeWhile( x => char.IsLetterOrDigit( x ) || x == '_' ) );
            if ( functionOrKeyword == "_" ) {
                return false;
            }

            Keywords result;
            if ( Enum.TryParse( functionOrKeyword, true, out result ) ) {
                token.Add( _tokenFactory.CreateKeyword( result, pos, lineNum ) );
                line.Dequeue( functionOrKeyword.Length ).ToList();
                return true;
            }

            MethodInfo function = typeof (AutoitRuntime<>).GetMethods().FirstOrDefault( m => m.Name.Equals( functionOrKeyword, StringComparison.InvariantCultureIgnoreCase ) );

            if ( function != null ) {
                token.Add( _tokenFactory.CreateFunction( function.Name, pos, lineNum ) );
                line.Dequeue( functionOrKeyword.Length ).ToList();
                return true;
            }

            token.Add( _tokenFactory.CreateUserfunction( functionOrKeyword, pos, lineNum ) );
            line.Dequeue( functionOrKeyword.Length );
            return true;
        }

        private bool LexNumber( Queue<char> tokenQueue, IList<Token> lineTokens, int pos, int lineNum ) {
            string tempString = string.Empty;
            if ( ( tokenQueue.Peek() == '0' )
                 &&
                 ( tokenQueue.Count > 1 && ( tokenQueue.Skip( 1 ).First() == 'x' || tokenQueue.Skip( 1 ).First() == 'X' ) ) ) {
                int intResult;
                tokenQueue.Dequeue();
                tokenQueue.Dequeue();
                tempString = new string( tokenQueue.DequeueWhile( char.IsLetterOrDigit ).ToArray() );

                if ( !int.TryParse( tempString, NumberStyles.HexNumber, null, out intResult ) ) {
                    return false;
                }

                lineTokens.Add( _tokenFactory.CreateInt( intResult, pos, lineNum ) );

                return true;
            }

            bool isDouble = false;
            bool isScientific = false;
            bool isHex = false;
            bool isEnd = false;
            tempString += tokenQueue.DequeueWhile( char.IsNumber ).Join();
            while ( tokenQueue.Any() ) {
                char ch = tokenQueue.Peek();
                switch (ch) {
                    case '.':
                        if ( isDouble ) {
                            throw new InvalidParseException( lineNum, pos, "Unexpected Token: '.'" );
                        }
                        isDouble = true;
                        tempString += tokenQueue.Dequeue();
                        tempString += tokenQueue.DequeueWhile( char.IsNumber ).Join();
                        break;
                    case 'e': // scientific notation
                    case 'E': // scientific notation
                        if ( isScientific ) {
                            throw new InvalidParseException( lineNum, pos, "Unexpected Token: 'E'" );
                        }
                        isScientific = true;
                        isDouble = true;
                        tempString += tokenQueue.Dequeue();

                        char next = tokenQueue.Peek();
                        if ( next == '+'
                             ||
                             next == '-' ) {
                            tempString += tokenQueue.Dequeue();
                        }
                        tempString += tokenQueue.DequeueWhile( char.IsNumber ).Join();
                        break;
                    case 'x':
                    case 'X':
                        if ( tempString != "0" ) {
                            throw new InvalidParseException( lineNum, pos, "Unexpected Token: 'X'" );
                        }
                        tempString += tokenQueue.Dequeue();
                        isHex = true;
                        break;
                    default:
                        isEnd = true;
                        break;
                }
                if ( isEnd ) {
                    break;
                }
            }

            if ( isDouble ) {
                double doubleValue = double.Parse( tempString, CultureInfo.InvariantCulture );
                lineTokens.Add( _tokenFactory.CreateDouble( doubleValue, pos, lineNum ) );
            }
            else {
                long tempInt64 = Int64.Parse( tempString );
                if ( tempInt64 > int.MaxValue
                     ||
                     tempInt64 < int.MinValue ) {
                    lineTokens.Add( _tokenFactory.CreateInt64( tempInt64, pos, lineNum ) );
                }
                else {
                    int value = int.Parse( tempString );
                    lineTokens.Add( _tokenFactory.CreateInt( value, pos, lineNum ) );
                }
            }
            return true;
        }
    }
}
