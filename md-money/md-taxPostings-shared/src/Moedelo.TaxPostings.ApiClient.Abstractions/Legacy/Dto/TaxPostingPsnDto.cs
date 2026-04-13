using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto
{
    public class TaxPostingPsnDto
    {
        /// <summary>
        /// Идентификатор проводки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// BaseId документа, по которому создана проводка
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// Дата проводки
        /// </summary>
        public DateTime PostingDate { get; set; }

        /// <summary>
        /// Смма проводки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Описание проводки
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Список связанных документов
        /// </summary>
        public IReadOnlyCollection<long> RelatedDocumentBaseIds { get; set; }
    }
}
