using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBpmBlDocumentFileLinkEvent
    {
        public int FileId { get; set; }
        
        public string DocumentId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}