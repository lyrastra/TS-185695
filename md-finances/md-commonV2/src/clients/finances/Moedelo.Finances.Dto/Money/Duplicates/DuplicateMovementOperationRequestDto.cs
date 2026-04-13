using System;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateMovementOperationRequestDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string PaymentOrderNumber { get; set; }
        public int? SettlementAccountFromId { get; set; }
        public int? SettlementAccountToId { get; set; }
        public bool DirectionIncoming { get; set; } = true;
        public string DestinationDescription { get; set; }
    }
}