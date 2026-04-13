using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBlDocumentCreatedEvent
    {
        public string DocumentId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}