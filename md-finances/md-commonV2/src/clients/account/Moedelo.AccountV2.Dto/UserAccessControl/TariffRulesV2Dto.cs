using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.AccountV2.Dto.UserAccessControl
{
    public class TariffRulesV2Dto
    {
        public int TariffId { get; set; }
        public string TariffName { get; set; }
        public ISet<AccessRule> AccessRules { get; set; }
    }
}