using System;

namespace CanonEquation.Infrastructure
{
	public class InputReader : IInputReader
	{
		public string Read()
		{
			Console.Write("Enter equation: ");
			return Console.ReadLine();
		}
	}
}