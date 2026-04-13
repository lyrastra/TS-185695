namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Документ
    /// </summary>
    public class CaseUpdateAttachedDocumentWithTypeDto
    {
        /// <summary>
        ///     Идентификатор документа
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Название документа, может быть изменено специалистов в процессе обработки документа
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        ///     Тип прикрепленного документа
        /// </summary>
        public DocumentType DocumentType { get; set; }
    }
}