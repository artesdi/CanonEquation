namespace CanonEquation.Equations
{
	public interface IEquationTranslator
	{
		IEquation Translate(string equation);
	}
}