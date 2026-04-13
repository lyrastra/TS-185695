using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmCrudOpportunityCreateEvent
    {
        public int FirmId { get; set; }

        public string SuiteOpportunityId { get; set; }

        public DateTime Timestamp { get; set; }

    }
}