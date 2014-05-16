using System.Collections.Generic;
using System.Linq;
using CanonEquation.Equations;
using CanonEquation.Lexems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanonEquation.Test
{
	[TestClass]
	public class EquationTranslatorTest
	{
		[TestMethod]
		public void EquationShouldBeTranslated()
		{
			// GIVEN
			IEquationTranslator translator = new EquationTranslator();
			
			const string inputEquation = "x^2 + 3.5xy + y = y^2 - xy + y";

			var expectedLeftPart = new List<Summand>
				{
					new Summand(new[] { new Variable('x', degree: 2) }),
					new Summand(new[] { new Variable('x'), new Variable('y') }, coefficient: 3.5f),
					new Summand(new[] { new Variable('y') })
				};
			var expectedRightPart = new List<Summand>
				{
					new Summand(new[] { new Variable('y', degree: 2) }),
					new Summand(new[] { new Variable('x'), new Variable('y') }, coefficient: -1),
					new Summand(new[] { new Variable('y')})
				};

			// WHEN
			var resultEquation = translator.Translate(inputEquation);

			// THEN
			Assert.IsNotNull(resultEquation);
			Assert.IsTrue(resultEquation.LeftPart.SequenceEqual(expectedLeftPart));
			Assert.IsTrue(resultEquation.RightPart.SequenceEqual(expectedRightPart));
		}
	}
}