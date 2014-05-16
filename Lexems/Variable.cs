namespace CanonEquation.Lexems
{
	public class Variable
	{
		private readonly char _name;
		private readonly int _degree;

		public Variable(char name, int degree = 1)
		{
			_name = name;
			_degree = degree;
		}

		public int Degree
		{
			get { return _degree; }
		}

		public char Name
		{
			get { return _name; }
		}

		public override bool Equals(object obj)
		{
			var variable = obj as Variable;
			
			if (variable == null)
				return false;

			return _degree == variable.Degree && _name == variable.Name;
		}

		public override string ToString()
		{
			return Degree == 1
				? Name.ToString()
				: string.Format("{0}^{1}", _name, _degree);
		}
	}
}