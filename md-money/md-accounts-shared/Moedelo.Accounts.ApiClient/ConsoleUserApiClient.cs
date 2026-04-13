using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IConsoleUserApiClient))]
    internal sealed class ConsoleUserApiClient : BaseLegacyApiClient, IConsoleUserApiClient
    {
        public ConsoleUserApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ConsoleUserApiClient> logger) : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("AccountPrivateApiEndpoint"),
            logger)
        {
        }

        public Task<UserDto> GetOrCreateByLoginAsync(string login)
        {
            return PostAsync<UserDto>($"/Rest/ConsoleUser/GetOrCreate?login={login}");
        }
    }
}