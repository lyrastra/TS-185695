using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto.Bank;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AgentsV2.Client.Bank
{
    [InjectAsSingleton]
    public class BankApiClient : BaseApiClient, IBankApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BankApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager
        ) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AgentsApiUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<BankItemDto>> GetBanks(string query, int count)
        {
            var result = await GetAsync<DataWrapper<List<BankItemDto>>>("/Bank/GetBanks", new { query, count }).ConfigureAwait(false);
            return result.Data;
        }
    }
}
