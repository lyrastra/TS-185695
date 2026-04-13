using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBpmBlFileRecognizeRequestEvent
    {
        public int FileId { get; set; }

        public string DocumentId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}