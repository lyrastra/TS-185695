using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationDisableDetails;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationDisableDetails;

public interface IIntegrationDisableDetailsClient
{
    Task<List<IntegrationDisableDetailsResponseDto>> GetByIntegrationRequestIdsAsync(IntegrationDisableDetailsRequestDto dto);
}