using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlLeadBankInformedUpdateEvent
    {
        public string BankInformed { get; set; }

        public string SuiteAccountId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}