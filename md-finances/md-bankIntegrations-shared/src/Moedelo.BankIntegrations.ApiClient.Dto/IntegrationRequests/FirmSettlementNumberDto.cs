using System.ComponentModel.DataAnnotations;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class FirmSettlementNumberDto
{
    public int FirmId { get; set; }

    public string SettlementNumber { get; set; }
}
