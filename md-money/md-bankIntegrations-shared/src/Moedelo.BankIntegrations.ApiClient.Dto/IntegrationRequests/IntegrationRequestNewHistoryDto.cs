using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class IntegrationRequestNewHistoryDto
{
    public int RequestId { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public IReadOnlyCollection<IntegrationRequestHistoryUnitDto> XmlHistory { get; set; }
}
