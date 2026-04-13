using System;

namespace Moedelo.CommonV2.EventBus.CommonApi
{
    public class CurrentStepUserEvent
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public string PageUrl { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }
    }
}