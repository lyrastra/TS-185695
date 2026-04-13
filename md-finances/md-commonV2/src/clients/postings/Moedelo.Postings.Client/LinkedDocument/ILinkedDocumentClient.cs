using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.Postings.Dto;

namespace Moedelo.Postings.Client.LinkedDocument
{
    public interface ILinkedDocumentClient
    {
        Task DeleteAsync(long id, int firmId, int userId);

        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<long> CreateOrUpdateAsync(LinkedDocumentDto dto, int firmId, int userId);

        Task<List<DocumentTypeDto>> GetDocumentTypeListByIdListAsync(IReadOnlyCollection<long> idList,
            int firmId, int userId, bool useReadonlyDb = false, CancellationToken cancellationToken = default);

        Task<List<long>> GetChildIdListByChildDocTypeAndLinkTypeAsync(
            long id,
            int firmId,
            int userId,
            AccountingDocumentType childDocumentType,
            LinkType linkType);

        Task<LinkedDocumentDto> GetByIdAsync(long id, int firmId, int userId);

        /// <summary>
        /// Получение списка базовых документов по их идентифиаторам
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="ids">идентификаторы базовых документов</param>
        /// <param name="useReadonlyDb">Перевести запросы на реплику (базу для чтения)</param>
        /// <param name="httpQuerySettings">Параметры запроса</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns></returns>
        Task<List<LinkedDocumentDto>> GetByIdsAsync(
            int firmId, int userId, IReadOnlyCollection<long> ids, bool useReadonlyDb = false,
            HttpQuerySetting httpQuerySettings = null,
            CancellationToken cancellationToken = default);

        Task<List<PaidSumDocumentDto>> GetPaidSumsForDocumentsAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> documentsBaseIdList,
            AccountingDocumentType excludeDocumentType);
        
        Task<List<LinkedDocumentDto>> GetByTypeAndSumAsync(
            int firmId,
            int userId,
            AccountingDocumentType type,
            decimal? minSum,
            decimal? maxSum);

        Task<List<LinkedDocumentDto>> GetParentDocumentsByBaseId(
            int firmId,
            int userId,
            long documentBaseId);

        Task<List<LinkedDocumentDto>> GetCreatedByUserAsync(int firmId,int userId, bool useReadonlyDb = false, CancellationToken cancellationToken = default);
        
        Task<List<long>> GetBaseIdsByAsync(int firmId, int userId, GetBaseIdsByRequestDto request);
        
        /// <summary>
        /// Массовое обновление НУ-статусов документов
        /// </summary>
        /// <returns></returns>
        Task UpdateTaxStatusesAsync(
            int firmId, 
            int userId, 
            IReadOnlyDictionary<long, TaxPostingStatus> docTaxStatusMap);

        /// <summary>
        /// Массовое создание документов
        /// </summary>
        Task<IReadOnlyDictionary<long, long>> CreateMultipleAsync(
            int firmId, 
            int userId, 
            IReadOnlyCollection<CreateLinkedDocumentRequestDto> documents);
    }
}