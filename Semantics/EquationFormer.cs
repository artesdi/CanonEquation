using System;
using System.Linq;
using System.Collections.Generic;
using CanonEquation.Equations;
using CanonEquation.Lexems;

namespace CanonEquation.Semantics
{
	public sealed class EquationFormer : IEquationFormer
	{
		private readonly IEquationTranslator _equationTranslator = new EquationTranslator();
		private readonly IEquationGrouper _equationGrouper = new EquationGrouper();
		private readonly ISummandsReducer _summandsReducer = new SummandsReducer();

		public IEquation GetCanonicalForm(string equationString)
		{
			if (equationString == null) throw new ArgumentNullException("equationString");

			var equation = _equationTranslator.Translate(equationString);
			var groupedEquation = _equationGrouper.GroupToLeft(equation);

			var redusedSummands = _summandsReducer.Reduce(groupedEquation.LeftPart);
			var sortedSummands = SortSummands(redusedSummands);

			return new Equation(leftPart: sortedSummands);
		}

		private IReadOnlyCollection<Summand> SortSummands(IEnumerable<Summand> redusedSummands)
		{
			return redusedSummands
				.OrderByDescending(summand => summand.Variables.Max(var => var.Degree))
				.ToList();
		}
	}
}