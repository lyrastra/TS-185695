using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkExemption;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListSaveDataDto
    {
        public SheetType SheetOfDisabilityType { get; set; }

        public DateTime? SheetOfDisabilityDate { get; set; }

        public bool IsDuplicate { get; set; }

        public string MedicalOrganizationName { get; set; }

        public string MedicalOrganizationAddress { get; set; }

        public string MedicalOrganizationOgrn { get; set; }

        public PlaceOfWorkType? PlaceOfWork { get; set; }

        public string SheetNumber { get; set; }

        public List<WorkExemptionDto> WorkExemptions { get; set; }

        public DateTime? StartOfWorkingDate { get; set; }

        public ConditionOfCalculation FirstConditionOfCalculation { get; set; }

        public ConditionOfCalculation SecondConditionOfCalculation { get; set; }

        public ConditionOfCalculation ThirdConditionOfCalculation { get; set; }

        public CauseOfDisabilityMainCode? CauseOfDisabilityMainCode { get; set; }

        public CauseOfDisabilityAdditionalCode? CauseOfDisabilityAdditionalCode { get; set; }
        
        public CauseOfDisabilityMainCode? CauseOfDisabilityChangeCode { get; set; }
        
        public string SheetProlongNumber { get; set; }
        
        public bool RegInEmploymentService { get; set; }
        
        public DateTime? N1FormActDate { get; set; }
        public DateTime? SanatoriumVoucherStartDate { get; set; }
        public DateTime? SanatoriumVoucherEndDate { get; set; }
        public string SanatoriumVoucherNumber { get; set; }
        public string SanatoriumVoucherOgrn { get; set; }
        public BreachRegimeDto BreachRegime { get; set; }
        public InpatientHospitalStayDto InpatientHospitalStay { get; set; }
        public DisabilityGroupType? DisabilityGroupType { get; set; }
        public DateTime? BureauDirectionDate { get; set; }
        public DateTime? BureauDocumentsDate { get; set; }
        public DateTime? BureauCertifiedDate { get; set; }
        public List<SickCareDto> SickCareList { get; set; } = new List<SickCareDto>();
        public DateTime? StateChangeDate { get; set; }
        public string SheetNextNumber { get; set; }
        public DisabledStatus? DisabledStatus { get; set; }

        public DateTime? StartDate => WorkExemptions != null && WorkExemptions.Any()
            ? WorkExemptions.Min(x => x.Start)
            : null;
        
        public DateTime? EndDate => WorkExemptions != null && WorkExemptions.Any()
            ? WorkExemptions.Max(x => x.End)
            : null;

        public bool IsSanatoriumVoucherAdditionalCode => (CauseOfDisabilityAdditionalCode.HasValue &&
                                                          (CauseOfDisabilityAdditionalCode.Value == Allowances.SickList
                                                               .CauseOfDisabilityAdditionalCode.Code017 ||
                                                           CauseOfDisabilityAdditionalCode.Value == Allowances.SickList
                                                               .CauseOfDisabilityAdditionalCode.Code018 ||
                                                           CauseOfDisabilityAdditionalCode.Value == Allowances.SickList
                                                               .CauseOfDisabilityAdditionalCode.Code019));
        public bool IsStateChangeDateRequired => DisabledStatus.HasValue &&
                                                 (DisabledStatus.Value == Allowances.SickList.DisabledStatus.Status32 ||
                                                  DisabledStatus.Value == Allowances.SickList.DisabledStatus.Status33 ||
                                                  DisabledStatus.Value == Allowances.SickList.DisabledStatus.Status34 ||
                                                  DisabledStatus.Value == Allowances.SickList.DisabledStatus.Status36);
    }
}