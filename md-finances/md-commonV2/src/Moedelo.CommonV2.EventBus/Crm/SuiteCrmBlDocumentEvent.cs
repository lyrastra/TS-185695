using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlDocumentEvent
    {
        public string DocumentId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}