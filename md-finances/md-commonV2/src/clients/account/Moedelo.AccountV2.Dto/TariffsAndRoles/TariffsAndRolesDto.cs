using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.TariffsAndRoles
{
    public class TariffsAndRolesDto
    {
        public List<TariffInfoDto> Tariffs { get; set; }
        
        public List<RoleInfoDto> RoleInfos { get; set; }
    }
}