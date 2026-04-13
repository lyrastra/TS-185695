using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.Products
{
    public class BarcodesDto
    {
        public long ProductId { get; set; }

        public List<string> Barcodes { get; set; }
    }
}
