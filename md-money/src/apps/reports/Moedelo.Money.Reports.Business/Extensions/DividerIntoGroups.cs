using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Reports.Business.Extensions
{
    public static class DividerIntoGroups
    {
        public static IReadOnlyCollection<IReadOnlyCollection<int>> Divide(IReadOnlyCollection<int> array, int groupSize)
        {
            return array
                .Select((x, i) => (Number: i / groupSize, Value: x))
                .GroupBy(x => x.Number)
                .Select(g => g.Select(x => x.Value).ToArray())
                .ToArray();
        }
    }
}
