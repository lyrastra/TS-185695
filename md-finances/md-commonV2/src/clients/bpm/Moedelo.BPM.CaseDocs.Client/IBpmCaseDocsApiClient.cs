using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BPM.CaseDocs.Client
{
    using System;
    using Dtos;

    public interface IBpmCaseDocsApiClient : IDI
    {
        #region Case
        Task<Guid> CaseCreateAsync(CreateCaseForEmailDto data);

        Task<Guid> CaseCreateAsync(Guid accountId, CreateCaseDto data);

        /// <summary>
        ///     Создание нового обращения или добавление сообщения в существующее обращение.
        ///     В качества автора используется почтовый адрес
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<CreateCaseResultDto> CaseCreateAsync(CreateCaseByContactDto data);

        /// <summary>
        ///     Создание нового обращения или добавление сообщения в существующее обращение.
        ///     В качестве автора используется идентификатор фирмы и пользователя из кабинета Мое Дело
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<CreateCaseResultDto> CreateWithUserIdFirmIdAsync(CreateCaseByUserAndFirmDto data);
        Task<Dictionary<string, int?>> CheckByMessageIdAsync(List<string> data);
        Task CaseAddContactsIdsAsync(string caseId, List<ContactDto> data);
        Task CaseAddContactIdsAsync(string caseId, List<string> data);
        Task CaseAddAccountIdsAsync(string caseId, List<string> data);
        Task CaseCancelAsync(string caseId);
        Task SetCaseTypeAsync(string caseId, RequestModelCaseType type);
        Task<CaseInfoDto[]> CaseGetOpenedAsync(int firmId, DateTime? sinceDate = null, string sinceId = null, int limit = 100, params CaseTypeDto[] types);
        Task<CaseInfoDto[]> CaseGetClosedAsync(int firmId, DateTime? sinceDate = null, string sinceId = null, int limit = 100, params CaseTypeDto[] types);
        Task<CaseInfoDto> CaseGetByIdAsync(int firmId, string caseId);
        Task CaseStateChangeEvent(string caseId, int firmId);
        Task CaseThreadUpdatedEvent(string caseId, string messageId, int firmId);

        #endregion

        #region CaseUpdate

        Task<Guid> CaseUpdateCreateAsync(Guid caseId, CreateCaseUpdateForEmailDto data);

        Task CaseUpdateDataParsedAsync(Guid id, Guid caseId, bool dataParsed);

        /// <summary>
        ///     Получение списка обновлений обращения по идентификатору обращения
        /// </summary>
        /// <param name="firmId">идентификатор фирмы МД</param>
        /// <param name="caseId">CRM идентификатор обращения</param>
        /// <param name="sinceDate">получить обновления с этой даты</param>
        /// <param name="sinceId">получить обновления после этого идентификатора обновления</param>
        /// <param name="limit">количество запрашиваемых обновлений</param>
        /// <returns></returns>
        Task<CaseUpdateInfoDto[]> CaseUpdateGetByCaseIdAsync(int firmId, string caseId, DateTime? sinceDate = null, string sinceId = null, int limit = 20);

        /// <summary>
        /// Получение обновления обращения по идентификатору обновления
        /// </summary>
        /// <param name="firmId">идентификатор фирмы МД</param>
        /// <param name="caseId">CRM идентификатор обращения</param>
        /// <param name="caseUpdateId">CRM идентификатор обновления обращения</param>
        /// <returns></returns>
        Task<CaseUpdateInfoDto> CaseUpdateGetByIdAsync(int firmId, string caseId, string caseUpdateId);

        #endregion

        #region Document

        Task<string> DocumentCreateForCaseAsync(Guid caseId, HttpFileModel file);

        Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
            HttpFileModel file);

        Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
           string url);

        Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
            int firmId, HttpFileModel file);

        Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
            Guid emailId, HttpFileModel file, bool isLost = false);

        Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
            Guid emailId,
            string url);

        Task<string> DocumentCreateForAccountAsync(int firmId, HttpFileModel file);

        Task<string> DocumentCreateForAccountAsync(int firmId, string url);

        Task<string> DocumentCreateForAccountAsync(Guid accountId, HttpFileModel file);

        Task DocumentUpdateLinksAsync(string documentId);

        /// <summary>
        /// Получение списка всех документов
        /// </summary>
        /// <param name="firmId">идентификатор фирмы Мое Дело</param>
        /// <param name="offset">смещение для пейджинга</param>
        /// <param name="limit">запрашиваемое количество документов</param>
        /// <returns></returns>
        Task<DocumentDto[]> DocumentGetAllByFirmIdAsync(int firmId, int limit, int offset);

        /// <summary>
        ///     Получение списка обрабатываемых документов
        /// </summary>
        /// <param name="firmId">идентификатор фирмы Мое Дело</param>
        /// <param name="showDocumentDateFrom">дата, с которой отображать документы в плохих статусах</param>
        /// <returns></returns>
        Task<QueueDocumentDto[]> DocumentGetQueueByFirmIdAsync(int firmId, DateTime? showDocumentDateFrom = null);

        /// <summary>
        ///     Получение списка архивных документов
        /// </summary>
        /// <param name="firmId">идентификатор фирмы Мое Дело</param>
        /// <param name="beginDate">начальная дата создания документа</param>
        /// <param name="endDate">конечная дата создания документа</param>
        /// <returns></returns>
        Task<ArchiveDocumentDto[]> DocumentGetArchiveByFirmIdAsync(int firmId, DateTime? beginDate, DateTime? endDate);

        /// <summary>
        ///     Получение списка документов по списку обновлений обращенмй
        /// </summary>
        /// <param name="caseUpdateIds">идентификаторы обновлений обращений</param>
        /// <returns></returns>
        Task<CaseUpdateAttachedDocumentDto[]> DocumentGetByCaseUpdateIds(string[] caseUpdateIds);

        /// <summary>
        ///     Получение документа по идентификатору
        /// </summary>
        /// <param name="documentId">CRM идентификатор документа</param>
        /// <returns></returns>
        Task<DocumentShortInfoDto> DocumentGetShortAsync(string documentId);

        /// <summary>
        ///     Получение FileId документа по идентификатору
        /// </summary>
        /// <param name="documentId">CRM идентификатор документа</param>
        /// <returns></returns>
        Task<int?> DocumentGetFileIdAsync(string documentId);

        #endregion

        #region Revision

        Task<DocumentScanDto> DocumentRevisionGetShortAsync(string documentId, string revisionId);

        #endregion

        #region File

        Task FileConvertAsync(string documentId);
        Task FileUploadAsync(string documentId, HttpFileModel file);
        Task FileLinkAsync(string documentId, int fileStorageId);

        #endregion

        #region Note

        Task<NoteDto[]> NotesByCaseUpdateIdsAsync(List<Guid> CaseUpdateIds);

        Task<HttpFileModel> GetNoteFile(string noteId);

        #endregion

        #region Task

        Task TaskCreateAsync(Guid accountId, string subject, string description, TaskTypeDto taskType);

        #endregion
    }
}
