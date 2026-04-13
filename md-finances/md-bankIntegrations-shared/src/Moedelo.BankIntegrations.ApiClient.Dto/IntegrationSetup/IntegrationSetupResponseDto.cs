using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationSetup;

public class IntegrationSetupResponseDto
{
    public IntegrationResponseStatusCode StatusCode { get; set; }

    public string ErrorText { get; set; }
}