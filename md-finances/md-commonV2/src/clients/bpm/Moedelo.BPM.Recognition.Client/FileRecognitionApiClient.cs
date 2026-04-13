using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.Recognition.Client
{
    [InjectAsSingleton]
    public class FileRecognitionApiClient : BaseApiClient, IFileRecognitionApiClient
    {
        private readonly SettingValue apiEndpoint;

        public FileRecognitionApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        public Task<bool> RecognizeAsync(int fileId) => PostAsync<bool>($"/Rest/Recognition/{fileId}/recognize");

        public async Task<DocumentClass> GetDocumentClassAsync(int fileId)
        {
            var type = await GetAsync<string>($"/Rest/Recognition/{fileId}/class").ConfigureAwait(false);
            DocumentClass documentClass;
            if (!Enum.TryParse(type, true, out documentClass))
            {
                documentClass = DocumentClass.Undefined;
            }

            return documentClass;
        }

        public Task SetDocumentClassAsync(int fileId, DocumentClass documentClass)
            => PostAsync($"/Rest/Recognition/{fileId}/class/{documentClass.ToString()}");


        public Task<string> GetParsedXmlAsync(int fileId) => GetAsync<string>($"/Rest/Recognition/{fileId}");

        public Task<OcrWaybill> GetWaybillAsync(int fileId) => GetAsync<OcrWaybill>($"/Rest/Recognition/{fileId}/waybill");

        public Task<OcrInvoice> GetInvoiceAsync(int fileId) => GetAsync<OcrInvoice>($"/Rest/Recognition/{fileId}/invoice");

        public Task<OcrUpd> GetUpdAsync(int fileId) => GetAsync<OcrUpd>($"/Rest/Recognition/{fileId}/upd");

        public Task CopyXmlAsync(int fromFileId, int toFileId) => PostAsync($"/Rest/Recognition/{fromFileId}/xml/copy?toFileId={toFileId}");

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/recognition";
        }
    }
}