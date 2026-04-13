using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments
{
    public class CurrencyInvoiceLink
    {
        public long DocumentBaseId { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public decimal LinkSum { get; set; }
    }
}