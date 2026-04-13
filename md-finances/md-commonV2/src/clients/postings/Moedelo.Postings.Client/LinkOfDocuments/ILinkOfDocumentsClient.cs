using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.Postings.Dto;

namespace Moedelo.Postings.Client.LinkOfDocuments
{
    public interface ILinkOfDocumentsClient : IDI
    {
        /// <summary>
        /// Обновить указанные односторонние связи между документами
        /// </summary>
        Task UpdateLinksAsync(int firmId, int userId, OneWayLinksUpdateRequestDto requestDto);

        /// <summary>
        /// Обновить указанные двусторонние связи между документами
        /// </summary>
        Task UpdateLinksAsync(int firmId, int userId, IList<TwoWayLinkOfDocumentsDto> links);

        Task DeleteLinksAsync(int firmId, int userId, IList<TwoWayLinkOfDocumentsIdDto> linkIds);

        Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, long documentBaseId);

        Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds,
            bool useReadOnlyDb = false,
            HttpQuerySetting httpQuerySettings = null,
            CancellationToken cancellationToken = default);

        Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, long documentBaseId, LinkType linkType);

        Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, LinkType linkType);

        Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, LinksFromRequestDto request);

        Task<List<LinkOfDocumentsDto>> GetLinksToAsync(int firmId, int userId, long documentBaseId);

        Task<List<LinkOfDocumentsDto>> GetLinksToAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task<List<LinkOfDocumentsDto>> GetLinksToAsync(int firmId, int userId,
            IReadOnlyCollection<long> baseIds, LinkType linkType,
            bool useReadOnlyDb = false,
            HttpQuerySetting httpQuerySettings = null,
            CancellationToken cancellationToken = default);

        Task ReplaceAllForDocumentAsync(ReplaceAllForDocumentRequestDto dto, int firmId, int userId);

        Task CreateMultipleAsync(int firmId, int userId,
            IReadOnlyCollection<ReplaceAllForDocumentRequestDto> dtos);

        /// <summary>
        /// Возвращает количество связных документов для указанных документов
        /// При подсчёте не учитываются "фейковые" документы типа основной договор, командировка и т.п.
        /// </summary>
        Task<List<DocumentLinksCountDto>> CountRealDocumentsLinksFromAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Удалить все связи указанного документа
        /// </summary>
        Task DeleteAllDocumentLinksAsync(int firmId, int userId, long documentBaseId);

        Task DeleteAllDocumentLinksAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Удалить связи (двусторонние и односторонние) с документами указанного типа
        /// </summary>
        Task DeleteLinksWithDocOfTypeAsync(int firmId, int userId, DeleteLinksWithDocOfTypeRequestDto requestDto);

        /// <summary>
        /// Удалить связи (двусторонние и односторонние) с документами по прямой связи указанного типа
        /// </summary>
        Task DeleteLinksWithDocByLinkTypeAsync(int firmId, int userId, DeleteLinksWithDocByLinkTypeRequestDto requestDto);
    }
}