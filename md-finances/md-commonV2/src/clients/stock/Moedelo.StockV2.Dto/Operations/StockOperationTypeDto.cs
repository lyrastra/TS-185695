using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Operations
{
    public class StockOperationTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public StockOperationTypeEnum Code { get; set; }

        public StockOperationParentTypeEnum ParentCode { get; set; }

        public bool? IsPositive { get; set; }

        public bool NeedCostCalculation => IsPositive == false;
    }
}
