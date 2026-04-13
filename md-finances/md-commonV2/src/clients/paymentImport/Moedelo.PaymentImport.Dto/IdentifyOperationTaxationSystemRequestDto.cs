using Moedelo.Common.Enums.Enums.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using System;

namespace Moedelo.PaymentImport.Dto
{
    public class IdentifyOperationTaxationSystemRequestDto
    {
        public Guid Guid { get; set; }

        public DateTime Date { get; set; }

        public int ContractorId { get; set; }

        public string PaymentPurpose { get; set; }

        public OperationType? OperationType { get; set; }
    }
}
