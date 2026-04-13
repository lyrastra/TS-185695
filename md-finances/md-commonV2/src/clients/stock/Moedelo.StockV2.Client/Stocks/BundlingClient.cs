using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Stocks;

namespace Moedelo.StockV2.Client.Stocks
{
    [InjectAsSingleton]
    public class BundlingClient : BaseApiClient, IBundlingClient
    {
        private readonly SettingValue apiEndPoint;

        public BundlingClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }


        public async Task<List<BundlingDto>> GetForPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            var response = await GetAsync<ListResponse<BundlingDto>>(
                "/Bundling/GetForPeriod",
                new { firmId, userId, startDate, endDate }).ConfigureAwait(false);

            return response.Items;
        }
    }
}
