using System;

namespace Moedelo.CommonV2.EventBus.Account
{
    /// <summary>
    /// Не используется
    /// </summary>
    public class UserRegistrationNameChangedEvent
    {
        public int UserId { get; set; }
        public string NewName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}