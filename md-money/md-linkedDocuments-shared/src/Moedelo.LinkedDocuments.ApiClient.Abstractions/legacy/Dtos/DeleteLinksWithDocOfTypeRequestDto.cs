using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos
{
    public class DeleteLinksWithDocOfTypeRequestDto
    {
        /// <summary>
        /// Идентификатор документа, для которого нужно удалить связи
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Тип связанного документа
        /// </summary>
        public LinkedDocumentType LinkedDocumentType { get; set; }
    }
}