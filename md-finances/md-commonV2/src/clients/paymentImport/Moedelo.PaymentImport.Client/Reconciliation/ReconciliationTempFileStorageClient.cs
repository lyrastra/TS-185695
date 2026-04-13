using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationTempFileStorageClient : BaseCoreApiClient, IReconciliationTempFileStorageClient
    {
        private readonly SettingValue endpoint;

        public ReconciliationTempFileStorageClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            endpoint = settingRepository.Get("PaymentImportHandlerApiEndpoint");
        }
        
        public async Task<string> GetTextAsync(string path)
        {
            return (await GetAsync<ApiDataResult<string>>("/GetText", new { path }))?.data;
        }
        
        public async Task<string> SaveAsync(SaveReconciliationTempFileDto dto)
        {
            return (await PostAsync<SaveReconciliationTempFileDto, ApiDataResult<string>>("", dto).ConfigureAwait(false))?.data;
        }
        
        public Task RemoveAsync(string path)
        {
            return DeleteAsync("", new { path });
        }
        
        protected override string GetApiEndpoint()
        {
            return endpoint.Value + "/private/api/v1/MovementList/Storage/ReconciliationTempFile";
        }
    }
}