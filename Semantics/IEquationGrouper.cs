using CanonEquation.Equations;

namespace CanonEquation.Semantics
{
	public interface IEquationGrouper
	{
		IEquation GroupToLeft(IEquation equation);
	}
}