using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.TsWizard;

namespace Moedelo.Edm.Dto.TsWizard
{
    public class DocflowDecisionDto
    {
        /// <summary>
        /// Тип решения
        /// </summary>
        public DocflowInfoItemDecisionType Type { get; set; }

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
