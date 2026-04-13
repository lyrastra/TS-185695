using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class LeadsReprocessingCommand
    {
        public string Email { get; set; }

        public string OpportunityName { get; set; }

        public string BucketId { get; set; }

        public string FunnelId { get; set; }

        public IReadOnlyCollection<string> Logins { get; set; }
    }
}