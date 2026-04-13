using System;
using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class SickListAdditionalDataDto
    {
        public SheetType SheetOfDisabilityType { get; set; }

        public DateTime? SheetOfDisabilityDate { get; set; }

        public bool IsDuplicate { get; set; }

        public MedicalOrgAllowanceDto MedicalOrganization { get; set; }

        public PlaceOfWorkType? PlaceOfWork { get; set; }

        public string SheetNumber { get; set; }

        public List<WorkExemptionAllowanceDto> WorkExemptions { get; set; } =
            new List<WorkExemptionAllowanceDto>();

        public DateTime? StartOfWorkingDate { get; set; }

        public ConditionOfCalculation FirstConditionOfCalculation { get; set; }

        public ConditionOfCalculation SecondConditionOfCalculation { get; set; }

        public ConditionOfCalculation ThirdConditionOfCalculation { get; set; }

        public CauseOfDisabilityMainCode? CauseOfDisabilityMainCode { get; set; }

        public CauseOfDisabilityAdditionalCode? CauseOfDisabilityAdditionalCode { get; set; }

        public CauseOfDisabilityMainCode? CauseOfDisabilityChangeCode { get; set; }

        public SanatoriumVoucherDto SanatoriumVoucher { get; set; }

        public string SheetProlongNumber { get; set; }

        public BreachRegimeDto BreachRegime { get; set; }

        public InpatientHospitalStayDto InpatientHospitalStay { get; set; }

        public DisabilityGroupType? DisabilityGroupType { get; set; }

        public bool RegInEmploymentService { get; set; }

        public DateTime? N1FormActDate { get; set; }

        public DateTime? BureauDirectionDate { get; set; }

        public DateTime? BureauDocumentsDate { get; set; }

        public DateTime? BureauCertifiedDate { get; set; }

        public List<SickCareDto> SickCareList { get; set; } = new List<SickCareDto>();

        public DateTime? StateChangeDate { get; set; }

        public string SheetNextNumber { get; set; }

        public DisabledStatus? DisabledStatus { get; set; }
    }
}
