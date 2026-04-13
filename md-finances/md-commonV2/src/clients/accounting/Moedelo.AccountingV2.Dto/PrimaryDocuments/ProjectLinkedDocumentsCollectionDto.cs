using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    /// <summary>
    /// Контейнер для коллекции связанных первичных документов по проекту договора.
    /// </summary>
    public class ProjectLinkedDocumentsCollectionDto
    {
        /// <summary>
        /// Список закрывающих документов, связанных с проектом договора.
        /// </summary>
        public List<ProjectLinkedDocumentDto> List { get; set; }
    }
}
