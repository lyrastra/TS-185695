using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.TsWizard;

namespace Moedelo.Edm.Dto.TsWizard
{
    public class UnreadDocflowDecisionDto
    {
        /// <summary>
        /// Тип решения
        /// </summary>
        public UnreadDocflowsDecisionType Type { get; set; }

        /// <summary>
        /// Советы оператору
        /// </summary>
        public List<HintDto> Hints { get; set; }

        /// <summary>
        /// Больше значение - приоритетнее решение
        /// </summary>
        public int Priority { get; set; }

    }
}
