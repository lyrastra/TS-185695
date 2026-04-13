using System.Collections.Generic;
using Moedelo.StockV2.Dto.Operations;

namespace Moedelo.StockV2.Dto.SelfCost
{
    public class SelfCostResultDto
    {
        public List<StockOperationDto> Operations { get; set; }
        
        public List<SelfCostDetalizationDto> SelfCostDetalizations { get; set; }
    }
}