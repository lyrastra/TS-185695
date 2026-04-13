using Moedelo.Common.Enums.Enums.TsWizard;
using System.Collections.Generic;

namespace Moedelo.Edm.Dto.TsWizard
{
    public class OrphanedKontragentDecisionDto
    {
        /// <summary>
        /// Тип решения
        /// </summary>
        public OrphanedKontragentDecisionType Type { get; set; }

        /// <summary>
        /// Советы, связанные с решением. Каждый показывается отдельно
        /// </summary>
        public List<HintDto> Hints { get; set; }

        /// <summary>
        /// Больше значение - приоритетней этот вариант решения
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Если решение относится к какому-то локальному контрагенту, то его ID будет здесь
        /// </summary>
        public int? RelatedLocalKontragentId { get; set; }
    }
}
