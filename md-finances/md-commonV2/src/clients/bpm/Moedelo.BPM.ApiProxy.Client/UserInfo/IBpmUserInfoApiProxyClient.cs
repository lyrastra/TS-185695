using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BPM.ApiProxy.Client.UserInfo
{
    public interface IBpmUserInfoApiProxyClient : IDI
    {
        Task<List<MessageAuthorDto>> GetInfoAsync(List<string> userIds);
        Task<MessageAuthorDto> GetInfoAsync(string userId);
        Task<MessageAuthorDto> GetInfoByEmailAsync(string email);
        Task<HttpFileModel> GetAvatarAsync(string avatarId);
    }
}