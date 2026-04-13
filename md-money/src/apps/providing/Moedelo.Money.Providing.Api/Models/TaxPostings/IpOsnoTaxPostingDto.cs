using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.TaxPostings.Enums;
using Newtonsoft.Json;
using System;

namespace Moedelo.Money.Providing.Api.Models.TaxPostings
{
    public class IpOsnoTaxPostingDto : ITaxPostingDto
    {
        /// <summary>
        /// Дата проводки
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма проводки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Направление движения денег (приход/расход)
        /// </summary>
        public TaxPostingDirection Direction { get; set; }
    }
}
