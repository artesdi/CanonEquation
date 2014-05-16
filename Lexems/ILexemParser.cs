using System.Collections.Generic;

namespace CanonEquation.Lexems
{
	public interface ILexemParser
	{
		List<Summand> Parse(string equation);
	}
}