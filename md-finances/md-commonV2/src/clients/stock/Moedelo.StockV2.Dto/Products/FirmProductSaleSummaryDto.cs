using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.Products
{
    public class FirmProductSaleSummaryDto
    {
        public int FirmId { get; set; }

        public List<ProductSaleSummaryDto> Products { get; set; } 
    }
}
