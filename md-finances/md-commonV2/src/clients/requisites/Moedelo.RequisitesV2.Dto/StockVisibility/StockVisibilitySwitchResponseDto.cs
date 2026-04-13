using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.RequisitesV2.Dto.Stock
{
    public class StockVisibilitySwitchResponseDto
    {
        public StockVisibilitySwitchStatus Status { get; set; }
        public string Description => Status.GetDescription(); 
    }
}