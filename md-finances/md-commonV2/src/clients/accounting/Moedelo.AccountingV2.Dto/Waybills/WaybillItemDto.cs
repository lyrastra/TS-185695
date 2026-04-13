using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Waybills
{
    public class WaybillItemDto
    {
        public int Id { get; set; }
        public decimal Count { get; set; }
        public decimal SumWithoutNds { get; set; }
        public decimal SumWithNds { get; set; }
        public bool IsCustomSums { get; set; }
        public decimal NdsSum { get; set; }
        public long? StockProductId { get; set; }
        public NdsType NdsType { get; set; }
        public decimal? DiscountRate { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int? NdsDeclarationSection7CodeId { get; set; }
        public decimal RealCount { get; set; }
    }
}