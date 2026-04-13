using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrationsV2.Dto.ExternalPartner.Robokassa
{
    public class RobokassaTransferFilesRequestDto
    {
        public List<RoboxRequestDto> Requests { get; set; }
    }
}
