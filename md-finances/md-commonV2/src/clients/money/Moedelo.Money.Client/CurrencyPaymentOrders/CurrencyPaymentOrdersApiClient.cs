using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.CurrencyPaymentOrders;

namespace Moedelo.Money.Client.CurrencyPaymentOrders
{
    [InjectAsSingleton]
    public class CurrencyPaymentOrdersApiClient : BaseCoreApiClient, ICurrencyPaymentOrdersApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string dateFormat = "yyyy-MM-dd";
        private const string prefix = "/private/api/v1/PaymentOrders/Currency";

        public CurrencyPaymentOrdersApiClient(
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
        
        public async Task<List<CurrencyPaymentOrderDto>> ByPeriodAsync(int firmId, int userId, PeriodRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<List<CurrencyPaymentOrderDto>>>($"{prefix}/ByPeriod", new
                {
                    StartDate = request.StartDate.ToString(dateFormat),
                    EndDate = request.EndDate.ToString(dateFormat)
                }, queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
            return response.data;
        }

        public async Task<List<CurrencyBalanceDto>> BalanceOnDateAsync(int firmId, int userId, DateTime date)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<List<CurrencyBalanceDto>>>($"{prefix}/BalanceOnDate",
                    new { date = date.ToString(dateFormat)},
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
            return response.data;
        }
    }
}