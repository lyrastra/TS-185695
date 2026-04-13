using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class SickListAllowanceExDto
    {
        public DateTime? DocumentSubmittingDate { get; set; }
        
        public EmploymentContractAllowanceExDto EmploymentContract { get; set; }

        public RadiationReasonType? RadiationExposureCode { get; set; }

        public CareForRelativeDto CareForRelative { get; set; }
    }
}