using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.CommonV2.EventBus.Stocks
{
    public class ChangeProductSubtypeEvent
    {
        public int FirmId { get; set; }
        
        public int UserId { get; set; }
        
        public long ProductId { get; set; }
        
        public StockProductSubTypeEnum FromValue { get; set; }
        
        public StockProductSubTypeEnum ToValue { get; set; }
    }
}