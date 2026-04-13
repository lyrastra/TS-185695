using Moedelo.Docs.Dto.Docs;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Common;

namespace Moedelo.Docs.Client.DocsBills
{
    [InjectAsSingleton]
    public class DocsBillsApiClient : BaseCoreApiClient, IDocsBillsApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1";
        private const string useStampAndSignParam = "useStampAndSign";
        private const string useWatermarkParam = "useWatermark";

        public DocsBillsApiClient(
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
            return settingRepository.Get("BillsApiEndpoint").Value;
        }

        public async Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, long id, bool useStampAndSign, bool useWatermark)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{id}/pdf?{useStampAndSignParam}={useStampAndSign}&{useWatermarkParam}={useWatermark}";

            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<HttpFileModel> DownloadDocFileAsync(int firmId, int userId, long id, bool useStampAndSign, bool useWatermark)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{id}/doc?{useStampAndSignParam}={useStampAndSign}&{useWatermarkParam}={useWatermark}";

            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<List<LinkedDocumentDto>> GetLinkedDocumentsAsync(int firmId, int userId, long id)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/{id}/LinkedDocuments";

            var response = await GetAsync<ApiDataResult<List<LinkedDocumentDto>>>(
                uri,
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }
    }
}