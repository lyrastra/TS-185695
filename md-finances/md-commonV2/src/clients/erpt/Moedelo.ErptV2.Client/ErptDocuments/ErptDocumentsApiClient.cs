using System.Threading.Tasks;
using Moedelo.ErptV2.Dto;
using Moedelo.ErptV2.Dto.ErptDocuments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.ErptDocuments
{
    [InjectAsSingleton]
    public class ErptDocumentsApiClient : BaseApiClient, IErptDocumentsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public ErptDocumentsApiClient(
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

        public Task CopyDocumentsAndRelinkFiles(int fromFirmId, int toFirmId, int toUserId) =>
            PostAsync($"/ErptDocuments/CopyDocumentsAndRelinkFiles?fromFirmId={fromFirmId}&toFirmId={toFirmId}&toUserId={toUserId}");

        public Task<BaseDto> SendSmsCodeAsync(int firmId, int userId) =>
            PostAsync<BaseDto>($"/ErptDocuments/SendSmsCode?firmId={firmId}&userId={userId}");

        public Task<BaseDto> GetSessionAsync(int firmId, int userId, string code) =>
            GetAsync<BaseDto>($"/ErptDocuments/GetSession?firmId={firmId}&userId={userId}&code={code}");

        public Task<BaseDto> SendFileAsync(SendFileDto dto) =>
            PostAsync<SendFileDto, BaseDto>($"/ErptDocuments/SendFile", dto);

        public Task SendingChangedAsync(SendingChangedDto dto) =>
            PostAsync($"/ErptDocuments/SendingChanged", dto);

        public Task<int> EvalReturnedToProcessingStatusAsync(int versionId) =>
            GetAsync<int>($"/ErptDocuments/EvalReturnedToProcessingStatus?versionId={versionId}");
    }
}