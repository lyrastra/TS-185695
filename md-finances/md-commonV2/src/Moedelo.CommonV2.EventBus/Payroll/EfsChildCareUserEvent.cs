using System;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.CommonV2.EventBus.Payroll
{
    public class EfsChildCareUserEvent
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public EfsChildCareEventType ActionType { get; set; }
        public DateTime? OldDate { get; set; }
        public DateTime? NewDate { get; set; }
    }
}