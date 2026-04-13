using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlDocumentXmlUpdatedEvent
    {
        public string DocumentId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}