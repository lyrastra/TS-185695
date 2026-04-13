using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Edm.Dto.Invitations
{
    public class GetEnabledResponseDto
    {
        public IReadOnlyCollection<int> KontragentIds { get; set; }
    }
}
