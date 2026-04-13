using System;

namespace Moedelo.BankIntegrationsV2.Dto.UralsibSso
{
    public class AccountInfoDto
    {
        public string AccountCurrencyLetterCode { get; set; }

        public string AccountType { get; set; }

        public string BankBIC { get; set; }

        public string BankName { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string CorrNumber { get; set; }

        public LegalEntityDto LegalEntity { get; set; }

        /// <summary> Поле из DB moedelo.Bank.Id, заполняется в ходе сверки р/сч </summary>
        public int MoedeloBankId { get; set; }
    }
}
