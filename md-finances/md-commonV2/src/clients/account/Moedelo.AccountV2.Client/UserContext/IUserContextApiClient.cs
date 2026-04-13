using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.UserContext;

namespace Moedelo.AccountV2.Client.UserContext;

public interface IUserContextApiClient
{
    Task<UserCommonContextDto> GetAsync(int userId, int firmId);

    [Obsolete("Используй IUserContextNetCoreApiClient::GetBasicInfoAsync")]
    Task<UserContextBasicInfoDto> GetBasicInfoAsync(int userId, int firmId);

    Task<HeaderUserContextDto> GetHeaderAsync(int userId, int firmId, CancellationToken cancellationToken = default);
}