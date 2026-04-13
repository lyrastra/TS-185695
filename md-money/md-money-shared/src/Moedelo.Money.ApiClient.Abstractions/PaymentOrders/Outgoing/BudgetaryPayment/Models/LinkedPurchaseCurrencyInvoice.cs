using System;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models
{
    public class LinkedPurchaseCurrencyInvoiceDto
    {
        /// <summary>
        /// Идентификатор инвойса
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата инвойса
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер инвойса
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма связи с инвойсом
        /// </summary>
        public decimal LinkSum { get; set; }
    }
}