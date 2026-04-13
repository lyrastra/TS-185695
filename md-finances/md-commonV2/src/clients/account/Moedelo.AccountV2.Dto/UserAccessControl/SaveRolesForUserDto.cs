using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.UserAccessControl
{
    public class SaveRolesForUserDto
    {
        public IList<FirmRoleDto> FirmRoles { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}