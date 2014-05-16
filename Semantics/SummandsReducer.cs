using System;
using System.Collections.Generic;
using System.Linq;
using CanonEquation.Equations;
using CanonEquation.Lexems;

namespace CanonEquation.Semantics
{
	public sealed class SummandsReducer : ISummandsReducer
	{
		private const float Epsilon = 0.001f;
		private IList<SimilarSummand> _mapSummands;

		public IEnumerable<Summand> Reduce(IEnumerable<Summand> summands)
		{
			if (summands == null) throw new ArgumentNullException("summands");

			_mapSummands = summands
				.Select(CreateSimilarSummand)
				.ToList();

			var result = new List<Summand>();

			foreach (var firstSimilar in _mapSummands)
			{
				var reduceList = FindSimilarsFor(firstSimilar);
				if (!reduceList.Any())
					continue;

				var reducedSummond = ReduceSummands(ref reduceList);

				if (Math.Abs(reducedSummond.Coefficient - 0) > Epsilon)
					result.Add(reducedSummond);
			}
			return result;
		}

		private SimilarSummand CreateSimilarSummand(Summand summand)
		{
			return new SimilarSummand { Summand = summand };
		}

		private IList<SimilarSummand> FindSimilarsFor(SimilarSummand firstSimilar)
		{
			var result = new List<SimilarSummand>();
			if (!firstSimilar.IsSimilar)
				result.Add(firstSimilar);

			
			foreach (var secondSimilar in _mapSummands)
			{
				if (secondSimilar.IsSimilar ||
					secondSimilar.Equals(firstSimilar))
					continue;

				if (firstSimilar.Summand.IsSimilarTo(secondSimilar.Summand))
					result.Add(secondSimilar);
			}
			return result;
		}

		private Summand ReduceSummands(ref IList<SimilarSummand> similarSummands)
		{
			if (!similarSummands.Any()) throw new ArgumentException("SimilarSummands collection is empty");

			float coefficient = 0f;
			foreach (var summand in similarSummands)
			{
				summand.IsSimilar = true;
				coefficient += summand.Summand.Coefficient;
			}
			return new Summand(similarSummands.First().Summand.Variables, coefficient);
		} 
	}
}