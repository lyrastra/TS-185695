using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.StockV2.Client.Stocks
{
    [InjectAsSingleton]
    public class StockInitializationClient : BaseApiClient, IStockInitializationClient
    {
        private readonly SettingValue apiEndPoint;

        public StockInitializationClient(
            IHttpRequestExecutor httpRequestExecutor,
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

        public Task InitializeAsync(int firmId, int userId)
        {
            return PostAsync($"/StockInitialization/CreateStockIfNotExist?firmId={firmId}&userId={userId}");
        }
    }
}