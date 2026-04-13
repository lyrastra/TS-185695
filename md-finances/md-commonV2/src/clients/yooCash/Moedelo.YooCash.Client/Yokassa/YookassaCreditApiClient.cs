using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.YooCash.Dto;

namespace Moedelo.YooCash.Client.Yookassa
{
    [InjectAsSingleton]
    public class YookassaCreditApiClient : BaseCoreApiClient, IYookassaCreditApiClient
    {
        private readonly SettingValue apiEndpoint;

        public YookassaCreditApiClient(
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
            apiEndpoint = settingRepository.Get("YookassaPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<YookassaCreditDto> GetAsync(int firmId, int userId, DateTime date)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await GetAsync<DataWrapper<YookassaCreditDto>>($"/v1/YookassaCredit?firmId={firmId}&userId={userId}&date={date.ToString("yyyy-MM-dd")}", queryHeaders: tokenHeaders);
            return result.Data;
        }

        public async Task SetAsync(int firmId, int userId, YookassaCreditDto yookassaCredit)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync($"/v1/YookassaCredit?firmId={firmId}&userId={userId}", yookassaCredit, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int firmId, int userId, DateTime date)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await DeleteByRequestAsync($"/v1/YookassaCredit?firmId={firmId}&userId={userId}&date={date.ToString("yyyy-MM-dd")}", new { }, queryHeaders: tokenHeaders);
        }
    }
}