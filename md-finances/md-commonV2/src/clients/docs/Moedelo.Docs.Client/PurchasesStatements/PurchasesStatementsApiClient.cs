using Moedelo.Docs.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.PurchasesStatements
{
    [InjectAsSingleton]
    public class PurchasesStatementsApiClient : BaseApiClient, IPurchasesStatementsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public PurchasesStatementsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.FromResult(new List<PaidSumDto>());
            }
            return PostAsync<IReadOnlyCollection<long>, List<PaidSumDto>>($"/PurchasesStatements/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }
    }
}