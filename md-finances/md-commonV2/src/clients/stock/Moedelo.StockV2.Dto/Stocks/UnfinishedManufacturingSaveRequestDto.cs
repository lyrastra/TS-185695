using System;
using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.Stocks
{
    public class UnfinishedManufacturingSaveRequestDto
    {
        public DateTime Date { get; set; }
        public List<UnfinishedManufacturingItemDto> Items { get; set; }  
    }
}