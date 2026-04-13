using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Dto.ClosedPeriods;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Json;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Finances.Client.ClosedPeriods
{
    [InjectAsSingleton]
    public class MoneyClosedPeriodsClient : BaseApiClient, IMoneyClosedPeriodsClient
    {
        private readonly SettingValue apiValue;

        public MoneyClosedPeriodsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiValue = settingRepository.Get("FinancesPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiValue.Value;
        }

        public async Task<DateTime> GetLastClosedDateAsync(int firmId, int userId)
        {
            var response = await GetAsync<string>("/ClosedPeriods/LastClosedDate", new { firmId, userId }).ConfigureAwait(false);

            return response.ToIsoDateTime();
        }

        public Task<List<MoneyDocumentsCountDto>> CountOrdersNonProvidedInAccountingAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            const string uri = "/ClosedPeriods/CountOrdersNonProvidedInAccounting";
            var queryParams = new {firmId, userId, startDate, endDate};

            return GetAsync<List<MoneyDocumentsCountDto>>(uri, queryParams);
        }
        
        public Task<List<MoneyDocumentsCountDto>> OrdersNonProvidedInAccountingAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            const string uri = "/ClosedPeriods/OrdersNonProvidedInAccounting";
            var queryParams = new {firmId, userId, startDate, endDate};

            return GetAsync<List<MoneyDocumentsCountDto>>(uri, queryParams);
        }
    }
}