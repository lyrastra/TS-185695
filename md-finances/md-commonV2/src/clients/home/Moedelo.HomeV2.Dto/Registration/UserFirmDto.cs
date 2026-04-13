using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.HomeV2.Dto.Registration
{
    public class UserFirmDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Fio { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ISet<FirmRoleDto> FirmRoles { get; set; }

        public ISet<AccountPermission> AccountPermissions { get; set; }

        public bool IsMainUser { get; set; }

        public bool IsLegalUser { get; set; }

        public bool IsAdmin { get; set; }

        public UserFirmType UserFirmType { get; set; }

        public bool DontAttachToProfOutsource { get; set; }

        public bool IsProfOutsourceUser { get; set; }

        public bool IsEditable { get; set; }

        public bool IsAdminable { get; set; }

        public bool FirmRolesIsEditable { get; set; }

        public int FirmId { get; set; }

        public int RoleId { get; set; }
    }
}