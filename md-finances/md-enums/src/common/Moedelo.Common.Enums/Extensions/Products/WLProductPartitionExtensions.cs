using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.Products;

namespace Moedelo.Common.Enums.Extensions.Products
{
    public static class WLProductPartitionExtensions
    {
        public static WLProductPartition GetProductPartition(this ISet<AccessRule> rules)
        {
            if (rules.Contains(AccessRule.OfficeTariffGroup))
            {
                return WLProductPartition.Buro;
            }

            if (rules.Contains(AccessRule.SberbankTariff))
            {
                return WLProductPartition.Sberbank;
            }

            if (rules.Contains(AccessRule.SkbBankWlTariff))
            {
                return WLProductPartition.SkbBank;
            }

            return WLProductPartition.DefaultBiz;
        }

        public static WLProductPartition GetProductPartition(this IReadOnlyCollection<AccessRule> rules)
        {
            if (rules.Contains(AccessRule.OfficeTariffGroup))
            {
                return WLProductPartition.Buro;
            }

            if (rules.Contains(AccessRule.SberbankTariff))
            {
                return WLProductPartition.Sberbank;
            }

            if (rules.Contains(AccessRule.SkbBankWlTariff))
            {
                return WLProductPartition.SkbBank;
            }

            return WLProductPartition.DefaultBiz;
        }
    }
}
