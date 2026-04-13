#nullable enable
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.UserContext;

namespace Moedelo.AccountV2.Client.UserContext
{
    public interface IUserContextNetCoreApiClient
    {
        Task<UserContextBasicInfoDto> GetBasicInfoAsync(int userId, int firmId);
        Task<UserContextBasicInfoDto?> GetAuthorizedUserInfoAsync(int userId, int firmId, CancellationToken cancellationToken);
    }
}
