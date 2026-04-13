using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.AccountV2.Dto.TariffsAndRoles
{
    public class TariffInfoDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductPlatform { get; set; }

        public string ProductGroup { get; set; }
        
        public ISet<AccessRule> AccessRules { get; set; }
    }
}