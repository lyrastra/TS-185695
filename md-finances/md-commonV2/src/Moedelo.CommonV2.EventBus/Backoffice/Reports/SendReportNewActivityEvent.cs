using System;

namespace Moedelo.CommonV2.EventBus.Backoffice.Reports
{
    public class SendReportNewActivityEvent
    {
        public bool ForIp { get; set; }

        public bool ForOoo { get; set; }

        public Guid QueryGuid { get; set; }

        public string Email { get; set; }
    }
}