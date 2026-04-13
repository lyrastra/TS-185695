using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/33f5cd3/src/clients/accPostings/Moedelo.AccPostings.Client/IAccountingPostingsClient.cs
    /// </summary>
    public interface IAccountingPostingsClient
    {
        /// <summary>
        /// Получить все проводки по base-идентификатору документа
        /// </summary>
        [Obsolete("Используйте GetByAsync criteria.DocumentBaseIds + есть возможность использовать реплику")]
        Task<AccountingPostingDto[]> GetAsync(FirmId firmId, UserId userId, long baseId);

        /// <summary>
        /// Получить все проводки по base-идентификаторов документов
        /// </summary>
        [Obsolete("Используйте GetByAsync criteria.DocumentBaseIds + есть возможность использовать реплику")]
        Task<AccountingPostingDto[]> GetAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Получить все проводки по  указанному критерию поиска
        /// </summary>
        Task<AccountingPostingDto[]> GetByAsync(FirmId firmId, UserId userId, AccountingPostingsSearchCriteriaDto criteria);

        Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<AccountingPostingDto> postings, HttpQuerySetting setting = null);

        Task DeleteByDocumentAsync(FirmId firmId, UserId userId, long documentBaseId);

        Task DeleteByDocumentAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds);

        Task DeleteByDocumentAndOperationTypeAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds, OperationType operationType);
    }
}