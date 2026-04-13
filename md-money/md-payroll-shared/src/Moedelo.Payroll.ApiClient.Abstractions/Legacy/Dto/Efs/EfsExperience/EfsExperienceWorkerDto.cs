using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience
{
    public class EfsExperienceWorkerDto : EfsWorkerDto
    {
        public string Inn { get; set; }

        public string Snils { get; set; }

        public FounderType FounderType { get; set; }

        public bool IsForeigner { get; set; }

        public bool IsExpert { get; set; }

        public string CountryCode { get; set; }

        public WorkerForeignerStatus WorkerForeignerStatus { get; set; }

        public string WorkplaceNumber { get; set; }

        public WorkingConditionClass? WorkingConditionClass { get; set; }
        
        public bool IsNotStaff { get; set; }

        public DateTime? WorkStartDate { get; set; }

        public DateTime? TerminationDate { get; set; }
        
        public bool IsPartTime { get; set; }
    }
}