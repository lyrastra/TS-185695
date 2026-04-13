using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.Preview.Client
{
    [InjectAsSingleton]
    public class FilePreviewApiClient : BaseApiClient, IFilePreviewApiClient
    {
        private readonly SettingValue apiEndpoint;

        public FilePreviewApiClient(
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

        public Task<int> GetPreviewAsync(int fileId, int pageIndex) => GetAsync<int>($"/Rest/File/{fileId}/preview/{pageIndex}");

        public Task<int> GetPreviewPageCountAsync(int fileId) => GetAsync<int>($"/Rest/File/{fileId}/preview/count");
        
        public Task RotateAsync(int fileId, int pageIndex, int degree)
            => PostAsync($"/Rest/File/{fileId}/preview/{pageIndex}/rotate/{degree}");

        public Task CopyAsync(int fromFileId, int toFileId) => PostAsync($"/Rest/File/{fromFileId}/preview/copy?toFileId={toFileId}");

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/preview";
        }
    }
}