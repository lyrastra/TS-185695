using System;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    public class SelfCostSourceRequestDto
    {
        /// <summary>
        /// Дата, С которой нужно получить док-ты (включительно).
        /// </summary>
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// Дата, ДО которой нужно получить док-ты (включительно). 
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Кол-во возвращаемых документов
        /// </summary>
        public int Limit { get; set; } = 1000;

        /// <summary>
        /// Сколько пропустить от начала списка. Используется вместе с Limit для порционной загрузки.
        /// </summary>
        public int Offset { get; set; } = 0;
    }
}
