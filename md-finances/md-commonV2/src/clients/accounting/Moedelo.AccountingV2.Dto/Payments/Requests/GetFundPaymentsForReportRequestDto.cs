using System;

namespace Moedelo.AccountingV2.Dto.Payments.Requests
{
    public class GetFundPaymentsForReportRequestDto
    {
        public DateTime? StartDate { get; set; } 
        
        public DateTime? EndDate { get; set; }
    }
}