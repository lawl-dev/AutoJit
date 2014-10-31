using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.Collection
{
	public sealed class TokenCollection : List<Token>
	{
		public TokenCollection( IEnumerable<Token> tokens ) : base( tokens ) {}

		public TokenCollection() {}

		public override string ToString() {
			return string.Join( " ", this.Select( token => token.ToString() ) );
		}
	}
}
