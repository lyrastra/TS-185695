using System.Collections.Generic;

namespace Moedelo.TaxPostings.Dto
{
    public class TaxPostingsUsnByFirmDto
    {
        public int FirmId { get; set; }
        public IReadOnlyCollection<TaxPostingUsnDto> Postings { get; set; }
    }
}