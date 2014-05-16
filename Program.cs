using CanonEquation.Infrastructure;
using CanonEquation.Semantics;

namespace CanonEquation
{
	class Program
	{
		private static IInputReader _inputReader;
		private static IOutputWriter _outputWriter;
		private static IEquationFormer _equationFormer;

		static void Main(string[] args)
		{
			_inputReader = new InputReader();
			_outputWriter = new OutputWriter();

			_equationFormer = new EquationFormer();

			while (true)
			{
				var input = _inputReader.Read();
				var result = _equationFormer.GetCanonicalForm(input);
				_outputWriter.Write(result.ToString());
			}
		}
	}
}
