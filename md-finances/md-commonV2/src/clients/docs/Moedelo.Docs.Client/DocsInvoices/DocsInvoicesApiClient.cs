using System.Threading.Tasks;
using Moedelo.Docs.Dto.DocsInvoices;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsInvoices
{
    [InjectAsSingleton]
    public class DocsInvoicesApiClient : BaseCoreApiClient, IDocsInvoicesApiClient
    {
        private const string prefix = "/api/v1";
        private readonly ISettingRepository settingRepository;

        public DocsInvoicesApiClient(
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
            return settingRepository.Get("InvoicesApiEndpoint").Value;
        }

        public async Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, InvoiceFileRequestDto requestDto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{requestDto.BaseId}/pdf?useStampAndSign={requestDto.UseStampAndSign}&includeContractorCopy={requestDto.IncludeContractorCopy}";

            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<HttpFileModel> DownloadDocFileAsync(int firmId, int userId, InvoiceFileRequestDto requestDto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{requestDto.BaseId}/doc?useStampAndSign={requestDto.UseStampAndSign}&includeContractorCopy={requestDto.IncludeContractorCopy}";

            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}