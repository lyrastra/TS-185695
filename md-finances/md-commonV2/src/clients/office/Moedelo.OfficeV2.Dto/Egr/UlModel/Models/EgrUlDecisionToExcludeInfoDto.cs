using System;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    public class EgrUlDecisionToExcludeInfoDto
    {
        /// <summary>
        /// Дата решения
        /// </summary>
        public DateTime DecisionDate { get; set; }

        /// <summary>
        /// Номер решения
        /// </summary>
        public string DecisionNumber { get; set; }

        /// <summary>
        /// Дата публикации решения
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Номер журнала, в котором опубликовано решение
        /// </summary>
        public string JournalNumber { get; set; }
    }
}