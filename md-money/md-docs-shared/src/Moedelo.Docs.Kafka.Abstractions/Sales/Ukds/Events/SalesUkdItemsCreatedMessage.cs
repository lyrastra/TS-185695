using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Events
{
    public class SalesUkdItemsCreatedMessage
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
        
        public decimal DiffCount
        {
            get
            {
                var countAfter = CountAfter ?? 0;
                return CountBefore - countAfter;
            }
        }

        public decimal DiffPrice
        {
            get
            {
                var priceAfter = PriceAfter ?? 0;
                return PriceBefore - priceAfter;
            }
        }
        
        public decimal DiffSum
        {
            get
            {
                var sumAfter = SumWithNdsAfter ?? 0;
                return SumWithNdsBefore - sumAfter;
            }
        }
    }
}