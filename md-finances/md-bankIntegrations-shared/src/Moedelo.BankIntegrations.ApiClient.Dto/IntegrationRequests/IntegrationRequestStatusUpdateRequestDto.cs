using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class IntegrationRequestStatusUpdateRequestDto
{
    public int IntegrationRequestId { get; set; }

    public RequestStatus Status { get; set; }
}
