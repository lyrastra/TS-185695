using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using System;
using Moedelo.Contracts.Enums;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    public class ContractDto
    {
        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата договора
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер договора
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Вид договора
        /// </summary>
        public ContractKind Kind { get; set; }
    }
}
