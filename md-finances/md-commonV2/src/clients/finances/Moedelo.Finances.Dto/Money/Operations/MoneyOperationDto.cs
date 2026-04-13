using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using System;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Finances.Dto.Money.Operations
{
    public class MoneyOperationDto
    {
        public long Id { get; set; }
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public int? KontragentId { get; set; }
        public string KontragentName { get; set; }
        public OperationType OperationType { get; set; }
        public OperationState OperationState { get; set; }
        public DocumentStatus PaidStatus { get; set; }
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal Sum { get; set; }

        public string Description { get; set; }
        public int? SettlementAccountId { get; set; }
    }
}
