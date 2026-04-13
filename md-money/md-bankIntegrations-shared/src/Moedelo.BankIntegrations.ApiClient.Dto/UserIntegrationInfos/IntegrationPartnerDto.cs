using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;

public class IntegrationPartnerDto
{
    public IntegrationPartners IntegrationPartner { get; set; }
    public string Name { get; set; }
}