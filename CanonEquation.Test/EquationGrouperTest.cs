using System.Collections.Generic;
using System.Linq;
using CanonEquation.Equations;
using CanonEquation.Lexems;
using CanonEquation.Semantics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanonEquation.Test
{
	[TestClass]
	public class EquationGrouperTest
	{
		[TestMethod]
		public void EquationShouldBeGroupedToLeftPart()
		{
			// GIVEN
			IEquationGrouper grouper = new EquationGrouper();

			var inputEquation = CreateTestEquation();

			var expectedLeftPart = new List<Summand>(inputEquation.LeftPart)
				{
					new Summand(new[] { new Variable('y', degree: 2) }, coefficient: -1),
					new Summand(new[] { new Variable('x'), new Variable('y') } ),
					new Summand(new[] { new Variable('y')} , coefficient: -1)
				};
			var expectedRightPart = new List<Summand>{ Summand.Empty() };

			// WHEN
			var resultEquation = grouper.GroupToLeft(inputEquation);

			// THEN
			Assert.IsNotNull(resultEquation);
			Assert.IsTrue(resultEquation.LeftPart.SequenceEqual(expectedLeftPart));
			Assert.IsTrue(resultEquation.RightPart.SequenceEqual(expectedRightPart));
		}

		private IEquation CreateTestEquation()
		{
			var leftPart = new List<Summand>
				{
					new Summand(new[] { new Variable('x', degree: 2) }),
					new Summand(new[] { new Variable('x'), new Variable('y') }, coefficient: 3.5f),
					new Summand(new[] { new Variable('y') })
				};
			var rightPart = new List<Summand>
				{
					new Summand(new[] { new Variable('y', degree: 2) }),
					new Summand(new[] { new Variable('x'), new Variable('y') }, coefficient: -1),
					new Summand(new[] { new Variable('y') })
				};
			return new Equation(leftPart, rightPart);
		}
	}
}