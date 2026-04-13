using System.Collections.Generic;

namespace Moedelo.Manufacturing.Dto.ManufacturingReports
{
    /// <summary>
    /// Запрос на получение списка продуктов для отчётов о выпуске готовой продукции
    /// </summary>
    public class ManufacturedProductReportsProductIdsListRequest
    {
        /// <summary>
        /// Продукты-кандидаты, ищем среди них подходящие под условия
        /// </summary>
        public IReadOnlyCollection<long> ProductIds { get; set; }
    }
}
