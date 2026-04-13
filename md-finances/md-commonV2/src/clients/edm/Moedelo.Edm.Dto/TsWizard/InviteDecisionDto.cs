using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.TsWizard;

namespace Moedelo.Edm.Dto.TsWizard
{
    public class InviteDecisionDto
    {
        /// <summary>
        /// Тип решения
        /// </summary>
        public InviteDecisionType Type { get; set; }

        /// <summary>
        /// Советы, связанные с решением. Каждый показывается отдельно
        /// </summary>
        public List<HintDto> Hints { get;  set; }

        /// <summary>
        /// Больше значение - приоритетней этот вариант решения
        /// </summary>
        public int Priority { get;  set; }
    }
}
