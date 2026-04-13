using System;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccPostings.Client
{
    public interface IAccountingPostingsClient : IDI
    {
        /// <summary>
        /// Получить все проводки по base-идентификаторов документов
        /// </summary>
        [Obsolete("Используйте GetByAsync criteria.DocumentBaseIds + есть возможность использовать реплику")]
        Task<List<AccountingPostingDto>> GetAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Получить все проводки по указанному критерию поиска
        /// </summary>
        Task<List<AccountingPostingDto>> GetByAsync(int firmId, int userId, AccountingPostingsSearchCriteriaDto criteria, HttpQuerySetting setting = null);

        /// <summary>
        /// Получить все проводки по base-идентификатору документа
        /// </summary>
        Task<List<AccountingPostingDto>> GetAsync(int firmId, int userId, long baseId);

        Task SaveAsync(int firmId, int userId, IReadOnlyCollection<AccountingPostingDto> postings, HttpQuerySetting setting = null);

        Task DeleteByDocumentAsync(int firmId, int userId, long documentBaseId);

        Task DeleteByDocumentAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task DeleteByDocumentAndOperationTypeAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, OperationType operationType);

        /// <summary>
        /// Объединяет счета и субконто в проводках, заменяя вторичные на первичный
        /// </summary>
        Task MergeAsync(int firmId, int userId, long primarySubcontoId, IReadOnlyCollection<long> secondarySubcontoIds, SyntheticAccountCode primaryDebitCode);

        /// <summary>
        /// Удаляет проводки, у которых ОТСУТСТВУЕТ ССЫЛКА на документ-источник (AccountingOperation),
        /// т. е. "грязные" данные, нельзя удалить иным образом.
        /// </summary>
        Task DeleteOrphanedRecordsAsync(int firmId, int userId, DeleteOrphanedRecordsRequestDto request);
    }
}