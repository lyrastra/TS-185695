using Moedelo.Common.Types;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// Источник https://github.com/moedelo/md-commonV2/blob/79ec8a20bf319c8cb391c11bf3971342ee408b46/src/clients/postings/Moedelo.Postings.Client/LinkOfDocuments/ILinkOfDocumentsClient.cs
    /// </summary>
    public interface ILinkOfDocumentsClient
    {
        /// <summary>
        /// Обновить указанные односторонние связи между документами
        /// </summary>
        Task CreateLinksAsync(FirmId firmId, UserId userId, IReadOnlyCollection<LinkOfDocumentsDto> links, HttpQuerySetting querySetting = null);

        /// <summary>
        /// Обновить указанные двусторонние связи между документами
        /// </summary>
        Task UpdateLinksAsync(FirmId firmId, UserId userId, IReadOnlyCollection<TwoWayLinkOfDocumentsDto> links, HttpQuerySetting querySetting = null);
        
        /// <summary>
        /// Сохранить связи документа (старые значения удаляются)
        /// </summary>
        Task ReplaceAllForDocumentAsync(FirmId firmId, UserId userId, ReplaceAllForDocumentRequestDto saveRequest);
        
        /// <summary>
        /// Удалить все связи указанного документа
        /// </summary>
        Task DeleteAllDocumentLinksAsync(FirmId firmId, UserId userId, long documentBaseId, HttpQuerySetting querySetting = null);

        /// <summary>
        /// Удалить все связи указанных документов
        /// </summary>
        Task DeleteAllDocumentLinksAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds, HttpQuerySetting querySetting = null);

        /// <summary>
        /// Возвращает "прямые" для документа (от него к другим)
        /// </summary>
        Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(FirmId firmId, UserId userId, long documentBaseId);

        /// <summary>
        /// Возвращает "прямые" ссылки для документов (от него к другим) с заданным типом
        /// </summary>
        Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(
            FirmId firmId,
            UserId userId,
            IReadOnlyCollection<long> baseIds,
            LinkType linkType,
            bool useReadOnly = false,
            CancellationToken ct = default);

        /// <summary>
        /// Возвращает "обратные" ссылки для документов (от других к нему) с заданным типом
        /// </summary>
        Task<List<LinkOfDocumentsDto>> GetLinksToAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds, LinkType linkType);
        
        /// <summary>
        /// Удалить связи (двусторонние и односторонние) с документами указанного типа
        /// </summary>
        Task DeleteLinksWithDocOfTypeAsync(FirmId firmId, UserId userId, DeleteLinksWithDocOfTypeRequestDto requestDto);
    }
}
