using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentSaveRequest
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public long CashId { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveModel> SubPayments { get; set; }

        public string Recipient { get; set; }
        public string Destination { get; set; }

        public bool ProvideInAccounting { get; set; }
        public ProvidePostingType PostingsAndTaxMode { get; set; }
    }
}
