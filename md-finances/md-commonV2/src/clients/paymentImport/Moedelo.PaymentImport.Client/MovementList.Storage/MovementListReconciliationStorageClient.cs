using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.MovementList.Storage
{
    [InjectAsSingleton]
    public class MovementListReconciliationStorageClient : BaseCoreApiClient, IMovementListReconciliationStorageClient
    {
        private readonly SettingValue endpoint;

        public MovementListReconciliationStorageClient(
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

        public Task<byte[]> GetBytesAsync(string path)
        {
            return GetAsync<byte[]>("/GetBytes", new { path });
        }

        public async Task<string> SaveAsync(SaveMovementListDto dto)
        {
            return (await PostAsync<SaveMovementListDto, ApiDataResult<string>>("", dto).ConfigureAwait(false))?.data;
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value + "/private/api/v1/MovementList/Storage/Reconciliation";
        }
    }
}