using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Balance
{
    public class ProductBalanceInfoDto
    {
        public int Id { get; set; }

        public long ProductId { get; set; }

        public long NomenclatureId { get; set; }

        public long StockId { get; set; }

        public int Nds { get; set; }

        public NdsPositionType NdsPositionType { get; set; }

        public string ProductName { get; set; }

        public string ProductArticle { get; set; }

        public string ProductUnit { get; set; }

        public string ProductUnitCode { get; set; }

        public string NomenclatureName { get; set; }

        public string StockName { get; set; }

        public decimal AtBeginning { get; set; }

        public decimal Arrived { get; set; }

        public decimal Retired { get; set; }

        public decimal Balance { get; set; }

        public decimal SalePrice { get; set; }

        public StockProductTypeEnum ProductType { get; set; }

        public long SyntheticAccountTypeId { get; set; }

        public int PrimaryDocumentItemType { get; set; }

        public int Rating { get; set; }

        public bool IsMediationProduct { get; set; }

        public string DivisionName { get; set; }

        public int DivisionId { get; set; }
    }
}
