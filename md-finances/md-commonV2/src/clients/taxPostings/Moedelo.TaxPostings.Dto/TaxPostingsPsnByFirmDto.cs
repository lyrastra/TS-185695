using System.Collections.Generic;

namespace Moedelo.TaxPostings.Dto
{
    public class TaxPostingsPsnByFirmDto
    {
        public int FirmId { get; set; }
        public IReadOnlyCollection<TaxPostingPsnDto> Postings { get; set; }
    }
}