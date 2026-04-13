using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class GetDataToRequisitesResponseDto
    {
        public List<IntegrationPartnerRequisitesDto> TurnedOn = new List<IntegrationPartnerRequisitesDto>();
        public List<IntegrationPartnerRequisitesDto> Accessible = new List<IntegrationPartnerRequisitesDto>();
        public List<IntegrationPartnerRequisitesDto> Inaccessible = new List<IntegrationPartnerRequisitesDto>();
    }
}
