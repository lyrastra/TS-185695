using System;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateIncomingOperationRequestDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string PaymentOrderNumber { get; set; }
        public int SettlementAccountId { get; set; }
        public string ContractorInn { get; set; }
        public string ContractorSettlementAccount { get; set; }
        public string DestinationDescription { get; set; }
    }
}