using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmTaskFromKayakoQueuedData
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fio { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }
    }
}