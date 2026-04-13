using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    /// <summary>
    /// Запрос на получение списка товаров, для которых есть бухсправки, удовлетворяющие определённым критериям
    /// </summary>
    public class AccDocumentsProductIdsListRequestDto
    {
        /// <summary>
        /// Дата для критерия поиска справок
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Список продуктов-кандидатов, поиск ведётся среди них
        /// </summary>
        public List<long> ProductIds { get; set; }
    }
}
