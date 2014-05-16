using System.Collections.Generic;
using CanonEquation.Lexems;

namespace CanonEquation.Semantics
{
	public interface ISummandsReducer
	{
		IEnumerable<Summand> Reduce(IEnumerable<Summand> summands);
	}
}