using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.AccountV2.Dto.UserAccessControl
{
    public class UserAccountPermissions
    {
        public int UserId { get; set; }

        public ISet<AccountPermission> Permissions { get; set; }
    }
}