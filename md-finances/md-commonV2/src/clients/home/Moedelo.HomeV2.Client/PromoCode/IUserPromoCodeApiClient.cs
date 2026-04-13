using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.PromoCode;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.PromoCode
{
    public interface IUserPromoCodeApiClient : IDI
    {
        Task<string> GetActivePromoCodeAsync(int firmId, int userId);

        Task<InviteFriendsWidgetDto> GetParticipantStateAsync(int firmId, int userId);

        Task<int> GetInvitedUsersCountAsync(int userId);
    }
}