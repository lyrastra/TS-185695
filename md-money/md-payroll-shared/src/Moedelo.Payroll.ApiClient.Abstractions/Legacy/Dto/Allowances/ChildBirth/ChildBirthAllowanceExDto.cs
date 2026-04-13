using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth
{
    public class ChildBirthAllowanceExDto
    {
        public ChildBirthAllowanceExDto()
        {
            Child = new ChildAllowanceExDto();
        }
        
        public ChildBirthAllowanceReferenceType? ReferenceType { get; set; }

        public string ReferenceNumber { get; set; }
        
        public DateTime? ReferenceDate { get; set; }
        
        public ChildAllowanceExDto Child { get; set; } 
        
        public int? ChildByAccount { get; set; }
        
        public string BirthCertificateNumber { get; set; }
        
        public DateTime? BirthCertificateDate { get; set; }
    }
}