using System;

namespace Moedelo.CommonV2.EventBus.CommonApi
{
    public class UserEnterInnEvent
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public string Inn { get; set; }

        public DateTime Timestamp { get; set; }
    }
}