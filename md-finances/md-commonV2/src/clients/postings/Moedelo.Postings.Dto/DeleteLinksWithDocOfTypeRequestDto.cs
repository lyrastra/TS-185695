using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Postings.Dto
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
        public AccountingDocumentType LinkedDocumentType { get; set; }
    }
}