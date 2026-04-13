namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    using System;

    /// <summary>
    ///     Обновление обращения (сообщение)
    /// </summary>
    public class CaseUpdateInfoDto
    {
        /// <summary>
        ///     CRM идентификатор обновления обращения
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     CRM идентификатор обращения
        /// </summary>
        public string CaseId { get; set; }

        /// <summary>
        ///     Дата создания обращения
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     Текст обновления
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     CRM идентификатор автора обновления
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        ///     Прикрепленные документы
        /// </summary>
        public CaseUpdateAttachedDocumentWithTypeDto[] Documents { get; set; }
    }
}