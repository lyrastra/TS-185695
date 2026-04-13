using System;

namespace Moedelo.CommonV2.EventBus.Pay
{
    public class UserWasInPayEvent
    {
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}