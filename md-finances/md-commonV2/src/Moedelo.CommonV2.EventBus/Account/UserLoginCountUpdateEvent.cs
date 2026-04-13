using System;

namespace Moedelo.CommonV2.EventBus.Account
{
    public class UserLoginCountUpdateEvent
    {
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public int LoginCount { get; set; }
    }
}