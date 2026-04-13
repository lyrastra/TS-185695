using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class IntegrationRequestSetStatusRequestDto
{
    /// <summary> Статус запроса </summary>
    public RequestStatus Status { get; set; }
    /// <summary> Фактическая дата запроса (null если не надо менять) </summary>
    public string DateOfCall { get; set; }
}
