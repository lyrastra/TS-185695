using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.ErptV2.Client.Notification
{
    public interface IErptNotificationClient : IDI
    {
        Task FormalDocumentCreated(int firmId, int userId, int documentId, int versionId);

        Task NeformalDocumentCreated(int firmId, int userId, int documentId);

        Task EdsDocumentsRejectedAsync(int firmId, int userId, int edsHistoryId);

        Task PfrDocumentsCommitedAsync(int firmId, int userId);

        Task PfrEdmStatementNotSignedAsync(int firmId, int userId);

        Task PfrDocumentsRejectedAsync(int firmId, int userId);

        Task FormalDocumentProcessedAsync(int firmId, int userId, int documentId, int versionId);

        Task FormalDocumentRejectedByAdminAsync(int firmId, int userId, int documentId, int versionId);

        Task LetterReceivedAsync(int firmId, int userId, int documentId);

        Task FnsCheckResponseReceived(int firmId, int userId, int documentId);
    }
}
