using System;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common
{
    public class SelfCostSourceRequestDto
    {
        /// <summary>
        /// Начальная дата закрываемого периода.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Последняя дата закрываемого периода.
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