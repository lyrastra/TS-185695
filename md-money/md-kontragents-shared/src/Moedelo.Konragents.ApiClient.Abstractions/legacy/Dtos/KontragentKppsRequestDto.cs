using System;
using System.Collections.Generic;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public sealed class KontragentKppsRequestDto
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