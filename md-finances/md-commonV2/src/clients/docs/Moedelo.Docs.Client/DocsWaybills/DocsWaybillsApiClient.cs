using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.Docs;
using Moedelo.Docs.Dto.DocsWaybills;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsWaybills
{
    [InjectAsSingleton]
    public class DocsWaybillsApiClient : BaseCoreApiClient, IDocsWaybillsApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1";
        
        public DocsWaybillsApiClient(
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
            return settingRepository.Get("WaybillsApiEndpoint").Value;
        }

        public async Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, WaybillFileRequestDto requestDto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{requestDto.BaseId}/pdf?useStampAndSign={requestDto.UseStampAndSign}&includeContractorCopy={requestDto.IncludeContractorCopy}&includeLinkedInvoice={requestDto.IncludeLinkedInvoice}";
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) };
            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders,
                setting: settings).ConfigureAwait(false);
        }

        public async Task<HttpFileModel> DownloadDocFileAsync(int firmId, int userId, WaybillFileRequestDto requestDto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Sales/Download/{requestDto.BaseId}/doc?useStampAndSign={requestDto.UseStampAndSign}&includeContractorCopy={requestDto.IncludeContractorCopy}&includeLinkedInvoice={requestDto.IncludeLinkedInvoice}";
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) };
            return await GetFileAsync(
                uri,
                queryHeaders: tokenHeaders,
                setting: settings).ConfigureAwait(false);
        }
        
        public async Task<IReadOnlyList<LinkedDocumentDto>> GetLinkedDocumentsAsync(int firmId, int userId, long id)
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