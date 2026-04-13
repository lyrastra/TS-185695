using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData
{
    public class PregnancyDataDto
    {
        public long SpecialScheduleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Number { get; set; }
        public long? ProlongSpecialScheduleId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public PregnancyWorkerDto Worker { get; set; }
        public bool WorkerIsPartialSchedule { get; set; }
        public PregnancyAdditionalDataDto AdditionalData { get; set; }
        public PregnancyIncomeAndCalculationDto Calculation { get; set; }
    }
}