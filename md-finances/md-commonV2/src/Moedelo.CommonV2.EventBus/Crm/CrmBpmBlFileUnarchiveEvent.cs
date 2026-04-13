using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBpmBlFileUnarchiveEvent
    {
        public int FileId { get; set; }
        
        public string RequestId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}