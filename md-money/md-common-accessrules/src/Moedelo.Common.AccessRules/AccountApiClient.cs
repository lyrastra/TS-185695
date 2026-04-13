using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.AccessRules.Abstractions.Models;
using Moedelo.Common.AccessRules.Models;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Common.AccessRules
{
    [InjectAsSingleton(typeof(AccountApiClient))]
    internal sealed class AccountApiClient : BaseLegacyApiClient
    {
        public AccountApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AccountApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountApiEndpoint"),
                logger)
        {
        }

        internal Task<TariffRolePair> GetTariffRolePairAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/Rest/TariffsAndRoles/TariffRolePair?firmId={firmId}&userId={userId}";

            return GetAsync<TariffRolePair>(uri);
        }

        internal Task<TariffsAndRolesDto> GetTariffsAndRolesAsync()
        {
            var uri = $"/Rest/TariffsAndRoles/All";

            return GetAsync<TariffsAndRolesDto>(uri);
        }

        internal class TariffsAndRolesDto
        {
            public List<TariffInfo> Tariffs { get; set; }

            public List<RoleInfo> RoleInfos { get; set; }
        }
    }
}