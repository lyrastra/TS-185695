using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class TariffDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductPlatform { get; set; }

        public string ProductGroup { get; set; }

        public bool IsMobile { get; set; }

        public bool IsProfOutsource { get; set; }

        public string Permissions { get; set; }

        public ISet<AccessRule> AccessRules { get; set; }
    }
}