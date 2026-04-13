using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Ndfl3Validator;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Ndfl3Validator
{
    [InjectAsSingleton]
    public class Ndfl3ValidatorApiClient : BaseApiClient, INdfl3ValidatorApiClient
    {
        private readonly SettingValue apiEndPoint;

        public Ndfl3ValidatorApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingsRepository) : base(
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

        public Task<IReadOnlyCollection<StockProductNegativeBalanceDto>> GetNegativeStockBalancesAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<IReadOnlyCollection<StockProductNegativeBalanceDto>>(
                "/Ndfl3Validation/GetNegativeStockBalances",
                new { firmId, userId, startDate, endDate });
        }
        
        public Task<int> GetUncoveredPaymentsCountAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<int>(
                "/Ndfl3Validation/GetUncoveredPaymentsCount",
                new { firmId, userId, startDate, endDate });
        }
    }
}
