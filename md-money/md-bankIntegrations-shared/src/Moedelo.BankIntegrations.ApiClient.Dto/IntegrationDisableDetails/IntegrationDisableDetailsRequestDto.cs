using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationDisableDetails;

public class IntegrationDisableDetailsRequestDto
{
    public List<int> IntegrationRequestIds { get; set; }
}