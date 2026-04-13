using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Products
{
    public class ProductAutoCompleteDto
    {
        public long ProductId { get; set; }

        public string Name { get; set; }

        public string Article { get; set; }

        public decimal SalePrice { get; set; }

        public string UnitOfMeasurement { get; set; }

        public string UnitCode { get; set; }

        public int Nds { get; set; }

        public NdsPositionType NdsPositionType { get; set; }

        public StockProductTypeEnum Type { get; set; }

        public long NomenclatureId { get; set; }

        public List<ProductCountDto> StockProductCounts { get; set; }
    }
}