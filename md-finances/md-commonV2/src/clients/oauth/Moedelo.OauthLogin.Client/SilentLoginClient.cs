using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

namespace Moedelo.OauthLogin.Client
{
    [InjectAsSingleton]
    public class SilentLoginClient : ISilentLoginClient
    {
        private readonly ISilentLoginRedisDbExecuter silentLoginRedisDbExecuter;

        public SilentLoginClient(ISilentLoginRedisDbExecuter silentLoginRedisDbExecuter)
        {
            this.silentLoginRedisDbExecuter = silentLoginRedisDbExecuter;
        }

        public Task<bool> AllowLoginAsync(int clientId, string login, TimeSpan? ttl = null)
        {
            return silentLoginRedisDbExecuter.SetValueForKeyAsync(GetKey(clientId, login), "1", ttl ?? new TimeSpan(0, 2, 0));
        }

        public async Task<bool> IsAllowLoginAsync(int clientId, string login)
        {
            var key = GetKey(clientId, login);
            var result = await silentLoginRedisDbExecuter.ExistsAsync(key).ConfigureAwait(false);
            await silentLoginRedisDbExecuter.DeleteKeyAsync(key).ConfigureAwait(false);

            return result;
        }

        private static string GetKey(int clientId, string login)
        {
            return $"client:{clientId}:login:{login}";
        }
    }
}