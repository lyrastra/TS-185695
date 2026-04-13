using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.AccountingV2.Dto.StockDebitOperation
{
    public class StockOperationOverProductDto
    {
        public long Id { get; set; }

        public long StockId { get; set; }

        public decimal Count { get; set; }

        public decimal Sum { get; set; }

        public bool IsDeleted { get; set; }

        public bool? IsOffbalance { get; set; }

        public StockOperationTypeEnum TypeCode { get; set; }

        public StockOperationParentTypeEnum TypeParentCode { get; set; }

        public StockProductDto Product { get; set; }
    }
}
