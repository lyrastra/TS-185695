using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.UserIntegrationInfo;

public interface IUserIntegrationInfoApiClient
{
    Task<List<ConnectIntegrationStateResponseDto>> GetConnectIntegrationState(ConnectIntegrationStateRequestDto dto);
    Task<UserIntegrationInfoDto> GetDataAsync(int firmId, int userId);
    Task<UserIntegrationInfoToRequisitesDto> GetDataToRequisitesAsync(int firmId, int userId);
}