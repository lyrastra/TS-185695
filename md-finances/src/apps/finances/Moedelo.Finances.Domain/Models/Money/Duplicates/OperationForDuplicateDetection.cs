using Moedelo.Common.Enums.Enums.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using System;

namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class OperationForDuplicateDetection
    {
        public Guid Guid { get; set; }
        public MoneyDirection Direction { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Number { get; set; }
        public string ContractorInn { get; set; }
        public string ContractorSettlementAccount { get; set; }
        public string Description { get; set; }
        public OperationType OperationType { get; set; }
    }
}