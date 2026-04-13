using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges
{
    public class EmploymentChangesWorkerDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Snils { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public bool IsStaffWorker { get; set; }
        
        public DateTime? DateOfStartWork { get; set; }

        public FounderType FounderType { get; set; }
        
        public SzvTdSubmittedType SzvTdSubmittedType { get; set; }
    }
    
    public enum SzvTdSubmittedType : byte
    {
        None = 0,
        Recruitment,
        Firing,
        RecruitmentAndFiring
    }
}
