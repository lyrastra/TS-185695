using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareAdoptionDecisionExDto
    {
        public ChildCareAdoptionDecisionType? DecisionType { get; set; }
        
        public DateTime? DecisionDate { get; set; }
        
        public string DecisionNumber { get; set; }
    }
}