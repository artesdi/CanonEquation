using System.Collections.Generic;
using System.Linq;
using CanonEquation.Lexems;
using CanonEquation.Semantics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanonEquation.Test
{
	[TestClass]
	public class EquationFormerTest
	{
		[TestMethod]
		public void EquationShouldBeTransformedToCanonicalForm()
		{
			// GIVEN
			IEquationFormer former = new EquationFormer();
			
			const string inputEquation = "x^2 + 3.5xy + y = y^2 - xy + y";
			
			var expectedLeftPart = new List<Summand>
				{
					new Summand(new[] { new Variable('x', degree: 2) }),
					new Summand(new[] { new Variable('y', degree: 2) }, coefficient: -1),
					new Summand(new[] { new Variable('x'), new Variable('y') }, coefficient: 4.5f)
				};
			var expectedRightPart = new List<Summand> { Summand.Empty() };

			// WHEN
			var resultEquation = former.GetCanonicalForm(inputEquation);

			// THEN
			Assert.IsNotNull(resultEquation);
			Assert.IsTrue(resultEquation.LeftPart.SequenceEqual(expectedLeftPart));
			Assert.IsTrue(resultEquation.RightPart.SequenceEqual(expectedRightPart));
		}
	}
}
