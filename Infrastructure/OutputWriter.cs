using System;

namespace CanonEquation.Infrastructure
{
	public class OutputWriter : IOutputWriter
	{
		public void Write(string result)
		{
			Console.WriteLine("Canonical form: " + result);
			Console.ReadKey();
		}
	}
}