using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Blancbank
{
    /// <summary>Бланкбанк не отдает БИК</summary>
    public class AccountDto
    {
        /// <summary>номер счета</summary>
        public string SettlementAccount { get; set; }

        /// <summary>БИК</summary>
        public string Bik { get; set; }

        /// <summary>Идентификатор (guid) счета в банке</summary>
        public string AccountId { get; set; }

        /// <summary>Статус счета ex. Enabled</summary>
        public string Status { get; set; }

        public DateTime StatusUpdate { get; set; }

        /// <summary>Тип валюты</summary>
        public string Currency { get; set; }

        /// <summary>Номер транзитного счета для валюты</summary>
        public string TransitAccountNumber { get; set; }

        /// <summary>ex. Business</summary>
        public string AccountType { get; set; }
    }
}
