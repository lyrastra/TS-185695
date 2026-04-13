namespace Moedelo.StockV2.Dto.Products
{
    public class ProductBalanceDto
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public int? StockId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Count { get; set; }
        public int NomenclatureId { get; set; }
        public string NomenclatureName { get; set; }
        public string Article { get; set; }
    }
}