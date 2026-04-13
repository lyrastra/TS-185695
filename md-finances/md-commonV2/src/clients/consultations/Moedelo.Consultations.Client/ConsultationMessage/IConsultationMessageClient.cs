using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Consultations;
using Moedelo.Consultations.Dto.Message;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Consultations.Client.ConsultationMessage
{
    public interface IConsultationMessageClient
    {
        Task<int> SaveOrUpdateMessageAsync(ConsultationMessageDto message);

        /// <summary>
        /// Прикрепить файл к сообщению
        /// </summary>
        /// <param name="messageId">идентификатор сообщения</param>
        /// <param name="userId">идентификатор пользователя, прикрепляющего файл</param>
        /// <param name="file">файл</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns></returns>
        Task AttachFileToMessageAsync(int messageId, int userId, HttpFileStream file, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список файлов, прикреплённых к сообщению
        /// </summary>
        /// <param name="messageId">идентификатор сообщения</param>
        /// <param name="messageType">ожидаемый тип сообщения (будет проверен на сервере)</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<ConsultationMessageAttacheFileInfoDto>> GetAttachedFilesInfoAsync(int messageId,
            ConsultationMessageType messageType, CancellationToken cancellationToken);

        Task DeleteAttachedFileAsync(int messageId, int fileId, ConsultationMessageType messageType, CancellationToken cancellationToken);
        Task<HttpFileStream> DownloadAttachedFileAsync(int messageId, int fileId, CancellationToken cancellationToken);
        Task<HttpFileStream> DownloadAttachedFileByFileIdAndMessageTypeAsync(
            int messageId,
            int fileId,
            ConsultationMessageType messageType,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ConsultationMessageViewModelDto>> GetMessagesViewModelListByThreadAsync(
            int threadId,
            CancellationToken cancellationToken);
    }
}