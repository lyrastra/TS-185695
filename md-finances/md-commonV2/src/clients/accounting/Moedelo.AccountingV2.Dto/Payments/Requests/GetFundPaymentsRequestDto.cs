using System;

namespace Moedelo.AccountingV2.Dto.Payments.Requests
{
    public class GetFundPaymentsRequestDto
    {
        public DateTime? StartDate { get; set; } 
        
        public DateTime? EndDate { get; set; }
        
        public DateTime ChargedStartDate { get; set; }
    }
}