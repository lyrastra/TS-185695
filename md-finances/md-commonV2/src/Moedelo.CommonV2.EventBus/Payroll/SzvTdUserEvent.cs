using System;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.CommonV2.EventBus.Payroll
{
    public class SzvTdUserEvent
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public int WorkerId { get; set; }
        public WorkerActionType ActionType { get; set; }
        public DateTime? OldDate { get; set; }
        public DateTime? NewDate { get; set; }
        public DateTime? NewOrderDate { get; set; }
        public DateTime? OldOrderDate { get; set; }
    }
}