namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    using System;

    /// <summary>
    ///     Документ
    /// </summary>
    public class DocumentDto
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
        ///     Название файла, как его загружал пользователь
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     Статус обработки документа
        /// </summary>
        public QueueDocumentStatus? Status { get; set; }

        /// <summary>
        ///     Дата создания документа
        /// </summary>
        public DateTime? DateTime { get; set; }
    }
}