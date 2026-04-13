using Moedelo.AccPostings.Enums;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Api.Models.AccPostings
{
    public class AccPostingDto
    {
        /// <summary>
        /// Дата
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Код счета по дебету
        /// </summary>
        public SyntheticAccountCode DebitCode { get; set; }

        /// <summary>
        /// Номер счета по дебету
        /// </summary>
        public string DebitNumber => DebitCode.GetAccountDisplayName();

        /// <summary>
        /// Код счета по кредиту
        /// </summary>
        public SyntheticAccountCode CreditCode { get; set; }

        /// <summary>
        /// Номер счета по кредиту
        /// </summary>
        public string CreditNumber => CreditCode.GetAccountDisplayName();

        /// <summary>
        /// Список дебетовых субконто
        /// </summary>
        public IReadOnlyCollection<SubcontoDto> DebitSubconto { get; set; }

        /// <summary>
        /// Список кредитовых субконто
        /// </summary>
        public IReadOnlyCollection<SubcontoDto> CreditSubconto { get; set; }

        /// <summary>
        /// Описание проводки
        /// </summary>
        public string Description { get; set; }
    }
}
