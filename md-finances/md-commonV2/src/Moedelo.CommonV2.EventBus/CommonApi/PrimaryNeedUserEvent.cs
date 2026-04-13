using System;

namespace Moedelo.CommonV2.EventBus.CommonApi
{
    public class PrimaryNeedUserEvent
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public string PrimaryNeedName { get; set; }

        public DateTime Timestamp { get; set; }
    }
}