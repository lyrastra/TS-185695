using System.Collections.Generic;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto
{
    public class TaxPostingUsnByFirmDto
    {
        public int FirmId { get; set; }
        public IReadOnlyCollection<TaxPostingUsnDto> Postings { get; set; }
    }
}