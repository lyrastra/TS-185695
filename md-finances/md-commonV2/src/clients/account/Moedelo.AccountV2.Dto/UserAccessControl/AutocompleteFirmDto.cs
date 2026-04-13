using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.UserAccessControl
{
    public class AutocompleteFirmDto
    {
        public int FirmId { get; set; }
        public string FirmName { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}

