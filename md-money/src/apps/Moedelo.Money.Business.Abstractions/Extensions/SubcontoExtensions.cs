using System.Collections.Generic;
using System.Linq;
using Moedelo.AccPostings.Enums;
using Moedelo.Money.Business.Abstractions.SyntheticAccounts;
using Moedelo.Money.Domain.AccPostings;

namespace Moedelo.Money.Business.Abstractions.Extensions;

public static class SubcontoExtensions
{
    public static IEnumerable<Subconto> OmitTypesByRules(
        this IEnumerable<Subconto> subcontos,
        IReadOnlyCollection<SyntheticAccountSubcontoRule> rules)
    {
        if (subcontos == null)
        {
            return null;
        }
        
        var omitIndexes = rules?
            .Where(r => r.SubcontoType == SubcontoType.Contract || r.SubcontoType == SubcontoType.Kontragent)
            .Select(r => r.Level - 1)
            .ToArray();

        if (omitIndexes?.Any() != true)
        {
            return subcontos;
        }

        return subcontos
            .Where((_, i) => !omitIndexes.Contains(i))
            .ToArray();
    }
}