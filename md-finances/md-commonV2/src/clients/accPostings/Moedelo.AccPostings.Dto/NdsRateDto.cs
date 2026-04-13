using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.AccPostings.Dto
{
    public class NdsRateDto
    {
        public long Id { get; set; }

        public long SubcontoId { get; set; }

        public string Name { get; set; }

        public NdsTypes NdsType { get; set; }

        public decimal Rate { get; set; }
    }
}