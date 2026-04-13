using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Cashier;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Cashier
{
    [InjectAsSingleton]
    public class CashierApiClient : BaseApiClient, ICashierApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public CashierApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<CashierDto> GetAsync(int firmId, int userId, long id)
        {
            var result = await GetAsync<DataResponseWrapper<CashierDto>>("/CashierApi/Get", new
            {
                firmId,
                userId,
                id
            }).ConfigureAwait(false);

            return result?.Data;
        }

        public async Task<CashierCollectionDto> GetCashiersListAsync(int firmId, int userId)
        {
            var result = await GetAsync<DataResponseWrapper<CashierCollectionDto>>("/CashierApi/GetCashiersList", new
            {
                firmId,
                userId
            }).ConfigureAwait(false);

            return result?.Data;
        }
    }
}