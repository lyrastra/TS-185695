using System.Threading;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.ErptUploadedFiles;

namespace Moedelo.ErptV2.Client.UploadFile
{
    public interface IErptUploadedDocumentsApiClient
    {
        Task<ErptUploadedDocumentMetadataDto[]> GetLinkedUploadedDocumentsMetadataListAsync(
            int reportId,
            ErptUploadedFileType fileType,
            CancellationToken cancellationToken);

        /// <summary>
        /// Выставить значение IsVisible в таблице dbo.NeformalDocumentVisibleCross
        /// по указанному значение UploadedFileId
        /// (какой-то легаси функционал)
        /// Изменения будут произведены только если соответствующая запись существует в таблице
        /// </summary>
        /// <param name="uploadedFileId">значение UploadedFileId для поиска строки в таблице</param>
        /// <param name="isVisible">выставляемое значение IsVisible</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        Task SetNeformalDocumentVisibleCrossValueAsync(int uploadedFileId, bool isVisible,
            CancellationToken cancellationToken);

        /// <summary>
        /// Выставить значение IsVisible в таблице dbo.FormalDocumentVisibleCross
        /// по указанному значение UploadedFileId
        /// (какой-то легаси функционал)
        /// Изменения будут произведены только если соответствующая запись существует в таблице
        /// </summary>
        /// <param name="uploadedFileId">значение UploadedFileId для поиска строки в таблице</param>
        /// <param name="isVisible">выставляемое значение IsVisible</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        Task SetFormalDocumentVisibleCrossValueAsync(int uploadedFileId, bool isVisible,
            CancellationToken cancellationToken);
    }
}
