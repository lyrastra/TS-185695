using System;

namespace Moedelo.CommonV2.EventBus.CommonApi
{
    public class MasterOfRegistrationStepUpdatedEvent
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public int StepNumber { get; set; }

        public bool IsOoo { get; set; }

        public DateTime Timestamp { get; set; }
    }
}