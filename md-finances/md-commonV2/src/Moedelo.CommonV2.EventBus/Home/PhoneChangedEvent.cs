using System;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.CommonV2.EventBus.Home
{
    /// <summary>
    /// Не используется
    /// </summary>
    public class PhoneChangedEvent
    {
        public int PhoneId { get; set; }

        public int FirmId { get; set; }

        public int UserId { get; set; }

        public string PhoneNumber { get; set; }

        public PhoneTypes PhoneType { get; set; }

        public DateTime Timestamp { get; set; }
    }
}