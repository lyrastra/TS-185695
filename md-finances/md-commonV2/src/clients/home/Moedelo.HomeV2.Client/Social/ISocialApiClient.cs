using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.Social;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.Social
{
    public interface ISocialApiClient : IDI
    {
        Task SaveUserSocialInfoAsync(UserSocialInfoDto socialInfo);
    }
}