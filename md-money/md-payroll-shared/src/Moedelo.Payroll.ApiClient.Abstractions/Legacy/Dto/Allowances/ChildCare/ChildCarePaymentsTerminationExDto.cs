using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCarePaymentsTerminationExDto
    {
        public DateTime? TerminationDate { get; set; }
        
        public DateTime? TerminationOrderDate { get; set; }
        
        public string TerminationOrderNumber { get; set; }
        
        public ChildCarePaymentsTerminationOrderType? TerminationOrderType { get; set; }
    }
}