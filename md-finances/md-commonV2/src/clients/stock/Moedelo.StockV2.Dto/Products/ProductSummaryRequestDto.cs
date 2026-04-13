using System;
using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.Products
{
    public class ProductSummaryRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        /// <summary>
        /// Количество товаров
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Первый день периода
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Последний день периода
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}