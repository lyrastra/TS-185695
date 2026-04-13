using System;
using Moedelo.BankIntegrations.Enums.PointBank;

namespace Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Accounts
{
    public class AccountDto
    {
        public string CustomerCode { get; set; }

        public string AccountId { get; set; }

        public string TransitAccount { get; set; }

        public AccountStatus Status { get; set; }

        public DateTime StatusUpdateDateTime { get; set; }

        public string Currency { get; set; }

        public AccountType AccountType { get; set; }

        public AccountSubType AccountSubType { get; set; }

        public DateTime RegistrationDate { get; set; }

        public AccountDetailDto AccountDetails { get; set; }
    }
}
