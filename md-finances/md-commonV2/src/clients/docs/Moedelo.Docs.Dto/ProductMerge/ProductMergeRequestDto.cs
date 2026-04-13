using System.Collections.Generic;

namespace Moedelo.Docs.Dto.ProductMerge
{
    public class ProductMergeRequestDto
    {
        /// <summary>
        /// ID главного товара
        /// </summary>
        public long PrimaryProductId { get; set; }

        /// <summary>
        /// Перечень ID присоединяемых товаров
        /// </summary>
        public IReadOnlyCollection<long> SecondaryProductIds { get; set; }
    }
}