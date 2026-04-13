using System;
using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
{
    public class PaymentCoverageSummaryRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IReadOnlyCollection<int> FirmIds { get; set; }
    }
}