using System;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class IntegrationRequestsWithXmlHistoryFilterDto
{
    public int FirmId { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public IntegrationCallType CallType { get; set; }
}
