using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction
{
    public class DeductionAccPostingRequest
    {
        public int SettlementAccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public BudgetaryPayerStatus PayerStatus { get; set; }
        public KontragentWithRequisites Contractor { get; set; }
        public string Kbk { get; set; }
    }
}