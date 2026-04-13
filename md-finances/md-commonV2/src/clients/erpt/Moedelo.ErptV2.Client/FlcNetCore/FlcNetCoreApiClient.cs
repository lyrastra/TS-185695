using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.FlcNetCore;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.FlcNetCore
{
    [InjectAsSingleton]
    public class FlcNetCoreApiClient : BaseCoreApiClient, IFlcNetCoreApiClient
    {
        private readonly SettingValue apiEndpoint;

        public FlcNetCoreApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FlcPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<Response> CheckFilesAsync(Request request)
        {
            return PostAsync<Request, Response>($"/api/v1/Flc/CheckFiles", request);
        }
    }
}