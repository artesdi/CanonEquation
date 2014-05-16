using CanonEquation.Lexems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanonEquation.Test
{
	[TestClass]
	public class LexemParserTest
	{
		[TestMethod]
		public void EquationStringShouldBeParsedToSummands()
		{
			//GIVEN
			ILexemParser lexemParser = new LexemParser();

			const string inputEquation = "x^2 + 3.5xy - y";

			const int expectedSummandsCount = 3;
			var expectedSummand1 = new Summand(new[] { new Variable('x', degree: 2) });
			var expectedSummand2 = new Summand(new[] { new Variable('x'), new Variable('y') }, coefficient: 3.5f);
			var expectedSummand3 = new Summand(new[] { new Variable('y') }, coefficient: -1);


			// WHEN
			var resultSummands = lexemParser.Parse(inputEquation);

			// THEN
			Assert.AreEqual(resultSummands.Count, expectedSummandsCount);

			Assert.AreEqual(resultSummands[0], expectedSummand1);
			Assert.AreEqual(resultSummands[1], expectedSummand2);
			Assert.AreEqual(resultSummands[2], expectedSummand3);
		}
	}
}