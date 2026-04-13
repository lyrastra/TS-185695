using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using System;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    public class BillLinkDto
    {
        /// <summary>
        /// Идентификатор счёта
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата счёта
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер счёта
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма счёта
        /// </summary>
        [SumValue]
        public decimal DocumentSum { get; set; }

        /// <summary>
        /// Учитываемая сумма
        /// </summary>
        [SumValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма, оплаченная в другом платежном документе
        /// </summary>
        [SumValue]
        public decimal PaidSum { get; set; }
    }
}
