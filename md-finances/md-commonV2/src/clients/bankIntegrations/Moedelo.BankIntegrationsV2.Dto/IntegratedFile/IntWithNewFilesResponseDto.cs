using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedFile
{
    public class IntWithNewFilesResponseDto
    {
        public List<IntegratedFilesDto> IntegratedFiles { get; set; }

        public List<IntegrationPartners> IntegrationPartners { get; set; }
    }
}
