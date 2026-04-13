namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    using System;

    /// <summary>
    ///     Документ в обработке
    /// </summary>
    public class QueueDocumentDto
    {
        /// <summary>
        ///     Идентификатор документа
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Название файла документа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Статус обработки документа
        /// </summary>
        public QueueDocumentStatus? Status { get; set; }

        /// <summary>
        ///     Дата создания документа
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        ///     Документа является ссылкой на облачное хранилище
        /// </summary>
        public bool IsCloudLink { get; set; }
    }
}