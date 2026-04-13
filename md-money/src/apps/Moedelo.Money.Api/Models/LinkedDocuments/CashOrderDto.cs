using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using System;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    public class CashOrderDto
    {
        /// <summary>
        /// Идентификатор кассового ордера
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата кассового ордера
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер кассового ордера
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма кассового ордера
        /// </summary>
        [SumValue]
        public decimal Sum { get; set; }
    }
}
