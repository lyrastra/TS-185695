using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions;
using Moedelo.Accounts.Clients.Dto;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IUserAccessControlApiClient))]
    internal sealed class UserAccessControlApiClient: BaseLegacyApiClient, IUserAccessControlApiClient
    {
        public UserAccessControlApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UserAccessControlApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("UserAccessControlApiEnpoint"),
                logger)
        {
        }

        public async Task<ISet<AccessRule>> GetExplicitUserRulesAsync(UserId userId)
        {
            var response = await GetAsync<DataWrapper<ISet<AccessRule>>>(
                "/GetExplicitUserRules",new { userId });

            return response.Data;
        }
    }
}