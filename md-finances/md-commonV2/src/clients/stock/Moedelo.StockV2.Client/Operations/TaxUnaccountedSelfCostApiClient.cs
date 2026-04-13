using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Dto.SelfCost;

namespace Moedelo.StockV2.Client.Operations
{
    [InjectAsSingleton]
    public class TaxUnaccountedSelfCostApiClient : BaseApiClient, ITaxUnaccountedSelfCostApiClient
    {
        private readonly SettingValue apiEndPoint;

        public TaxUnaccountedSelfCostApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) :
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
        
        public Task SaveAsync(int firmId, int userId, IReadOnlyCollection<SelfCostTaxUnaccountedSaveRequestDto> requests)
        {
            // пустой список считается корректным допустимым значением
            if (requests == null)
            {
                return Task.CompletedTask;
            }

            return PostAsync(
                $"/SelfCost/TaxUnaccounted/Save?firmId={firmId}&userId={userId}",
                requests);
        }
        
        public Task<List<SelfCostTaxUnaccountedDto>> GetAsync(int firmId, int userId)
        {
            return GetAsync<List<SelfCostTaxUnaccountedDto>>(
                $"/SelfCost/TaxUnaccounted/Get?firmId={firmId}&userId={userId}");
        }
    }
}