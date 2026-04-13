using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.TaxPostings.Enums;
using Newtonsoft.Json;
using System;

namespace Moedelo.Money.Providing.Api.Models.TaxPostings
{
    public class OsnoTaxPostingDto : ITaxPostingDto
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

        /// <summary>
        /// Источник дохода/расхода (только для ОСНО)
        /// </summary>
        public OsnoTransferType Type { get; set; }

        /// <summary>
        /// Вид дохода/расхода (только для ОСНО)
        /// </summary>
        public OsnoTransferKind Kind { get; set; }

        /// <summary>
        /// Тип нормируемого расхода (только для ОСНО)
        /// </summary>
        public NormalizedCostType NormalizedCostType { get; set; }
    }
}
