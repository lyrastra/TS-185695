using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.Excerpt;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.OutSystemsIntegrationV2.Client.NalogRu
{
    [InjectAsSingleton]
    public class NalogRuApiClient : BaseApiClient, INalogRuApiClient
    {
        private readonly SettingValue apiEndpoint;

        public NalogRuApiClient
        (
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository
        ) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("OutSystemsIntegrationApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<string> GetSession()
        {
            return GetAsync<string>("/NalogRu/GetSession");
        }

        public Task<SignedExcerptResponseDto> Query(SignedExcerptQueryRequestDto request)
        {
            return GetAsync<SignedExcerptResponseDto>("/NalogRu/Query", request);
        }

        public Task<SignedExcerptSearchResponseDto> Search(SignedExcerptRequestDto request)
        {
            return GetAsync<SignedExcerptSearchResponseDto>("/NalogRu/Search", request);
        }

        public Task<SignedExcerptResponseDto> Request(SignedExcerptRequestDto request)
        {
            return GetAsync<SignedExcerptResponseDto>("/NalogRu/Request", request);
        }
        
        public Task<bool> IsReady(SignedExcerptRequestDto request)
        {
            return GetAsync<bool>("/NalogRu/IsReady", request);
        }

        public Task<byte[]> Download(SignedExcerptRequestDto request)
        {
            return GetAsync<byte[]>("/NalogRu/Download", request);
        }
        
        public Task<SignedExcerptCaptchaResponseDto> Captcha(string sessionId)
        {
            return GetAsync<SignedExcerptCaptchaResponseDto>("/NalogRu/Captcha", new {sessionId});
        }

        public Task<string> CaptchaResolve(SignedExcerptCaptchaResolveRequestDto request)
        {
            return GetAsync<string>("/NalogRu/CaptchaResolve", request);
        }
    }
}