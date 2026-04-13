using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Money;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Money
{
    [InjectAsSingleton]
    public class MoneyTurnoverApiClient : BaseApiClient, IMoneyTurnoverApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public MoneyTurnoverApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<MoneyTurnoverDto>> GetByContractIdsAsync(int firmId, int userId, IReadOnlyCollection<int> contractIds)
        {
            var result = await PostAsync<IReadOnlyCollection<int>, DataResponseWrapper<List<MoneyTurnoverDto>>>($"/MoneyTurnover/GetByContracts?firmId={firmId}&userId={userId}", contractIds).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<MoneyTurnoverDto>> GetByKontragentIdsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            var result = await PostAsync<IReadOnlyCollection<int>, DataResponseWrapper<List<MoneyTurnoverDto>>>($"/MoneyTurnover/GetByKontragents?firmId={firmId}&userId={userId}", kontragentIds).ConfigureAwait(false);
            return result.Data;
        }
    }
}
