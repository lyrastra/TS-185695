using Moedelo.ErptV2.Dto.EdsSmev;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto;

namespace Moedelo.ErptV2.Client.EdsSmev
{
    [InjectAsSingleton]
    public class EdsSmevClient : BaseApiClient, IEdsSmevClient
    {
        private readonly SettingValue apiEndpoint;

        public EdsSmevClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<PaginationResponse<EdsSmevFailure>> GetEdsSmevFailuresAsync(PaginationRequest request)
        {
            return PostAsync<PaginationRequest, PaginationResponse<EdsSmevFailure>>("/EdsSmev/GetEdsSmevFailures", request);
        }
    }
}
