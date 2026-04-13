using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class IntegrationRequestStatusesUpdateRequestDto
{
    public IReadOnlyCollection<IntegrationRequestStatusUpdateRequestDto> Statuses { get; set; }
}
