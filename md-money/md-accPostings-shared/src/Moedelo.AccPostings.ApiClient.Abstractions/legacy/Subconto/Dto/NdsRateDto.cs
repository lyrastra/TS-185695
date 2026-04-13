using Moedelo.AccPostings.Enums;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto
{
    public class NdsRateDto
    {
        public long Id { get; set; }

        public long SubcontoId { get; set; }

        public string Name { get; set; }

        public NdsRateType NdsType { get; set; }

        public decimal Rate { get; set; }
    }
}
