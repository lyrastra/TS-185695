using System.Collections.Generic;

namespace Moedelo.RequisitesV2.Dto.Purses
{
    public class PurseValidationForDeleteRequestDto
    {
        public IReadOnlyCollection<int> PurseIds { get; set; }
    }
}