using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Banks
{
    [InjectAsSingleton(typeof(IBanksApiClient))]
    public class BanksApiClient : BaseApiClient, IBanksApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public BanksApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Banks/V2";
        }

        public Task<List<BankDto>> GetByIdsAsync(IReadOnlyCollection<int> ids)
        {
            return GetByIdsAsync(ids, CancellationToken.None);
        }

        public Task<List<BankDto>> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken cancellationToken)
        {
            return PostAsync<IReadOnlyCollection<int>, List<BankDto>>("/GetByIds", ids, cancellationToken: cancellationToken);
        }

        public Task<List<BankDto>> GetByBiksAsync(IReadOnlyCollection<string> biks)
        {
            return GetByBiksAsync(biks, CancellationToken.None);
        }

        public Task<List<BankDto>> GetByBiksAsync(IReadOnlyCollection<string> biks, CancellationToken cancellationToken)
        {
            return PostAsync<IReadOnlyCollection<string>, List<BankDto>>("/GetByBiks", biks, cancellationToken: cancellationToken);
        }

        public Task<List<BankDto>> GetByInnsAsync(IReadOnlyCollection<string> inns)
        {
            return PostAsync<IReadOnlyCollection<string>, List<BankDto>>("/GetByInns", inns);
        }
    }
}