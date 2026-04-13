using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.AccountV2.Dto.UserAccountPermission
{
    public class UserAccountPermissionDto
    {
        public int UserId { get; set; }
        public AccountPermission Permission { get; set; }
    }
}