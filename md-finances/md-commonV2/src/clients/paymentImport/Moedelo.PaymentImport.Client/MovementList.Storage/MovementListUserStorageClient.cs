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
    [InjectAsSingleton(typeof(IMovementListUserStorageClient))]
    public class MovementListUserStorageClient : BaseCoreApiClient, IMovementListUserStorageClient
    {
        private readonly SettingValue endpoint;

        public MovementListUserStorageClient(
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
            return endpoint.Value + "/private/api/v1/MovementList/Storage/User";
        }
        
        public async Task<string> GetFileNameAsync(string path)
        {
            return (await GetAsync<ApiDataResult<string>>($"/GetFileName", new { path }))?.data;
        }

        public async Task<string> SaveAsync(SaveMovementListDto dto)
        {
            return (await PostAsync<SaveMovementListDto, ApiDataResult<string>>("", dto).ConfigureAwait(false))?.data;
        }
        
        public Task<byte[]> GetBytesAsync(string path, CancellationToken cancellationToken)
        {
            return GetAsync<byte[]>("/GetBytes", new { path }, cancellationToken: cancellationToken);
        }
    }
}