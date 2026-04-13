using System;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateDividendPaymentOperationRequestDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string PaymentOrderNumber { get; set; }
        public int SettlementAccountId { get; set; }
    }
}