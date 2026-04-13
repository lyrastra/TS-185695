using System;

namespace Moedelo.CommonV2.EventBus.Sps
{
    public class UserWasInWebinarsEvent
    {
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}