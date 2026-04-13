using System.ComponentModel.DataAnnotations;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class FirmSettlementNumberOnDateDto
{
    public int FirmId { get; set; }

    public string SettlementNumber { get; set; }

    public string Date { get; set; }

    public IntegrationPartners Partner { get; set; }
}
