using System;

namespace Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment
{
    public class CurrencyInvoiceNdsPayment
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }
    }
}