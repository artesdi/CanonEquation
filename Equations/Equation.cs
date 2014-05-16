using System;
using System.Collections.Generic;
using System.Text;
using CanonEquation.Lexems;

namespace CanonEquation.Equations
{
	public sealed class Equation : IEquation
	{
		private const float Epsilon = 0.001f;

		private readonly IReadOnlyCollection<Summand> _leftPart;
		private readonly IReadOnlyCollection<Summand> _rightPart;

		public Equation(
			IReadOnlyCollection<Summand> leftPart = null,
			IReadOnlyCollection<Summand> rightPart = null)
		{
			_leftPart = leftPart ?? new List<Summand>{ Summand.Empty() };
			_rightPart = rightPart ?? new List<Summand> { Summand.Empty() };
		}

		public IReadOnlyCollection<Summand> LeftPart
		{
			get { return _leftPart; }
		}

		public IReadOnlyCollection<Summand> RightPart
		{
			get { return _rightPart; }
		}

		public override string ToString()
		{
			return string.Format(
				"{0} = {1}",
				FormatEquationPartToString(LeftPart),
				FormatEquationPartToString(RightPart));
		}

		private string FormatEquationPartToString(IEnumerable<Summand> equationPart)
		{
			var builder = new StringBuilder();

			bool firstSummand = true;
			foreach (var summand in equationPart)
			{
				#region Append sign
				if (firstSummand)
				{
					if (summand.Coefficient < 0)
						builder.Append("- ");
					firstSummand = false;
				}
				else
					builder.Append(summand.Coefficient > 0 ? " + " : " - ");
				#endregion

				#region Append coefficient
				if (Math.Abs(Math.Abs(summand.Coefficient) - 1) > Epsilon)
					builder.Append(summand.Coefficient);
				#endregion

				#region Append variables
				if (summand.Variables != null)
					foreach (var variable in summand.Variables)
						builder.Append(variable);
				#endregion
			}

			return builder.ToString();
		}
	}
}