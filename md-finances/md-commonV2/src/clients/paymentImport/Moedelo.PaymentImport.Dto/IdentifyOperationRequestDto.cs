using Moedelo.Common.Enums.Enums.Money;
using System;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.PaymentImport.Dto
{
    public class IdentifyOperationRequestDto
    {
        public Guid Guid { get; set; }

        public int ContractorId { get; set; }

        public string PaymentPurpose { get; set; }

        public MoneyDirection Direction { get; set; }

        public SettlementAccountType SettlementAccountType { get; set; }
    }
}
