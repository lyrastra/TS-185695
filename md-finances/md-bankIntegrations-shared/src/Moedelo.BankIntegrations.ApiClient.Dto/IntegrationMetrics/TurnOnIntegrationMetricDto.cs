using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMetrics;

public class TurnOnIntegrationMetricDto
{
    public int FirmId { get; set; }
    public int UserId { get; set; }
    public IntegrationSource IntegrationSource { get; set; }
    public IntegrationPartners Partner { get; set; }
}