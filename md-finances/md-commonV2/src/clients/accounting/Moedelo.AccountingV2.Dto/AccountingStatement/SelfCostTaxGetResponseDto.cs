using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    /// <summary>
    /// Возвращаемая с/с бухсправка с налоговыми проводками.
    /// </summary>
    public class SelfCostTaxGetResponseDto
    {
        /// <summary>
        /// Id бухсправки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Base Id бухсправки
        /// </summary>
        public long BaseId { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Описание документа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Налоговые проводки
        /// </summary>
        public IReadOnlyCollection<SelfCostTaxPostingDto> TaxPostings { get; set; }
    }
}
