using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.DataInformation;

public class SettlementAccountStatusDto
{
    public string SettlementNumber { get; set; }

    public string Bik { get; set; }

    public IntegrationPartners IntegrationPartner { get; set; }

    public SettlementIntegrationStatus Status { get; set; }

    public bool RequestExtractAvailable { get; set; }
}