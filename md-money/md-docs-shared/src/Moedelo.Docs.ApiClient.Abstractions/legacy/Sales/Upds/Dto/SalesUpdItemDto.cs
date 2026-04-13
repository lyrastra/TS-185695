using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto
{
    public class SalesUpdItemDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Count { get; set; }
        
        public NdsType NdsType { get; set; }
        
        public decimal NdsSum { get; set; }
        
        public decimal SumWithoutNds { get; set; }
        
        public decimal SumWithNds { get; set; }
        
        public long? StockProductId { get; set; }
        
        public ItemType ItemType { get; set; }
        
        public decimal Price { get; set; }
        
        public string Unit { get; set; }

        public int? NdsDeclarationSection7CodeId { get; set; }

        public bool IsCustomSums { get; set; }

        public int? NdsOperationType { get; set; }

        public string Country { get; set; }

        public string Declaration { get; set; }
        
        /// <summary>
        /// Маркировочные коды
        /// </summary>
        public List<ProductLabelRestDto> Labels { get; set; } = new List<ProductLabelRestDto>();

        /// <summary>
        /// Прослеживание товаров
        /// </summary>
        public List<SalesUpdProductTraceDto> ProductTrace { get; set; } = new List<SalesUpdProductTraceDto>();
    }
}
