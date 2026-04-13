namespace Moedelo.StockV2.Dto.Products
{
    public class ProductCountDto
    {
        public long? StockId { get; set; }
        public string StockName { get; set; }
        public decimal Count { get; set; }
    }
}