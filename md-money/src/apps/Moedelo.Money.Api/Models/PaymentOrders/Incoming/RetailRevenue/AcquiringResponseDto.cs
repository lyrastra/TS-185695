using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.RetailRevenue
{
    public class AcquiringResponseDto
    {
        /// <summary>
        /// Комиссия (эквайринг)
        /// </summary>
        [SumValue]
        public decimal CommissionSum { get; set; }

        /// <summary>
        /// Дата комиссии
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime CommissionDate { get; set; }
    }
}