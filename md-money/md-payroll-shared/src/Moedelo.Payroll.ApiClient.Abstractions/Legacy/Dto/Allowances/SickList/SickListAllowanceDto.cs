using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class SickListAllowanceDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public DateTime? FssStartDate { get; set; }
        
        public DateTime? PaidEndDate { get; set; }

        public decimal RegionalRate { get; set; }

        public int Year1 { get; set; }

        public int Year2 { get; set; }

        public decimal Income1 { get; set; }

        public decimal Income2 { get; set; }

        public int PeriodDaysCount { get; set; }
        
        public string SickListNumber { get; set; }
        
        public decimal PayFss { get; set; }

        public decimal PayOrg { get; set; }

        public InsuranceExperienceAllowanceDto InsuranceExperience { get; set; }

        public SickListAdditionalDataDto AdditionalData { get; set; }

        public DateTime PilotProjectStartDate { get; set; }

        public DateTime? PrimaryStartDate { get; set; }
    }
}