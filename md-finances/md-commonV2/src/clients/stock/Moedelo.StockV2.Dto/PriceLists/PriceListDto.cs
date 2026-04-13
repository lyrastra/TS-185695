using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.StockV2.Dto.PriceLists
{
    public class PriceListDto
    {
        /// <summary>
        /// Идентификатор прайс-листа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование прайс-листа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата создания прайс-листа
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Тип Ндс
        /// </summary>
        public NdsPositionType NdsType { get; set; }

        /// <summary>
        /// Список товаров, включённых в прайс-лист
        /// </summary>
        public List<PriceListItemDto> Items { get; set; }
    }
}