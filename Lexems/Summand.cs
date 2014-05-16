using System;
using System.Collections.Generic;
using System.Linq;

namespace CanonEquation.Lexems
{
	public sealed class Summand
	{
		private const float Elipson = 0.001f;

		private readonly Variable[] _variables;
		private readonly float _coefficient;

		public Summand(Variable[] variables, float coefficient = 1)
		{
			_variables = variables;
			_coefficient = coefficient;
		}

		public Variable[] Variables
		{
			get { return _variables; }
		}

		public float Coefficient
		{
			get { return _coefficient; }
		}

		public static Summand Empty()
		{
			return new Summand(new Variable[0], 0);
		}

		public bool IsSimilarTo(Summand summand)
		{
			if (summand == null)
				return false;

			if (Variables == null || summand.Variables == null)
				return false;

			if (Variables.Length != summand.Variables.Length)
				return false;

			var sortedFirstVariables = SortVariables(this);
			var sortedSecondVariables = SortVariables(summand);

			return sortedFirstVariables.SequenceEqual(sortedSecondVariables);
		}

		public override bool Equals(object obj)
		{
			var summand = obj as Summand;

			if (summand == null)
				return false;

			if (!IsSimilarTo(summand))
				return false;

			if (Math.Abs(_coefficient - summand.Coefficient) > Elipson)
				return false;

			return true;
		}

		private IEnumerable<Variable> SortVariables(Summand summand)
		{
			return summand.Variables
				.OrderBy(var => var.Name)
				.ThenBy(var => var.Degree)
				.ToList();
		}
	}
}