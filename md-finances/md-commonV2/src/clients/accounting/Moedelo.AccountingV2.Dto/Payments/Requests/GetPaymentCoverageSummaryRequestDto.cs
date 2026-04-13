using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments.Requests
{
    public class GetPaymentCoverageSummaryRequestDto
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public IReadOnlyList<int> FirmIds { get; set; }
    }
}