using CanonEquation.Equations;

namespace CanonEquation.Semantics
{
	public interface IEquationFormer
	{
		IEquation GetCanonicalForm(string equationString);
	}
}