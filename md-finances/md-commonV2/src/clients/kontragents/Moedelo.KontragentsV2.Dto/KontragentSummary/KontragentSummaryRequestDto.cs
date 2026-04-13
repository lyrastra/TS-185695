using System;
using System.Collections.Generic;

namespace Moedelo.KontragentsV2.Dto.KontragentSummary
{
    public class KontragentSummaryRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        public int Count { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}