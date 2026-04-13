using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Remains;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Remains
{
    [InjectAsSingleton(typeof(IOpeningBalanceApiClient))]
    public class OpeningBalanceApiClient : BaseApiClient, IOpeningBalanceApiClient
    {
        private readonly SettingValue apiEndPoint;

        public OpeningBalanceApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingsRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingsRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<OpeningBalancesRepresentation> GetAsync(int firmId, int userId)
        {
            return GetAsync<OpeningBalancesRepresentation>($"/api/v1/openingbalance?firmId={firmId}&userId={userId}");
        }

        public Task SaveAsync(int firmId, int userId, OpeningBalancesSaveRequestModel request)
        {
            return PostAsync<OpeningBalancesSaveRequestModel>($"/api/v1/openingbalance?firmId={firmId}&userId={userId}", request);
        }
    }
}
