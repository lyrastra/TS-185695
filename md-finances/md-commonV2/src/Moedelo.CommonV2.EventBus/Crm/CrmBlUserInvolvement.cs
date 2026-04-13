using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBlUserInvolvementEvent
    {
        public int FirmId { get; set; }

        public string Phone { get; set; }

        public string Subject { get; set; }

        public DateTime Timestamp { get; set; }
    }
}