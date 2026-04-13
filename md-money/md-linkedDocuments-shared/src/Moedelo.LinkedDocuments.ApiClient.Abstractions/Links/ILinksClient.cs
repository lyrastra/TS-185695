using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.Links
{
    public interface ILinksClient
    {
        /// <summary>
        /// Для документа: возвращает связи и базовое представление связанных документов 
        /// </summary>
        Task<LinkWithDocumentDto[]> GetLinksWithDocumentsAsync(
            long documentBaseId,
            bool useReadOnly = false,
            HttpQuerySetting setting = null,
            CancellationToken ct = default);
        
        /// <summary>
        /// Для списка документов: возвращает связи и базовое представление связанных документов
        /// Результат сгруппирован по baseId исходных документов
        /// </summary>
        Task<Dictionary<long, LinkWithDocumentDto[]>> GetLinksWithDocumentsAsync(
            IReadOnlyCollection<long> documentBaseIds,
            bool useReadOnly = false,
            HttpQuerySetting setting = null,
            CancellationToken ct = default);
        
        /// <summary>
        /// Для списка документов (FromBaseId: возвращает ссылки с документами заданных типов (плоская модели)
        /// </summary>
        Task<List<LinkDto>> GetLinksByLinkedDocumentTypesAsync(LinksFromBaseIdsByToDocumentTypesRequestDto request);

        /// <summary>
        /// Возвращает кол-во связей документов из списка
        /// </summary>
        Task<Dictionary<long, int>> GetLinksCountAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}