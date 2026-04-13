using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlLeadLiquidityUpdateEvent
    {
        public string LeadStatus { get; set; }

        public string SuiteLeadId { get; set; }

        public string UtmSource { get; set; }

        public DateTime Timestamp { get; set; }
    }
}