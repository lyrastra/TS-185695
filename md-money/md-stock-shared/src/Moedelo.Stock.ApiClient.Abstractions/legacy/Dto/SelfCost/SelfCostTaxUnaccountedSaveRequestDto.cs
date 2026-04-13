namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.SelfCost
{
    /// <summary>
    /// Неучтенная в НУ сумма себестоимости (сохранение)
    /// </summary>
    public class SelfCostTaxUnaccountedSaveRequestDto
    {
        /// <summary>
        /// Идентификатор операции над товаром
        /// </summary>
        public long OverProductId { get; set; }
        
        /// <summary>
        /// Неучтенная сумма
        /// </summary>
        public decimal Sum { get; set; }
    }
}