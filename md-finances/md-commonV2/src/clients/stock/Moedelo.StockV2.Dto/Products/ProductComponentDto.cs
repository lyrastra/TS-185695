namespace Moedelo.StockV2.Dto.Products
{
    public class ProductComponentDto
    {
        public long ParentProductId { get; set; }

        public long ProductId { get; set; }

        public decimal? Count { get; set; }
        
        public bool IsMain { get; set; }
    }
}