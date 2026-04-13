using System.Collections.Generic;

namespace Moedelo.RequisitesV2.Dto.Purses
{
    public class PurseDeleteRequestDto
    {
        public IReadOnlyCollection<int> PurseIds { get; set; }
        
        public bool RemoveRelatedKontragents { get; set; }
    }
}