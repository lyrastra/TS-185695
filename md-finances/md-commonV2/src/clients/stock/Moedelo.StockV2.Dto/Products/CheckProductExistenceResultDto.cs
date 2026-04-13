namespace Moedelo.StockV2.Dto.Products
{
    public class CheckProductExistenceResultDto
    {
        public bool IsExist { get; set; }
        public long? ProductId { get; set; }
        public bool? IsBundle { get; set; }
    }
}