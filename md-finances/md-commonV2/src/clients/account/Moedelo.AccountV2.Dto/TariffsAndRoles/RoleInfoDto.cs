using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.AccountV2.Dto.TariffsAndRoles
{
    public class RoleInfoDto
    {
        public int Id { get; set; }

        public string RoleCode { get; set; }

        public string Name { get; set; }
        
        public ISet<AccessRule> AccessRules { get; set; }
    }
}