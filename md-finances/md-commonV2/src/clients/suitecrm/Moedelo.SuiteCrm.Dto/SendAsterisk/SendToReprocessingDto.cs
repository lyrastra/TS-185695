using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.SendAsterisk
{
    public class SendToReprocessingDto
    {
        public string Email { get; set; }

        public string OpportunityName { get; set; }

        public string BucketId { get; set; }

        public string FunnelId { get; set; }

        public IReadOnlyCollection<string> Logins { get; set; }
    }
}