using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.Products
{
    /// <summary>
    /// Критерий поиска товаров
    /// </summary>
    public class ProductSearchCriteriaDto
    {
        /// <summary>
        /// Поиск по списку наименований (строгое соответствие)
        /// </summary>
        public IReadOnlyCollection<string> Names { get; set; }
    }
}