using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.Notification
{
    [InjectAsSingleton]
    public class ErptNotificationClient : BaseApiClient, IErptNotificationClient
    {
        private readonly SettingValue apiEndpoint;
        
        public ErptNotificationClient(
           IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
           : base(
               httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task FormalDocumentCreated(int firmId, int userId, int documentId, int versionId)
        {
            return PostAsync($"/ErptNotification/FormalDocumentCreated?firmId={firmId}&userId={userId}&documentId={documentId}&versionId={versionId}");
        }

        public Task NeformalDocumentCreated(int firmId, int userId, int documentId)
        {
            return PostAsync($"/ErptNotification/NeformalDocumentCreated?firmId={firmId}&userId={userId}&documentId={documentId}");
        }

        public Task PfrDocumentsCommitedAsync(int firmId, int userId)
        {          
            return PostAsync($"/ErptNotification/PfrDocumentsCommited?firmId={firmId}&userId={userId}");
        }

        public Task PfrEdmStatementNotSignedAsync(int firmId, int userId)
        {
            return PostAsync($"/ErptNotification/PfrEdmStatementNotSigned?firmId={firmId}&userId={userId}");
        }

        public Task PfrDocumentsRejectedAsync(int firmId, int userId)
        {      
            return PostAsync($"/ErptNotification/PfrDocumentsRejected?firmId={firmId}&userId={userId}");
        }      

        public Task FormalDocumentProcessedAsync(int firmId, int userId, int documentId, int versionId)
        {            
            return PostAsync($"/ErptNotification/FormalDocumentProcessed?firmId={firmId}&userId={userId}&documentId={documentId}&versionId={versionId}");
        }

        public Task FormalDocumentRejectedByAdminAsync(int firmId, int userId, int documentId, int versionId)
        {
            return PostAsync($"/ErptNotification/FormalDocumentRejectedByAdmin?firmId={firmId}&userId={userId}&documentId={documentId}&versionId={versionId}");
        }

        public Task LetterReceivedAsync(int firmId, int userId, int documentId)
        {
            return PostAsync($"/ErptNotification/LetterReceived?firmId={firmId}&userId={userId}&documentId={documentId}");
        }

        public Task EdsDocumentsRejectedAsync(int firmId, int userId, int edsHistoryId)
        {
            return PostAsync($"/ErptNotification/EdsDocumentsRejected?firmId={firmId}&userId={userId}&edsHistoryId={edsHistoryId}");
        }

        public Task FnsCheckResponseReceived(int firmId, int userId, int documentId)
        {
            return PostAsync($"/ErptNotification/FnsCheckResponseReceived?firmId={firmId}&userId={userId}&documentId={documentId}");
        }
    }
}
