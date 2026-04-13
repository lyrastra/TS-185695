using System;

namespace Moedelo.CommonV2.EventBus.Home
{
    public class UserWasInOfferEvent
    {
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}