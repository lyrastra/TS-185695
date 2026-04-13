using Moedelo.Common.Enums.Enums.Consultations;
using Moedelo.Consultations.Dto.Message;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Moedelo.Consultations.Client.ConsultationMessage
{
    [InjectAsSingleton(typeof(IConsultationMessageClient))]
    public class ConsultationMessageClient : BaseApiClient, IConsultationMessageClient
    {
        private readonly ISettingRepository settingRepository;
        private const string ControllerName = "/ConsultationMessage/";

        public ConsultationMessageClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, 
                auditTracer, 
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("ConsultationsApiEndpoint") + ControllerName;
        }

        public Task<int> SaveOrUpdateMessageAsync(ConsultationMessageDto message)
        {
            return PostAsync<ConsultationMessageDto, int>("SaveOrUpdateMessage", message);
        }

        public Task AttachFileToMessageAsync(int messageId, int userId, HttpFileStream file, CancellationToken cancellationToken)
        {
            var uri = $"{messageId}/file?userId={userId}";

            return SendFileAsync<int>(uri, file, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<ConsultationMessageAttacheFileInfoDto>> GetAttachedFilesInfoAsync(
            int messageId,
            ConsultationMessageType messageType,
            CancellationToken cancellationToken)
        {
            var uri = $"{messageId}/files/metadata?messageType={(int)messageType}";

            return GetAsync<IReadOnlyCollection<ConsultationMessageAttacheFileInfoDto>>(uri,
                cancellationToken: cancellationToken);
        }

        public Task DeleteAttachedFileAsync(int messageId, int fileId, ConsultationMessageType messageType, CancellationToken cancellationToken)
        {
            var uri = $"{messageId}/file/{fileId}?messageType={(int)messageType}";

            return DeleteAsync(uri, cancellationToken: cancellationToken);
        }

        public Task<HttpFileStream> DownloadAttachedFileAsync(int messageId, int fileId, CancellationToken cancellationToken)
        {
            var uri = $"{messageId}/file/{fileId}";

            return DownloadFileByGetMethodAsync(uri, new {}, cancellationToken: cancellationToken);
        }

        public Task<HttpFileStream> DownloadAttachedFileByFileIdAndMessageTypeAsync(int messageId, int fileId, ConsultationMessageType messageType,
            CancellationToken cancellationToken)
        {
            var uri = $"{messageId}/file/{fileId}";

            return DownloadFileByGetMethodAsync(uri, new { messageType },  cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<ConsultationMessageViewModelDto>> GetMessagesViewModelListByThreadAsync(int threadId, CancellationToken cancellationToken)
        {
            var uri = $"viewModels/byThread?threadId={threadId}";

            return GetAsync<IReadOnlyCollection<ConsultationMessageViewModelDto>>(uri,
                cancellationToken: cancellationToken);
        }
    }
}
