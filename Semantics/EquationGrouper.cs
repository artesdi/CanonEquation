using System;
using System.Collections.Generic;
using System.Linq;
using CanonEquation.Equations;
using CanonEquation.Lexems;

namespace CanonEquation.Semantics
{
	public sealed class EquationGrouper : IEquationGrouper
	{
		public IEquation GroupToLeft(IEquation equation)
		{
			if (equation == null) throw new ArgumentNullException("equation");

			var groupedSummands = GetGruopedSummands(equation).ToList();
			return new Equation(leftPart: groupedSummands);
		}

		private IEnumerable<Summand> GetGruopedSummands(IEquation equation)
		{
			var result = new List<Summand>(equation.LeftPart);
			result.AddRange(GetInversedPart(equation.RightPart));

			return result;
		}

		private IEnumerable<Summand> GetInversedPart(IEnumerable<Summand> equationPart)
		{
			return equationPart.Select(GetInversedLexem).ToList();
		}

		private Summand GetInversedLexem(Summand summand)
		{
			return new Summand(summand.Variables, summand.Coefficient * -1);
		}
	}
}