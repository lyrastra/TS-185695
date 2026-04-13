namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    using System;

    /// <summary>
    ///     Архивный обработанный документ
    /// </summary>
    public class ArchiveDocumentDto
    {
        /// <summary>
        ///     Дата документа
        /// </summary>
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        ///     Дата создания документа
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///     Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        ///     Тип документа
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        ///     Сумма документа
        /// </summary>
        public decimal? Sum { get; set; }

        /// <summary>
        ///     Файлы документа
        /// </summary>
        public DocumentScanDto[] Scans { get; set; }

        /// <summary>
        ///     Имя контрагента документа
        /// </summary>
        public string PartnerName { get; set; }

        /// <summary>
        ///     Тип операции документа
        /// </summary>
        public string OperationType { get; set; }
    }
}