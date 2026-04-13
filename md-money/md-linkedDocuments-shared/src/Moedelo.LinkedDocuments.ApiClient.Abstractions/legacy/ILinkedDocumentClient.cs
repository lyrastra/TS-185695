using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.LinkedDocuments.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/79ec8a20bf319c8cb391c11bf3971342ee408b46/src/clients/postings/Moedelo.Postings.Client/LinkedDocument/ILinkedDocumentClient.cs#L13
    /// </summary>
    public interface ILinkedDocumentClient
    {
        /// <summary>
        /// Получение списка базовых документов по их идентифиаторам
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="ids">идентификаторы базовых документов</param>
        /// <param name="useReadonlyDb">Перевести запросы на реплику (базу для чтения)</param>
        /// <returns></returns>
        Task<List<LinkedDocumentDto>> GetByIdsAsync(
            FirmId firmId, 
            UserId userId, 
            IReadOnlyCollection<long> ids, 
            bool useReadonlyDb = false);
        
        /// <summary>
        /// ПОлучение списка базовых документов, созданных указанным пользователем
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="useReadonlyDb">использовать реплику БД</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>список документов</returns>
        Task<List<LinkedDocumentDto>> GetCreatedByUserAsync(FirmId firmId,
            UserId userId,
            bool useReadonlyDb,
            CancellationToken cancellationToken);
        
        Task<long> CreateOrUpdateAsync(FirmId firmId, UserId userId, LinkedDocumentSaveRequestDto saveRequest);
        
        Task DeleteAsync(FirmId firmId, UserId userId, long id);
        
        Task DeleteAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> ids);
        
        /// <summary>
        /// Массовое обновление НУ-статусов документов
        /// </summary>
        Task UpdateTaxStatusesAsync(FirmId firmId, UserId userId, IReadOnlyDictionary<long, TaxPostingStatus> docTaxStatusMap);

        /// <summary>
        /// Массовое создание документов
        /// </summary>
        Task<IReadOnlyDictionary<long, long>> CreateMultipleAsync(
            FirmId firmId, UserId userId, IReadOnlyCollection<CreateLinkedDocumentRequestDto> documents, HttpQuerySetting setting = null);
    }
}