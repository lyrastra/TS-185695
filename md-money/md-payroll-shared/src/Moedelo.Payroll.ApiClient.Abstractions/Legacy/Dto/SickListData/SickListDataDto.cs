using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListDataDto
    {
        public long SpecialScheduleId { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? DateBreachRegime { get; set; }
        public long? ProlongSpecialScheduleId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public SicklistType Type { get; set; }
        public ChildAgeType? ChildAgeType { get; set; }
        public bool WorkerIsPartialSchedule { get; set; }
        public SickListWorkerDto Worker { get; set; }
        public SickListAdditionalDataDto AdditionalData { get; set; }
        public SickListIncomeAndCalculationDto Calculation { get; set; }
    }
}