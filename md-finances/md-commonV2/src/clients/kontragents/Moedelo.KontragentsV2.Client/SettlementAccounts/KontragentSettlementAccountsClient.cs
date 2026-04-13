using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.SettlementAccounts
{
    [InjectAsSingleton]
    public class KontragentSettlementAccountsClient : BaseApiClient, IKontragentSettlementAccountsClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentSettlementAccountsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<KontragentSettlementAccountDto> GetByIdAsync(int firmId, int userId, long id)
        {
            return GetAsync<KontragentSettlementAccountDto>("/SettlementAccountV2/GetById", new { userId, firmId, id });
        }

        public Task<List<KontragentSettlementAccountDto>> GetByKontragentAsync(int firmId, int userId, int kontragentId)
        {
            return GetAsync<List<KontragentSettlementAccountDto>>("/SettlementAccountV2/GetByKontragent", new { userId, firmId, kontragentId });
        }

        public Task<List<KontragentSettlementAccountDto>> GetByNumberAsync(int firmId, int userId, int kontragentId, string number)
        {
            return GetAsync<List<KontragentSettlementAccountDto>>("/SettlementAccountV2/GetByNumber", new { userId, firmId, kontragentId, number });
        }

        public Task<long> SaveAsync(int firmId, int userId, KontragentSettlementAccountDto settlementAccount)
        {
            return PostAsync<KontragentSettlementAccountDto, long>($"/SettlementAccountV2/Save?firmId={firmId}&userId={userId}", settlementAccount);
        }

        public Task UpdateLastChoiceDateAsync(int firmId, int userId, long settlementAccountId)
        {
            return PostAsync($"/SettlementAccount/UpdateLastChoiceDate?firmId={firmId}&userId={userId}&settlementAccountId={settlementAccountId}");
        }

        public Task AddAllAsync(int firmId, int userId, List<KontragentSettlementAccountDto> dtos)
        {
            return PostAsync($"/SettlementAccount/AddAll?firmId={firmId}&userId={userId}", dtos);
        }
    }
}