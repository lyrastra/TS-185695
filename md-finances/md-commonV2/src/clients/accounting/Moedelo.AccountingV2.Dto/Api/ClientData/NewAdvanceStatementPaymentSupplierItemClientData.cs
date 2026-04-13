using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    /// <summary>
    /// Авансовый отчет. Таблица. Вкладка "Оплата поставщикам"
    /// </summary>
    public class NewAdvanceStatementPaymentSupplierItemClientData
    {
        public long Id { get; set; }

        public long? ConfirmationDocumentId { get; set; }

        public long? StatementOfFixedAssetId { get; set; }

        public string DocumentName { get; set; }

        public int? KontragentId { get; set; }

        public string KontragentName { get; set; }

        public string PaymentDocument { get; set; }

        public decimal ReportedSum { get; set; }

        public decimal AcceptedSum { get; set; }

        public AdvanceStatementItemDataType ExpenditureType { get; set; }

        public string PaymentDocumentNumber { get; set; }

        public string PaymentDocumentDate { get; set; }

        public Decimal KudirSum { get; set; }

        public string KudirDescription { get; set; }

        /// <summary>
        /// id связанного документа
        /// </summary>
        public long? RelatedDocumentBaseId { get; set; }

        /// <summary>
        /// Номер связанного документа
        /// </summary>
        public string RelatedDocumentNumber { get; set; }

        /// <summary>
        /// Дата связанного документа
        /// </summary>
        public string RelatedDocumentDate { get; set; }

        /// <summary>
        /// Сумма в связанном документе
        /// </summary>
        public decimal? RelatedDocumentSum { get; set; }

        /// <summary>
        /// Тип связанного документа
        /// </summary>
        public AdvanceStatementConfirmingDocumentType RelatedDocumentType { get; set; }

        public decimal UnpaidSum { get; set; }
    }
}