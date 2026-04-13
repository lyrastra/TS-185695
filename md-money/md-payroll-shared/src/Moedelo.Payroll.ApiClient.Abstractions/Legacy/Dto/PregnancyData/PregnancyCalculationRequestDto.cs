using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkExemption;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData
{
    public class PregnancyCalculationRequestDto
    {
        public long SpecialScheduleId { get; set; }
        
        public int WorkerId { get; set; }
        
        public short YearFirst { get; set; }
        
        public short YearSecond { get; set; }

        public decimal? YearFirstSum { get; set; }
        
        public decimal? YearSecondSum { get; set; }

        public int ExcludedDaysCount { get; set; }
        
        public long? ProlongSpecialScheduleId { get; set; }
        
        public List<WorkExemptionDto> WorkExemptions { get; set; } = new List<WorkExemptionDto>();
        
        public DateTime? StartDate => WorkExemptions != null && WorkExemptions.Any()
            ? WorkExemptions.Min(x => x.Start)
            : null;
        
        public DateTime? EndDate => WorkExemptions != null && WorkExemptions.Any()
            ? WorkExemptions.Max(x => x.End)
            : null;
        
        public int PregnancyDaysCount => EndDate.HasValue && StartDate.HasValue
            ? EndDate.Value.Subtract(StartDate.Value.Date).Days + 1
            : 0;
    }
}