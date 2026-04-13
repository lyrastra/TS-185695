using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Threading.Tasks;

namespace Moedelo.Chat.Requests.Client
{
    public interface IChatPlatformRequestClient: IDI
    {
        Task BindClientDataToRequestByUserIdAsync(Guid requestId, int userId);

        Task BindClientDataToRequestByLoginAsync(Guid requestId, string login);
    }
}
