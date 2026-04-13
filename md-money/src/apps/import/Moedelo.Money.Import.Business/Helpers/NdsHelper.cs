using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;

namespace Moedelo.Money.Import.Business.Helpers
{
    public static class NdsHelper
    {
        public static decimal GetNdsFromSum(decimal sum, NdsType? type)
        {
            var ndsRatio = type?.GetNdsRatio() ?? 0m;
            return sum * ndsRatio;
        }
    }
}