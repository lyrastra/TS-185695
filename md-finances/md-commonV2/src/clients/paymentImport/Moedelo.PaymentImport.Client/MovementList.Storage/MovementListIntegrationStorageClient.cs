using System.Threading;
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
    [InjectAsSingleton(typeof(IMovementListIntegrationStorageClient))]
    public class MovementListIntegrationStorageClient : BaseCoreApiClient, IMovementListIntegrationStorageClient
    {
        private readonly SettingValue endpoint;

        public MovementListIntegrationStorageClient(
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
        
        protected override string GetApiEndpoint()
        {
            return endpoint.Value + "/private/api/v1/MovementList/Storage/Integration";
        }
        
        public async Task<string> GetTextAsync(string path)
        {
            return (await GetAsync<ApiDataResult<string>>("/GetText", new { path }))?.data;
        }

        public Task<byte[]> GetBytesAsync(string path, CancellationToken cancellationToken)
        {
            return GetAsync<byte[]>("/GetBytes", new { path }, cancellationToken: cancellationToken);
        }

        public async Task<string> SaveAsync(SaveMovementListDto dto)
        {
            return (await PostAsync<SaveMovementListDto, ApiDataResult<string>>("", dto).ConfigureAwait(false))?.data;
        }

        public Task RemoveAsync(string path)
        {
            return DeleteAsync("", new { path });
        }

        public Task<MovementFileInfoDto[]> GetAsync(int firmId)
        {
            return GetAsync<MovementFileInfoDto[]>($"/InfoList", new { firmId });
        }
    }
}