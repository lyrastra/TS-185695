namespace Moedelo.StockV2.Dto.SelfCost
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

        /// <summary>
        /// По какому товару не учтена сумма (в случае продажи комплекта идентификатор составляющей) 
        /// </summary>
        public long ProductId { get; set; }
    }
}