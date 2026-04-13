using System;

namespace Moedelo.CommonV2.EventBus.Tester
{
    public class CreateUserEvent
    {
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
