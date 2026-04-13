using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BPM.ApiProxy.Client.Chat
{
    public interface IBpmChatApiProxyClient : IDI
    {
        Task<List<ChatUserDto>> GetUsersAsync();
        Task<ChatAccountDto> GetAccountInfoAsync(int firmId);
        Task<ChatAccountDto> GetAccountInfoAsync(string login);
    }
}