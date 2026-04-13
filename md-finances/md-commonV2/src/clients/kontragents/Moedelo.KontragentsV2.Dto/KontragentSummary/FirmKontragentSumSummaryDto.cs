using System.Collections.Generic;

namespace Moedelo.KontragentsV2.Dto.KontragentSummary
{
    public class FirmKontragentSumSummaryDto
    {
        public int FirmId { get; set; }

        public List<KontragentSummaryDto> Kontragents { get; set; }
    }
}
