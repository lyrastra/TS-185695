using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.Role
{
    public class FirmRolesDto
    {
        public int FirmId { get; set; }
        public IList<RoleInfo> Roles { get; set; }

        public class RoleInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}

