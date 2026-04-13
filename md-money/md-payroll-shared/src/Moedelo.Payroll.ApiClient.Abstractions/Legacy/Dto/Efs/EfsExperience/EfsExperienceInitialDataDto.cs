using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience
{
    public class EfsExperienceInitialDataDto
    {
        public List<EfsExperienceWorkerDto> Workers { get; set; }

        public List<EfsExperiencePeriodDto> ExperiencePeriods { get; set; }
        
        public List<EfsExperienceTerritoryCodeItemDto> TerritoryCodes { get; set; }

        public List<EfsExperienceScheduleRateDto> ScheduleRates { get; set; }
        
        public List<EfsExperienceWorkerWorkPeriodDto> WorkPeriods { get; set; }

        public List<ForeignerStatusHistoryDto> ForeignerStatusHistories { get; set; }

        public List<EfsExperiencePositionDto> PositionHistories { get; set; }
    }
}