using System.Collections.Generic;
using CanonEquation.Lexems;

namespace CanonEquation.Equations
{
	public interface IEquation
	{
		IReadOnlyCollection<Summand> LeftPart { get; }
		IReadOnlyCollection<Summand> RightPart { get; }
	}
}