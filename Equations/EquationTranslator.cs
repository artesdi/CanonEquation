using System;
using CanonEquation.Lexems;

namespace CanonEquation.Equations
{
	public sealed class EquationTranslator : IEquationTranslator
	{
		private readonly LexemParser _lexemParser = new LexemParser();

		public IEquation Translate(string equation)
		{
			var splitedEquation = equation.Split(new []{ Symbol.Equality }, StringSplitOptions.RemoveEmptyEntries);

			var leftPart = _lexemParser.Parse(splitedEquation[0]);
			var rightPart = _lexemParser.Parse(splitedEquation[1]);

			return new Equation(leftPart, rightPart);
		}
	}
}