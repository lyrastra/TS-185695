using System;
using System.Collections.Generic;

namespace Moedelo.KontragentsV2.Dto
{
    public class KontragentKppsRequestDto
    {
        /// <summary>
        /// Идентификаторы контрагентов
        /// </summary>
        public IReadOnlyCollection<int> KontragentIds { get; set; }
        
        /// <summary>
        /// Отфильтровать КПП, актуальные на дату (опционально)
        /// </summary>
        public DateTime? ActualOnDate { get; set; }
    }
}