using System.Collections.Generic;
using System.Linq;
using CanonEquation.Lexems;
using CanonEquation.Semantics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanonEquation.Test
{
	[TestClass]
	public class SummandReducerTest
	{
		[TestMethod]
		public void EquationShouldBeTransformedToCanonicalForm()
		{
			// GIVEN
			ISummandsReducer reducer = new SummandsReducer();

			var inputSummands = CreateTestSummands();

			var expectedLeftPart = new List<Summand>
				{
					new Summand(new[] {new Variable('x', degree: 2)}),
					new Summand(new[] {new Variable('x'), new Variable('y')}, coefficient: 4.5f),
					new Summand(new[] {new Variable('y', degree: 2)}, coefficient: -1)
				};

			// WHEN
			var resultSummands = reducer.Reduce(inputSummands);

			// THEN
			Assert.IsNotNull(resultSummands);
			Assert.IsTrue(resultSummands.SequenceEqual(expectedLeftPart));
		}

		private IEnumerable<Summand> CreateTestSummands()
		{
			return new List<Summand>
				{
					new Summand(new[] {new Variable('x', degree: 2)}),
					new Summand(new[] {new Variable('x'), new Variable('y')}, coefficient: 3.5f),
					new Summand(new[] {new Variable('y')}),
					new Summand(new[] {new Variable('y', degree: 2)}, coefficient: -1),
					new Summand(new[] {new Variable('x'), new Variable('y')}),
					new Summand(new[] {new Variable('y')}, coefficient: -1)
				};
		}
	}
}