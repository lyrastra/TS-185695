namespace Moedelo.StockV2.Dto.Operations
{
    /// <summary>
    /// Обновление себестоимости в складской операции
    /// </summary>
    public class SelfCostUpdateRequestDto
    {
        public long OperationOverProductId { get; set; }
        
        public decimal SelfCostSum { get; set; }
    }
}