using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.PriceLists
{
    public class PriceListInfoCollectionDto
    {
        public PriceListInfoCollectionDto()
        {
        }

        public PriceListInfoCollectionDto(IList<PriceListInfoDto> items, int totalCount)
        {
            ResourceList = items;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Список прайс-листов (без позиций товаров)
        /// </summary>
        public IList<PriceListInfoDto> ResourceList { get; set; } = new List<PriceListInfoDto>();

        /// <summary>
        /// Общее количество парайс-листов
        /// </summary>
        public int TotalCount { get; set; }
    }
}