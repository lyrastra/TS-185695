using Moedelo.Common.Enums.Enums.Bills;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Edm.Dto.Documents.StatementDprr
{
    public class StatementDprrItemDto
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public decimal Price { get; set; }

        public decimal Count { get; set; }

        public string Unit { get; set; }

        public string UnitCode { get; set; }

        public NdsType NdsType { get; set; }

        public decimal SumWithoutNds { get; set; }

        public decimal NdsSum { get; set; }

        public decimal SumWithNds { get; set; }

        public ItemType ItemType => ItemType.Service;

        public decimal NdsPercent => NdsType.GetNdsPercent();
    }
}
