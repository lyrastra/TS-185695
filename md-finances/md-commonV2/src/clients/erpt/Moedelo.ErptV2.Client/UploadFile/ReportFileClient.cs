using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.UploadFile
{
    [InjectAsSingleton]
    public class ReportFileClient : BaseApiClient, IReportFileClient
    {
        private readonly SettingValue apiEndpoint;

        public ReportFileClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator,
            responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<FileDto> GetFileAsync(int fileId)
        {
            return GetAsync<FileDto>($"/ReportFile/GetFile?fileId={fileId}");
        }

        public Task<int> UploadFormalDocumentIncomingFileAsync(FileDto fileDto)
        {
            return PostAsync<FileDto, int>($"/ReportFile/UploadFormalDocumentIncomingFile", fileDto);
        }

        public Task UploadFormalDocumentOutgoingFileWithVisibleCrossAsync(FileDto fileDto,
            CancellationToken cancellationToken)
        {
            return PostAsync($"/ReportFile/UploadFormalDocumentOutgoingFile?withVisibleCross=true", fileDto, cancellationToken: cancellationToken);
        }

        public Task<int> UploadNonFormalDocumentIncomingFileAsync(FileDto fileDto)
        {
            return PostAsync<FileDto, int>($"/ReportFile/UploadNonFormalDocumentIncomingFile", fileDto);
        }
    }
}