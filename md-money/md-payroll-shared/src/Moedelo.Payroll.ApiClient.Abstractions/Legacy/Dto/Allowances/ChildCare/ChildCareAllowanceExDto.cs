using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareAllowanceExDto
    {
        public ChildCareAllowanceExDto()
        {
            Child = new ChildAllowanceExDto();
        }
        
        public string OrderNumber { get; set; }
        
        public DateTime? OrderDate { get; set; }
        
        public ChildCareReplacedOrderExDto ReplacedOrder { get; set; }
        
        public ChildAllowanceExDto Child { get; set; }
        
        public int? ChildByAccount { get; set; }
        
        public string BirthCertificateNumber { get; set; }
        
        public DateTime? BirthCertificateDate { get; set; }
        
        public bool HasBirthCertificateOfAnotherChild { get; set; }
        
        public bool HasDeathCertificateOfAnotherChild { get; set; }
         
        public bool DeprivationMaternity { get; set; }
        
        public bool CareOfSeveralChildren { get; set; }

        public List<ChildCareNonReceiptCertificateExDto> NonReceiptCertificates { get; set;}
        
        public ChildCareAdoptionDecisionExDto AdoptionDecision { get; set; }
        
        public ChildCarePaymentsTerminationExDto PaymentsTermination { get; set; }
        
        public ChildCareLivingConditionsExDto LivingConditions { get; set; }
    }
}