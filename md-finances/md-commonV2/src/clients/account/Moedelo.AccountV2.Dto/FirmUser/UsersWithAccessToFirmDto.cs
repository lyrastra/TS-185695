using Moedelo.AccountV2.Dto.UserAccessControl;

namespace Moedelo.AccountV2.Dto.FirmUser
{
    public class UsersWithAccessToFirmDto
    {
        public bool CanAddUsers { get; set; }

        public ListWithTotalCount<FirmUserAccessDto> UserAccesses { get; set; }
    }

    public class FirmUserAccessDto
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public FirmRoleDto Role { get; set; }
        public bool CanEdit { get; set; }
        public bool CanChangeRole { get; set; }
        public bool CanDelete { get; set; }
        public bool IsProfOutsourceUser { get; set; }
        public bool IsLegalUser { get; set; }
        public bool IsAdmin { get; set; }
        public int AccountId { get; set; }
    }
}
