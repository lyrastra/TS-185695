using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsMiddlemanReports
{
    [InjectAsSingleton]
    public class DocsMiddlemanReportsApiClient : BaseCoreApiClient, IDocsMiddlemanReportsApiClient
    {
        private const string prefix = "/api/v1";
        
        private readonly ISettingRepository settingRepository;

        public DocsMiddlemanReportsApiClient(
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
            return settingRepository.Get("MiddlemanReportsApiEndpoint").Value;
        }
        
        public Task<HttpFileModel> DownloadPdfAsync(int firmId, int userId, long baseId)
        {
            return GetFileAsync(firmId, userId, baseId, "pdf");
        }

        public Task<HttpFileModel> DownloadDocAsync(int firmId, int userId, long baseId)
        {
            return GetFileAsync(firmId, userId, baseId, "doc");
        }

        private async Task<HttpFileModel> GetFileAsync(int firmId, int userId, long baseId, string format)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{baseId}/{format}";

            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}