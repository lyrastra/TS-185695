using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Initialization
{
    [InjectAsSingleton]
    public class InitializationApiClient : BaseApiClient, IInitializationApiClient
    {
        private readonly SettingValue apiEndPoint;

        public InitializationApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<AccountingInitializationState> GetInitializationStatusAsync(int firmId, int userId)
        {
            var result = await GetAsync<InitializationStatusDto>("/InitializationApi/GetInitializationStatus", new {firmId, userId}).ConfigureAwait(false);
            return result.Data;
        }

        public Task StartInitializationAsync(int firmId, int userId)
        {
            return PostAsync($"/InitializationApi/StartInitialization?firmId={firmId}&userId={userId}");
        }
    }
}