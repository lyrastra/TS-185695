using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.StockV2.Dto.Products
{
    public class ProductForEvotorDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Article { get; set; }

        public decimal SalePrice { get; set; }

        public string UnitOfMeasurement { get; set; }

        public NdsPositionType NdsPositionType { get; set; }

        public int Nds { get; set; }
    }
}