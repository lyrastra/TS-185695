using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.Patent
{
    public class PatentTaxPosting
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
        /// Описание проводки
        /// </summary>
        public string Description { get; set; }

        public IReadOnlyCollection<long> RelatedDocumentBaseIds { get; set; }
    }
}
