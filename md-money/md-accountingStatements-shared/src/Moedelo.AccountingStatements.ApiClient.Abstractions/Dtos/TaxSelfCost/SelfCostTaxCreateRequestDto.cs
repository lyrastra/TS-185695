using System;
using System.Collections.Generic;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.SelfCostTax
{
    public class SelfCostTaxCreateRequestDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Описание документа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Базовый идентификатор связанного документа
        /// </summary>
        public long LinkedDocumentBaseId { get; set; }

        /// <summary>
        /// Налоговые проводки
        /// </summary>
        public IReadOnlyCollection<SelfCostTaxPostingDto> TaxPostings { get; set; }
    }
}
