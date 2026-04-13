using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    public class FifoSelfCostProductSummariesDto
    {
        /// <summary>
        /// Все товары
        /// </summary>
        public IReadOnlyCollection<FifoSelfCostProductDto> Products { get; set; }
        
        /// <summary>
        /// Составляющие комплектов
        /// </summary>
        public IReadOnlyCollection<FifoSelfCosBundleComponentDto> BundleComponents { get; set; }
        
        /// <summary>
        /// Данные по стоимости и кол-ву приходов/расходов на момент перехода со средней
        /// </summary>
        public IReadOnlyCollection<FifoSelfCostProductBalanceDto> SelfCostBalances { get; set; }
    }
}