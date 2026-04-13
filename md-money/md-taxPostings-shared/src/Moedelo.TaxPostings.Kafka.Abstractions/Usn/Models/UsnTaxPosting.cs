using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Usn.Models
{
    public class UsnTaxPosting
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
        public TaxPostingDirection Direction { get; set; }

        /// <summary>
        /// Описание проводки
        /// </summary>
        public string Description { get; set; }

        public IReadOnlyCollection<long> RelatedDocumentBaseIds { get; set; }
    }
}
