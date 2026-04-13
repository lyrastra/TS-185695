using Moedelo.Common.Enums.Enums.Money;
using System;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class OperationForDuplicateDetectionDto
    {
        public Guid Guid { get; set; }
        public MoneyDirection Direction { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Number { get; set; }
        public string ContractorInn { get; set; }
        public string ContractorSettlementAccount { get; set; }
        public string Description { get; set; }
        public int OperationType { get; set; }
    }
}