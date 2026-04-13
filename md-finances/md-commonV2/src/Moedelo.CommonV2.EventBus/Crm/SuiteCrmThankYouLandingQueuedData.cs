using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmThankYouLandingQueuedData
    {
        public string Login { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
        
        public bool ToAsterisk { get; set; }

        public DateTime Timestamp { get; set; }
    }
}