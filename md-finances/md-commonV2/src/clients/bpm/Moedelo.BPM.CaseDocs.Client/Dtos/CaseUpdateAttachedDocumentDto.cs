namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Документ
    /// </summary>
    public class CaseUpdateAttachedDocumentDto
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
        ///     Идентификатор обновления обращения, к которому прикреплен документ
        /// </summary>
        public string CaseUpdateId { get; set; }
    }
}