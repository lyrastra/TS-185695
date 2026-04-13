using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.Docs
{
    /// <summary>
    /// Онписание документа, не проведенного в учете
    /// </summary>
    public class NotProvideDocumentResultDto
    {
        /// <summary>
        /// BaseId
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public AccountingDocumentType Type { get; set; }

        /// <summary>
        /// Направление (входящий/исходящий)
        /// </summary>
        public PrimaryDocumentsTransferDirection Direction { get; set; }

        /// <summary>
        /// В документе только услуги
        /// </summary>
        public bool HasOnlyServices { get; set; }
    }
}