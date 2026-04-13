using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class TaskForOfferPayCreationResultEvent
    {
        public Guid RequestId { get; set; }

        public bool Success { get; set; }
        
        public bool IsNotTarget { get; set; }
        
        public int FirmId { get; set; }
        
        public string TaskId { get; set; }
    }
}