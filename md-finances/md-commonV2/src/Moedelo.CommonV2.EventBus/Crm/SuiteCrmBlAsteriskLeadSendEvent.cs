using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlAsteriskLeadSendEvent
    {
        public string SuiteLeadId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string QualityGroup { get; set; }

        public string Phone { get; set; }

        public string TimeZone { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime Timestamp { get; set; }
    }
}