using System.Collections.Generic;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos
{
    public class LinksFromBaseIdsByToDocumentTypesRequestDto
    {
        /// <summary>
        /// Документы, для которых нужно найти связи
        /// </summary>
        public IReadOnlyCollection<long> FromBaseIds { get; set; }

        /// <summary>
        /// Связи должны быть с документами заданных типов
        /// </summary>
        public IReadOnlyCollection<LinkedDocumentType> ToDocumentTypes { get; set; }
    }
}