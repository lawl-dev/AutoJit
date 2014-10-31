using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.Collection
{
	public class TokenQueue : Queue<Token>
	{
		public TokenQueue( IEnumerable<Token> parameter ) : base( parameter ) {}

		protected TokenQueue() {}

		public static implicit operator TokenQueue( TokenCollection list ) {
			return new TokenQueue( list );
		}

		public override string ToString() {
			return string.Join( " ", this.Select( token => token.ToString() ) );
		}
	}
}
