using System;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.CommonV2.EventBus.Payroll
{
    public class WorkerContractUserEvent
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public long WorkContractId { get; set; }
        public WorkerContractEventType EventType { get; set; }
        public WorkerContractPeriodEvent OldPeriod { get; set; }
        public WorkerContractPeriodEvent NewPeriod { get; set; }
        public bool IsEfsExcluded { get; set; }
        public bool HasSfrCharges { get; set; }
    }

    public class WorkerContractPeriodEvent
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}