using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.ChangeLog.Mappers
{
    internal static class NdsExtensions
    {
        internal static bool IsWithNds(this Nds nds)
        {
            return nds != null &&
                (nds.NdsType.HasValue || nds.NdsSum.HasValue);
        }

        internal static string GetNdsType(this Nds nds)
        {
            return nds?.NdsType?.GetDescription();
        }

        internal static MoneySum GetNdsSum(this Nds nds)
        {
            return nds != null &&
                nds.NdsType != Enums.NdsType.None &&
                nds.NdsSum.HasValue
                ? MoneySum.InRubles(nds.NdsSum.Value)
                : default;
        }
    }
}
