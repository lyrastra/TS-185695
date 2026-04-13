using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class IntegrationRequestUpdateStatusRequestDto
{
    /// <summary>Новый статус</summary>
    public RequestStatus Status { get; set; }

    /// <summary>Значение статуса</summary>
    public RequestStatus OldStatus { get; set; }
}
