using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy
{
    public class PregnancyAllowanceExDto
    {
        public DateTime? DocumentSubmittingDate { get; set; }

        public EarlyPregnancyReferenceDto EarlyPregnancyReference { get; set; }

        public EmploymentContractAllowanceExDto EmploymentContract { get; set; }

        public RadiationReasonType? RadiationExposureCode { get; set; }
    }
}