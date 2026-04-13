using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Domain.AccPostings;

namespace Moedelo.Money.Business.AccPostings;

internal static class SubcontoExtensions
{
    internal static long[] GetDistinctIds(this IEnumerable<Subconto> subcontos)
    {
        return subcontos
            ?.Where(x => x.Id > 0)
            .Select(x => x.Id)
            .Distinct()
            .ToArray() ?? Array.Empty<long>();
    }
}