using System;
using Moedelo.Payroll.Enums.Efs;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience
{
    public class EfsExperiencePeriodDto
    {
        public long? PeriodId { get; set; }
        
        public int WorkerId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public EfsExperiencePeriodType Type { get; set; }
    }
}