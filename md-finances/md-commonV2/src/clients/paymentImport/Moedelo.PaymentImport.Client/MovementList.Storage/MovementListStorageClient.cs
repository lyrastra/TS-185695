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
    [InjectAsSingleton(typeof(IMovementListStorageClient))]
    public class MovementListStorageClient: BaseCoreApiClient, IMovementListStorageClient
    {
        private readonly SettingValue endpoint;
        
        public MovementListStorageClient(
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
            return endpoint.Value + "/private/api/v1/MovementList/Storage";
        }
        
        public Task<MovementFileInfoDto[]> GetAsync(int firmId, int count, CancellationToken cancellationToken)
        {
            return GetAsync<MovementFileInfoDto[]>($"/InfoList", new { firmId, count },
                cancellationToken: cancellationToken);
        }
    }
}