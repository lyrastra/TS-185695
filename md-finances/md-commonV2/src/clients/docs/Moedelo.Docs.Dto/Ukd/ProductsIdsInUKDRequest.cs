using System.Collections.Generic;

namespace Moedelo.Docs.Dto.Ukd
{
    /// <summary>
    /// Запрос для получения списка ID, использующихся в УКД
    /// </summary>
    public class ProductsIdsInUKDRequest
    {
        /// <summary>
        /// Список продуктов, среди которых нужно вести поиск.
        /// </summary>
        public IReadOnlyCollection<long> ProductIds;
    }
}
