using System;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments
{
    public class CurrencyInvoiceResponseDto
    {
        /// <summary>
        /// Идентификатор инвойса
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата инвойса
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
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