using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Client
{
    [InjectAsSingleton]
    public class MoneyOperationsClient : BaseCoreApiClient, IMoneyOperationsClient
    {
        private readonly ISettingRepository settingRepository;

        public MoneyOperationsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("MoneyApiEndpoint").Value;
        }

        public async Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            await DeleteByRequestAsync(
                "/api/v1/operations",
                documentBaseIds,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}