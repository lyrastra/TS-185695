using System;

namespace Moedelo.CommonV2.EventBus.Registration
{
    public class TestDataCreateEvent
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public bool IsOoo { get; set; }

        public DateTime Timestamp { get; set; }
    }
}