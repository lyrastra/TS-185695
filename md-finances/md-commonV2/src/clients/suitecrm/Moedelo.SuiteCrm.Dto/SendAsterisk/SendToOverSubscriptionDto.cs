using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.SendAsterisk
{
    public class SendToOverSubscriptionDto
    {
        public string OperatorLogin { get; set; }

        public string Description { get; set; }

        public string BucketId { get; set; }

        public string FunnelId { get; set; }
        
        public IReadOnlyCollection<string> Logins { get; set; }

        public bool? TaskStatusCompleted { get; set; }
        
        public string EmailForSendResult { get; set; }
    }
}