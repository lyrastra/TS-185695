using Moedelo.Accounting.Enums.Documents;
using System;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements
{
    public class AdvanceStatementPaymentSupplierItemDto
    {
        public AdvanceStatementItemDataType ExpenditureType { get; set; }

        /// <summary>
        /// id связанного документа
        /// </summary>
        [Obsolete]
        public long? RelatedDocumentBaseId => DocumentBaseId;

        /// <summary>
        /// В случае оплаты поставщику - id связанного документа у акта/накладной.
        /// В случае командировки - id связанного документа у счета-фактуры.
        /// </summary>
        public long? DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа об оплате
        /// </summary>
        public string PaymentDocumentNumber { get; set; }

        /// <summary>
        /// Дата документа об оплате
        /// </summary>
        public DateTime? PaymentDocumentDate { get; set; }

        /// <summary>
        /// Сумма (отчет)
        /// </summary>
        public decimal ReportedSum { get; set; }

        /// <summary>
        /// Сумма (принято)
        /// </summary>
        public decimal AcceptedSum { get; set; }

        public int? KontragentId { get; set; }
    }
}
