using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class SickCareDto
    {
        public int? Years { get; set; }
        
        public int? Months { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string Snils { get; set; }
        
        public CauseOfDisabilityMainCode? CauseOfDisabilityMainCode { get; set; }
        
        public string FamilyMemberFio { get; set; }

        public RelationshipType? RelationshipType { get; set; }
    }
}