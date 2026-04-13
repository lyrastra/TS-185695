using System;
using Moedelo.Finances.Enums;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
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