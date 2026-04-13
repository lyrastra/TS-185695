using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.AccountV2.Dto.Account
{
    public class AccountFirmsDto
    {
        public int AccountId { get; set; }

        public int MainAdminUserId { get; set; }

        public IReadOnlyCollection<int> FirmIds { get; set; }

        public AccountType AccountType { get; set; }
    }
}