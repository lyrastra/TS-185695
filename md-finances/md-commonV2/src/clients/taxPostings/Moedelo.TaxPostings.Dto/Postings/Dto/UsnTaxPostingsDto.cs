using Moedelo.Common.Enums.Enums.TaxPostings;
using System;

namespace Moedelo.TaxPostings.Dto.Postings.Dto
{
    public class UsnTaxPostingDto : ITaxPostingDto
    {
        /// <summary>
        /// Дата проводки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма проводки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Направление движения денег (приход/расход)
        /// </summary>
        public TaxPostingsDirection Direction { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
