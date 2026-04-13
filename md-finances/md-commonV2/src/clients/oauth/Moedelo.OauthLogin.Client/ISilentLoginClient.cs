using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.OauthLogin.Client
{
    public interface ISilentLoginClient : IDI
    {
        Task<bool> AllowLoginAsync(int clientId, string login, TimeSpan? ttl = null);

        Task<bool> IsAllowLoginAsync(int clientId, string login);
    }
}