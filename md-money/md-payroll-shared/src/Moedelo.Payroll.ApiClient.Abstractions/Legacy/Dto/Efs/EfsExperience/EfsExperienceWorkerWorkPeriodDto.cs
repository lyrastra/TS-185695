using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience
{
    public class EfsExperienceWorkerWorkPeriodDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsNotStaff { get; set; }

        public int WorkerId { get; set; }
    }
}