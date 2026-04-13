using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments
{
    public class CurrencyInvoiceNdsPaymentsAutocompleteResponse
    {
        /// <summary>
        /// BaseId платежа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Остаток суммы, не распределенный между прочими инвойсами
        /// </summary>
        public decimal Sum { get; set; }
    }
}