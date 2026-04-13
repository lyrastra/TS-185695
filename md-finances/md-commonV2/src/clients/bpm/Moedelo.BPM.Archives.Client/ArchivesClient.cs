using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.Archives.Client
{
    [InjectAsSingleton]
    public class ArchivesClient : BaseApiClient, IArchivesClient
    {
        private readonly SettingValue apiEndpoint;

        public ArchivesClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        public Task<bool> CanExtractAsync(int fileId) => GetAsync<bool>($"/Rest/Archive/{fileId}/CanExtract");

        public Task<bool> ExtractAsync(int fileId) => GetAsync<bool>($"/Rest/Archive/{fileId}/Extract");

        public Task<List<CrmDocumentArchiveInfo>> GetExtractedFilesAsync(int fileId)
            => GetAsync<List<CrmDocumentArchiveInfo>>($"/Rest/Archive/{fileId}/GetExtractedFiles");

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/archives";
        }
    }
}