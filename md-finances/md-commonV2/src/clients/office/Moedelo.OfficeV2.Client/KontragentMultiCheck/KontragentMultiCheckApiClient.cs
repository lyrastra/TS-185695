using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.OfficeV2.Dto.KontragentMultiCheck;

namespace Moedelo.OfficeV2.Client.KontragentMultiCheck
{
    [InjectAsSingleton]
    public class KontragentMultiCheckApiClient : BaseApiClient, IKontragentMultiCheckApiClient
    {
        private const string ControllerUri = "/Rest/KontragentMultiCheck/";

        private readonly ISettingRepository settingRepository;

        public KontragentMultiCheckApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("OfficePrivateApiEndpoint") + ControllerUri;
        }

        /// <inheritdoc />
        public async Task<MultiCheckResponseDto> CheckAsync(int firmId, int userId, MultiCheckRequestDto request)
        {
            var result = await PostAsync<MultiCheckRequestDto, DataDto<MultiCheckResponseDto>>(
                $"CheckAsync?firmId={firmId}&userId={userId}", 
                request).ConfigureAwait(false);

            return result.Data;
        }

        /// <inheritdoc />
        public async Task<MultiCheckResponseDto> GetCheckResultAsync(int firmId, int userId, long requestId)
        {
            var result = await GetAsync<DataDto<MultiCheckResponseDto>>(
                $"GetCheckResult?firmId={firmId}&userId={userId}&requestId={requestId}").ConfigureAwait(false);
            return result.Data;
        }

        /// <inheritdoc />
        public async Task<MultiCheckResponseDto> CheckAsync(MultiCheckRequestDto request)
        {
            var result = await PostAsync<MultiCheckRequestDto, DataDto<MultiCheckResponseDto>>(
                "Check",
                request).ConfigureAwait(false);

            return result.Data;
        }
    }
}