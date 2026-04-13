using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class LeadsOverSubscriptionCommand
    {
        public string OperatorLogin { get; set; }

        public string Description { get; set; }

        public string BucketId { get; set; }

        public string FunnelId { get; set; }
        
        public IReadOnlyCollection<string> Logins { get; set; }

        public bool TaskStatusCompleted { get; set; }
        
        public string Email { get; set; }
    }
}