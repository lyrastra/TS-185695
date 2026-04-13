using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.BudgetaryStatusOfPayer;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.BudgetaryStatusOfPayer
{
    [InjectAsSingleton(typeof(IBudgetaryStatusOfPayerApiClient))]
    public class BudgetaryStatusOfPayerApiClient : BaseApiClient, IBudgetaryStatusOfPayerApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BudgetaryStatusOfPayerApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager
                 )
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/BudgetaryStatusOfPayer/V2";
        }

        public Task<List<BudgetaryStatusOfPayerDto>> GetListAsync()
        {
            return GetAsync<List<BudgetaryStatusOfPayerDto>>("/List");
        }
    }
}