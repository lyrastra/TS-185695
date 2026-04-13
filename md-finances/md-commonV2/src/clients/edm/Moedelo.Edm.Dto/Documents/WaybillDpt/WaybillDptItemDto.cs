using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Edm.Dto.Documents.WaybillDpt
{
    public class WaybillDptItemDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public decimal Count { get; set; }
        public decimal NdsSum { get; set; }
        public NdsType NdsType { get; set; }
        public decimal Price { get; set; }
        public decimal SumWithNds { get; set; }
        public decimal SumWithoutNds { get; set; }
        public string Unit { get; set; }
        public string UnitCode { get; set; }
    }
}
