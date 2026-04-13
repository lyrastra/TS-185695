using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUkd.Model
{
    public class UkdItem
    {
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
    }
}