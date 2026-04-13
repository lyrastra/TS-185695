using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.Synchronization
{
    public class SyncResultDto
    {
        public List<int> Synced { get; set; }

        public List<int> NotChanged { get; set; }

        public List<int> NotSynced { get; set; }
    }
}
