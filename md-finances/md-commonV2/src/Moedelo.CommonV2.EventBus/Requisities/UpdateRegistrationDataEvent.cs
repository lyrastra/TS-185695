using System;

namespace Moedelo.CommonV2.EventBus.Requisities
{
    public class UpdateRegistrationDataEvent
    {
        public int FirmId { get; set; }
        public string MainActivityCode { get; set; }
        public string MainActivityName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}