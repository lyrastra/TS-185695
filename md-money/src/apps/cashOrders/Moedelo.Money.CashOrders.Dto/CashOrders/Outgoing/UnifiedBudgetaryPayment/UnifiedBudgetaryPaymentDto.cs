using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public long CashId { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }

        public string Recipient { get; set; }
        public string Destination { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> SubPayments { get; set; }

        public bool ProvideInAccounting { get; set; }
        public ProvidePostingType PostingsAndTaxMode { get; set; }
    }
}
