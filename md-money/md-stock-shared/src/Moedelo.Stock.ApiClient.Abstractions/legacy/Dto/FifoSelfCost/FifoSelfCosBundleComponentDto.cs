namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    public class FifoSelfCosBundleComponentDto
    {
        /// <summary>
        /// Идентификатор комплекта
        /// </summary>
        public long BundleId { get; set; }
        
        /// <summary>
        /// Идентификатор составляющей комплекта (товар/материалл)
        /// </summary>
        public long ComponentId { get; set; }
        
        /// <summary>
        /// Количество составляющей в 1 комплекте
        /// </summary>
        public decimal Count { get; set; }
    }
}