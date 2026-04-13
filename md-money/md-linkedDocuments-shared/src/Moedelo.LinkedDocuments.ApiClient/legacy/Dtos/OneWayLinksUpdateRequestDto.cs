using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos;
using System.Collections.Generic;

namespace Moedelo.LinkedDocuments.ApiClient.legacy.Dtos
{
    /// <summary>
    /// Запрос на обновление указанных связей
    /// </summary>
    internal class OneWayLinksUpdateRequestDto
    {
        public IReadOnlyCollection<LinkOfDocumentsDto> ToSave { get; set; }
    }
}
