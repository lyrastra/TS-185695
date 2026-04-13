using Moedelo.Common.Enums.Enums.Documents;
using System;

namespace Moedelo.Docs.Dto.Ukd
{
    public class UkdItemDto
    {
        public int Id { get; set; }
        public long UkdDocumentBaseId { get; set; }
        public int FirmId { get; set; }
        public string Name { get; set; }
        public long? ProductId { get; set; }
        public string Unit { get; set; }
        public decimal CountBefore { get; set; }
        public decimal? CountAfter  { get; set; }
        public decimal PriceBefore  { get; set; }
        public decimal? PriceAfter  { get; set; }
        public NdsType NdsType { get; set; }
        public decimal SumNdsBefore { get; set; }
        public decimal? SumNdsAfter { get; set; }
        public decimal SumWithoutNdsBefore { get; set; }
        public decimal? SumWithoutNdsAfter { get; set; }
        public decimal SumWithNdsBefore { get; set; }
        public decimal? SumWithNdsAfter { get; set; }
        public int? ActivityAccountCode { get; set; }

        /// <summary>
        /// Прослеживание товаров
        /// </summary>
        public UkdProductTraceDto[] ProductTrace { get; set; } = Array.Empty<UkdProductTraceDto>();
    }
}