using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkExemption;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListCalculationRequestDto
    {
        public long SpecialScheduleId { get; set; }
        
        public int WorkerId { get; set; }
        
        public short YearFirst { get; set; }
        
        public short YearSecond { get; set; }
        
        public int PaidDaysCount { get; set; }
        
        public decimal? YearFirstSum { get; set; }
        
        public decimal? YearSecondSum { get; set; }
        
        public long? ProlongSpecialScheduleId { get; set; }
        
        public CauseOfDisabilityMainCode? CauseOfDisabilityMainCode { get; set; }

        public CauseOfDisabilityAdditionalCode? CauseOfDisabilityAdditionalCode { get; set; }
        
        public BreachRegimeDto BreachRegime { get; set; }
        
        public InpatientHospitalStayDto InpatientHospitalStay { get; set; }

        public List<WorkExemptionDto> WorkExemptions { get; set; } = new List<WorkExemptionDto>();

        public List<SickCareDto> SickCareList { get; set; } = new List<SickCareDto>();

        public DateTime? N1FormActDate { get; set; }
        
        public DateTime? StartDate => WorkExemptions != null && WorkExemptions.Any()
            ? WorkExemptions.Min(x => x.Start)
            : null;
        
        public DateTime? EndDate => WorkExemptions != null && WorkExemptions.Any()
            ? WorkExemptions.Max(x => x.End)
            : null;
    }
}