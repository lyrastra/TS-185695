using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.Egr.Search
{
    /// <summary>
    /// Реквизиты поиска контрагентов
    /// </summary>
    public class SearchEgrRequestDto
    {
        /// <summary>
        /// Запрос
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Кол-во
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Пропустить
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Взять
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Список регионов
        /// </summary>
        public List<short> Regions { get; set; }

        /// <summary>
        /// Список отраслей
        /// </summary>
        public List<int> Sections { get; set; }
    }
}
