using System.Collections.Generic;

namespace Moedelo.Edm.Dto.Invitations
{
    public class GetEnabledRequestDto
    {
        public IReadOnlyCollection<int> KontragentIds { get; set; }

        public int EdmSystem { get; set; }
    }
}
