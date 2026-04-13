using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;

public class UserIntegrationInfoToRequisitesDto
{
    public List<IntegrationPartnerRequisitesDto> TurnedOn = new List<IntegrationPartnerRequisitesDto>();
    public List<IntegrationPartnerRequisitesDto> Accessible = new List<IntegrationPartnerRequisitesDto>();
    public List<IntegrationPartnerRequisitesDto> Inaccessible = new List<IntegrationPartnerRequisitesDto>();
}