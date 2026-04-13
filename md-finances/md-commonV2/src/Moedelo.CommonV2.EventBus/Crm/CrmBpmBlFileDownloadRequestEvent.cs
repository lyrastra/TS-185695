using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBpmBlFileDownloadRequestEvent
    {
        public string RequestId { get; set; }
        
        public string Url { get; set; }

        public DateTime Timestamp { get; set; }
    }
}