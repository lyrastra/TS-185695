using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBpmBlCaseStateChangedEvent
    {
        public string CaseId { get; set; }
        
        public int FirmId { get; set; }
        
        public int? UserId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}