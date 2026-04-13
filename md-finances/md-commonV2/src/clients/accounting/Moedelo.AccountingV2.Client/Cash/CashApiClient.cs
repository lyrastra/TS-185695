using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Cash;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Cash
{
    [InjectAsSingleton]
    public class CashApiClient : BaseApiClient, ICashApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public CashApiClient(
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

        public Task<List<CashDto>> GetAsync(int firmId, int userId)
        {
            return GetAsync<List<CashDto>>("/Cash", new { firmId, userId });
        }

        public Task<CashDto> GetByIdAsync(int firmId, int userId, long id)
        {
            return GetAsync<CashDto>("/Cash", new { firmId, userId, id });
        }

        public Task<List<CashDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids)
        {
            if (ids.Count == 0)
            {
                return Task.FromResult(new List<CashDto>());
            }
            return PostAsync<IReadOnlyCollection<long>, List<CashDto>>($"/Cash/ByIds?firmId={firmId}&userId={userId}", ids);
        }
        
        public Task<int> GetOutgoingCashOrderNextNumber(int firmId, int userId, long cashId, int year)
        {
            return GetAsync<int>("/Cash/GetOutgoingCashOrderNextNumber", new { firmId, userId, cashId, year });
        }
    }
}