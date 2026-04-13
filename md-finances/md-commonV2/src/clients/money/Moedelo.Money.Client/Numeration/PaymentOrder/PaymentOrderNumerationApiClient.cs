using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Dto;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Money.Client.PaymentOrderNumeration
{
    [InjectAsSingleton(typeof(IPaymentOrderNumerationApiClient))]
    public class PaymentOrderNumerationApiClient : BaseCoreApiClient, IPaymentOrderNumerationApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1/PaymentOrderNumeration";

        public PaymentOrderNumerationApiClient(
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
            return settingRepository.Get("MoneyNumerationApiEndpoint").Value;
        }

        public async Task<int> GetNextNumberAsync(int firmId, int userId, int settlementAccountId, int year)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var response = await GetAsync<ApiDataResult<int>>(
                $"{prefix}/NextNumber/",
                queryParams: new { settlementAccountId, year },
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }

        public async Task<IReadOnlyCollection<int>> GetNextNumbersAsync(int firmId, int userId, int settlementAccountId, int year, int? count)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var response = await GetAsync<ApiDataResult<IReadOnlyCollection<int>>>(
                $"{prefix}/NextNumbers/",
                queryParams: new { settlementAccountId, year, count },
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }
    }
}
