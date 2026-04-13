using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class SettlementNumbersWithoutRequestsInPeriodRequestDto
{
    public IReadOnlyCollection<FirmSettlementNumberDto> SettlementNumbers { get; set; }

    public IntegrationPartners IntegrationPartner { get; set; }

    public string BeginDate { get; set; }
}
