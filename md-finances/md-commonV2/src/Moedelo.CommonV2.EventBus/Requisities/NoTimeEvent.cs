using System;

namespace Moedelo.CommonV2.EventBus.Requisities
{
    public class NoTimeEvent
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fio { get; set; }

        public DateTime Timestamp { get; set; }
    }
}