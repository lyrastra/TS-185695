using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlAsteriskLeadDeleteEvent
    {
        public string SuiteLeadId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}