namespace Moedelo.StockV2.Dto.SelfCost
{
    public class SelfCostDetalizationDto
    {
        /// <summary>
        /// Приход готовой продукции 
        /// </summary>
        public long OperationOverProductId { get; set; }
        
        /// <summary>
        /// Себестоимость материалов
        /// </summary>
        public decimal Materials { get; set; }
        
        /// <summary>
        /// Себестоимость ЗП сотрудников 
        /// </summary>
        public decimal WorkersSalary { get; set; }
        
        /// <summary>
        /// Себестоимость взносов за сотрудников в фонды 
        /// </summary>
        public decimal Funds { get; set; }

        /// <summary>
        /// Себестоимость всех составляющих
        /// </summary>
        public decimal Total { get; set; }
    }
}