using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            var lines = autoitScript.Split( Environment.NewLine.ToEnumerable().ToArray(), StringSplitOptions.None );

            var tokenizesLines = new List<List<Token>>();

            for ( var index = 0; index < lines.Length; index++ ) {
                var line = lines[index];
                var lineTokens = LexLine( index, line ).ToList();
                tokenizesLines.Add( lineTokens );
            }

            for ( int i = tokenizesLines.Count-1; i >= 0; i-- ) {
                if ( tokenizesLines[i].Any( x => x.Type == TokenType.ContinueLine ) ) {
                    tokenizesLines[i].RemoveAt( tokenizesLines[i].Count-1 );
                    tokenizesLines[i].RemoveAt( tokenizesLines[i].Count-1 );
                    tokenizesLines[i].AddRange( tokenizesLines[i+1] );
                    tokenizesLines[i+1].Clear();
                }
            }

            return new TokenCollection( tokenizesLines.SelectMany( x => x ) );
        }

        private IEnumerable<Token> LexLine( int lineNum, string line ) {
            var toReturn = new TokenCollection();

            var tokenQueue = new Queue<char>( line );
            int pos = 0;

            while ( tokenQueue.Any() ) {
                var list = tokenQueue.DequeueWhile( x => x == ' ' || x == '\t' ).ToList();
                pos += list.Count+1;

                if ( !tokenQueue.Any() ) {
                    break;
                }

                var currentChar = tokenQueue.Peek();

                if ( ( char.IsNumber( currentChar ) || currentChar == '.' ) &&
                     LexNumber( tokenQueue, toReturn, pos, lineNum ) ) {
                    continue;
                }

                if ( !IsSpecialTokenType( tokenQueue ) &&
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
                        var currentCar = tokenQueue.Peek();
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
                        if ( tokenQueue.Peek() == 'R' ||
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
                        if ( ( tokenQueue.Peek() == 'N' || ( tokenQueue.Peek() == 'n' ) ) &&
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
                        if ( ( tokenQueue.Peek() == 'O' || ( tokenQueue.Peek() == 'o' ) ) &&
                             ( tokenQueue.Skip( 1 ).First() == 'T' || tokenQueue.Skip( 1 ).First() == 't' ) ) {
                            toReturn.Add( _tokenFactory.CreateNot( pos, lineNum ) );
                            tokenQueue.Dequeue();
                            tokenQueue.Dequeue();
                        }
                        else if ( ( tokenQueue.Peek() == 'U' || tokenQueue.Peek() == 'u' ) &&
                                  ( tokenQueue.Skip( 1 ).First() == 'L' || tokenQueue.Skip( 1 ).First() == 'l' ) &&
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
                    default:
                        throw new InvalidOperationException( string.Format( "Error: Line: {0}, Pos {1}", lineNum, pos ) );
                }
            }

            toReturn.Add( _tokenFactory.CreateEndline( pos, lineNum ) );
            return toReturn;
        }

        private bool IsSpecialTokenType( Queue<char> line ) {
            var specialKeywords = new List<TokenType>() {
                TokenType.AND,
                TokenType.OR,
                TokenType.NOT,
                TokenType.Null
            };
            foreach (TokenType suit in specialKeywords) {
                if ( new String( line.Take( suit.ToString().Length ).ToArray() ).Equals( suit.ToString(), StringComparison.InvariantCultureIgnoreCase ) ) {
                    return true;
                }
            }
            return false;
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
            var start = tokenQueue.Dequeue();
            var tokenString = new string( tokenQueue.DequeueWhile( x => x != start ).ToArray() );
            tokenQueue.Dequeue();
            lineTokens.Add( _tokenFactory.CreateString( tokenString, pos, lineNum ) );
        }

        private bool LexKeywordOrFunction( Queue<char> line, IList<Token> token, int pos, int lineNum ) {
            var functionOrKeyword = string.Join( "", line.TakeWhile( x => char.IsLetterOrDigit( x ) || x == '_' ) );

            Keywords result;
            if ( Enum.TryParse( functionOrKeyword, true, out result ) ) {
                token.Add( _tokenFactory.CreateKeyword( result, pos, lineNum ) );
                line.Dequeue( functionOrKeyword.Length ).ToList();
                return true;
            }

            var function = typeof (AutoitRuntime<>).GetMethods().FirstOrDefault( m => m.Name.Equals( functionOrKeyword, StringComparison.InvariantCultureIgnoreCase ) );

            if ( function != null ) {
                token.Add( _tokenFactory.CreateFunction( function.Name, pos, lineNum ) );
                line.Dequeue( functionOrKeyword.Length ).ToList();
                return true;
            }
            var nextToken = line.Skip( functionOrKeyword.Length ).FirstOrDefault();
            if ( result == Keywords.None &&
                 line.Count > 1 &&
                 nextToken != '\0' &&
                 nextToken == '(' ) {
                token.Add( _tokenFactory.CreaeteUserfunction( functionOrKeyword, pos, lineNum ) );
                line.Dequeue( functionOrKeyword.Length ).ToList();
                return true;
            }
            return false;
        }

        [Flags]
        private enum State
        {
            Digit = 1,
            Comma = 2,
            Exp = 4,
            Sign = 8,
            More = 16,
            Ok = 32
        }

        private bool LexNumber( Queue<char> tokenQueue, IList<Token> lineTokens, int pos, int lineNum ) {
            string tempString = string.Empty;
            if ( ( tokenQueue.Peek() == '0' ) &&
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

            var isDouble = false;
            var state = ( State.Digit|State.Comma|State.Exp|State.More );
            while ( tokenQueue.Any() ) {
                switch (tokenQueue.Peek()) {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        state = ( state&~ State.More )|State.Ok;
                        break;
                    case '.':
                        if ( ( state&State.Comma ) == State.Comma ) {
                            state = ( State.Digit|State.Exp|State.Ok );
                        }
                        isDouble = true;
                        break;
                    case 'e':
                    case 'E':
                        if ( ( state&( State.Exp|State.More ) ) == State.Exp ) {
                            state = ( State.Digit|State.Sign|State.More|State.Ok );
                        }
                        break;
                    case '+':
                    case '-':
                        if ( ( state&State.Sign ) == State.Sign ) {
                            state = ( State.Digit|State.More|State.Ok );
                        }
                        break;
                }

                if ( ( state&State.Ok ) == State.Ok ) {
                    tempString += tokenQueue.Dequeue();
                }
                else if ( ( state&State.More ) == State.More ) {
                    return false;
                }
                else {
                    break;
                }
                state &= ~State.Ok;
            }

            if ( isDouble ) {
                var doubleValue = double.Parse( tempString, CultureInfo.InvariantCulture );
                lineTokens.Add( _tokenFactory.CreateDouble( doubleValue, pos, lineNum ) );
            }
            else {
                var tempInt64 = Int64.Parse( tempString );
                if ( tempInt64 > int.MaxValue ||
                     tempInt64 < int.MinValue ) {
                    lineTokens.Add( _tokenFactory.CreateInt64( tempInt64, pos, lineNum ) );
                }
                else {
                    var value = int.Parse( tempString );
                    lineTokens.Add( _tokenFactory.CreateInt( value, pos, lineNum ) );
                }
            }
            return true;
        }
    }
}
