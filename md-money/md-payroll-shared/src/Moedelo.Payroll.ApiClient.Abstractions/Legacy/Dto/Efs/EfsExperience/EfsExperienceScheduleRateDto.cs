using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience
{
    public class EfsExperienceScheduleRateDto
    {
        public int WorkerId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public decimal? Rate { get; set; }
    }
}