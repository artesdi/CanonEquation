using System.Linq;
using System.Collections.Generic;

namespace CanonEquation.Lexems
{
	public class LexemParser : ILexemParser
	{
		public List<Summand> Parse(string equation)
		{
			return SplitEquationWithOperators(equation)
				.Select(CreateSummand)
				.ToList();
		}

		private IEnumerable<string> SplitEquationWithOperators(string equation)
		{
			var operators = new[] { Symbol.Plus, Symbol.Minus, Symbol.Equality };
			var result = new List<string>();

			var currentLexem = string.Empty;

			foreach (var ch in equation)
			{
				if (char.IsWhiteSpace(ch) ||
					ch == Symbol.LeftBracket || ch == Symbol.RightBracket)
					continue;

				if (operators.Contains(ch))
				{
					result.Add(currentLexem);
					currentLexem = string.Empty;
				}

				currentLexem += ch;
			}
			result.Add(currentLexem);

			return result;
		}
		
		private Summand CreateSummand(string lexem)
		{
			float coefficient = 1.0f;
			Variable[] variables = null;

			using (var lexemEnumerator = new LexemEnumerator(lexem))
			{
				while (lexemEnumerator.MoveNext())
				{
					if (lexemEnumerator.Current == Symbol.Minus)
						coefficient *= -1;

					if (char.IsDigit(lexemEnumerator.Current))
						coefficient *= ReadNextFloatDigit(lexemEnumerator);

					if (lexemEnumerator.IsHasValue && char.IsLetter(lexemEnumerator.Current))
						variables = ReadVariables(lexemEnumerator);
				}
			}
			return new Summand(variables, coefficient);
		}

		private float ReadNextFloatDigit(LexemEnumerator enumerator)
		{			
			var lexem = enumerator.Current.ToString();

			while (enumerator.MoveNext())
			{
				var ch = enumerator.Current;
				if (char.IsDigit(ch) || ch == Symbol.DotSeparator)
					lexem += ch;
				else
					break;
			}
			return ParseToFloat(lexem);
		}

		private float ParseToFloat(string coefficientLexem)
		{
			return float.Parse(coefficientLexem.Replace('.',','));
		}

		private Variable[] ReadVariables(LexemEnumerator enumerator)
		{
			var resultVariables = new List<Variable>();

			while (enumerator.IsHasValue)
				resultVariables.Add(ReadNextVariable(enumerator));
			
			return resultVariables.ToArray();
		}

		private Variable ReadNextVariable(LexemEnumerator enumerator)
		{
			var variableLexem = enumerator.Current;

			if (enumerator.MoveNext() &&
				enumerator.Current == Symbol.DegreeSymbol)
			{
				enumerator.MoveNext();
				var degree = ReadNextIntDigit(enumerator);

				return new Variable(variableLexem, degree);
			}

			return new Variable(variableLexem);
		}

		private int ReadNextIntDigit(LexemEnumerator enumerator)
		{
			var lexem = enumerator.Current.ToString();

			while (enumerator.MoveNext())
			{
				if (char.IsDigit(enumerator.Current))
					lexem += enumerator.Current;
				else
					break;
			}
			return int.Parse(lexem);
		}
	}
}