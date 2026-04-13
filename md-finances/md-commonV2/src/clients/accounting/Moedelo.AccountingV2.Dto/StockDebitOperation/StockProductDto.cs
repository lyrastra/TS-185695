using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.AccountingV2.Dto.StockDebitOperation
{
    public class StockProductDto
    { 
        public long Id { get; set; }

        public long NomenclatureId { get; set; }

        public long SyntheticAccountId { get; set; }

        public long? SubcontoId { get; set; }

        public int NDS { get; set; }

        public virtual NdsPositionType NdsPositionType { get; set; }

        public int SyntheticAccountCode { get; set; }

        public decimal SalePrice { get; set; }

        public decimal PurchasePrice { get; set; }

        public string ProductName { get; set; }

        public string Article { get; set; }

        public string UnitOfMeasurement { get; set; }

        public StockProductTypeEnum Type { get; set; }
    }
}
