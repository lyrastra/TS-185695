using System.Collections.Generic;
using Moedelo.Accounts.ApiClient.Enums;

namespace Moedelo.Accounts.Abstractions.Dto
{
    public class AccountFirmsDto
    {
        public int AccountId { get; set; }

        public int MainAdminUserId { get; set; }

        public IReadOnlyCollection<int> FirmIds { get; set; }

        public AccountType AccountType { get; set; }
    }
}