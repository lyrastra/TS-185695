using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmCrudTaskEvent
    {
        public string TaskId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}