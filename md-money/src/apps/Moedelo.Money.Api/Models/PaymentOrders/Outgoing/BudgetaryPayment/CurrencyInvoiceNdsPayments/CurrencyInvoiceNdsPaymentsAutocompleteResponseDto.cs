using System;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments
{
    public class CurrencyInvoiceNdsPaymentsAutocompleteResponseDto
    {
        /// <summary>
        /// BaseId платежа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер платежа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Остаток суммы, не распределенный между прочими инвойсами
        /// </summary>
        public decimal Sum { get; set; }
    }
}