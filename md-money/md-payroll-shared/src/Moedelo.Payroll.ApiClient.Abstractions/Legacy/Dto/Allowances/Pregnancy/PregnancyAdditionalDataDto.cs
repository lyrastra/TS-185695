using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy
{
    public class PregnancyAdditionalDataDto
    {
        public SheetType SheetOfDisabilityType { get; set; }

        public DateTime SheetOfDisabilityDate { get; set; }

        public bool IsDuplicate { get; set; }

        public MedicalOrgAllowanceDto MedicalOrganization { get; set; }

        public PlaceOfWorkType? PlaceOfWork { get; set; }

        public string SheetNumber { get; set; }

        public DateTime? PresumedDateOfBirth { get; set; }

        public EarlyPregnancyRegistrationType? EarlyPregnancyRegistration { get; set; }

        public List<WorkExemptionAllowanceDto> WorkExemptions { get; set; }

        public DateTime? StartOfWorkingDate { get; set; }

        public ConditionOfCalculation FirstConditionOfCalculation { get; set; }

        public ConditionOfCalculation SecondConditionOfCalculation { get; set; }

        public ConditionOfCalculation ThirdConditionOfCalculation { get; set; }

        public string CauseOfDisabilityMainCode { get; set; }

        public string CauseOfDisabilityAdditionalCode { get; set; }
    }
}
