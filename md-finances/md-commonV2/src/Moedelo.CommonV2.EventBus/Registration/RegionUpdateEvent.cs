using System;

namespace Moedelo.CommonV2.EventBus.Registration
{
    public class RegionUpdateEvent
    {
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public string Phone { get; set; }
        public int RegionId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}