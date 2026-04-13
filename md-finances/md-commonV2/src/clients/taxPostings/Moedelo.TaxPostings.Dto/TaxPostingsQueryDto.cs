using System.Collections.Generic;

namespace Moedelo.TaxPostings.Dto
{
    public class TaxPostingsQueryDto : PeriodRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }
    }
}