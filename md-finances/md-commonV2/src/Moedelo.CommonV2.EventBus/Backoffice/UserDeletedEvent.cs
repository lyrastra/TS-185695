using System;

namespace Moedelo.CommonV2.EventBus.Backoffice
{
    public class UserDeletedEvent
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public DateTime Timestamp { get; set; }
    }
}