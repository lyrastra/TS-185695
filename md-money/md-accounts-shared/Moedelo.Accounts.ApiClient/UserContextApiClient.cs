using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IUserContextApiClient))]
    public class UserContextApiClient: BaseApiClient, IUserContextApiClient
    {
        public UserContextApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UserContextApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("UserContextApiEndpoint"),
                logger)
        {
        }
        
        public Task<UserCommonContextWithoutRulesDto> GetAsync(UserId userId, FirmId firmId)
        {
            return GetAsync<UserCommonContextWithoutRulesDto>("/V2/GetUserCommonContextWithoutRules", new { userId, firmId });
        }
    }
}