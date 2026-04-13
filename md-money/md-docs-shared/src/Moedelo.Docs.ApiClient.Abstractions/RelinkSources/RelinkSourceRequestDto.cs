using System;

namespace Moedelo.Docs.ApiClient.Abstractions.RelinkSources
{
    public class RelinkSourceRequestDto
    {
        /// <summary>
        /// Дата, с которой будут запрошены документы
        /// </summary>
        public DateTime StartDate { get; set; }

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