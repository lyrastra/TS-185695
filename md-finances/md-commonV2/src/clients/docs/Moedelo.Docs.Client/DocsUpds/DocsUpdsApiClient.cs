using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.DocsUpds;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsUpds
{
    [InjectAsSingleton]
    public class DocsUpdsApiClient : BaseCoreApiClient, IDocsUpdsApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1";
        
        public DocsUpdsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("UpdsApiEndpoint").Value;
        }

        public async Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, UpdFileRequestDto requestDto, HttpQuerySetting setting = null, CancellationToken ct = default)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, ct).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{requestDto.BaseId}/pdf?useStampAndSign={requestDto.UseStampAndSign}";

            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders,
                setting: setting,
                cancellationToken: ct).ConfigureAwait(false);
        }

        public async Task<HttpFileModel> DownloadDocFileAsync(int firmId, int userId, UpdFileRequestDto requestDto, HttpQuerySetting setting = null, CancellationToken ct = default)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, ct).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{requestDto.BaseId}/doc?useStampAndSign={requestDto.UseStampAndSign}";

            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders,
                setting: setting,
                cancellationToken: ct).ConfigureAwait(false);
        }
    }
}