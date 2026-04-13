using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    /// <summary>
    /// Авансовый отчет. Таблица. Вкладка "Товары, материалы, услуги"
    /// </summary>
    public class NewAdvanceStatementProductAndMaterialItemClientData
    {
        public AdvanceStatementItemDataType ExpenditureType { get; set; }

        public ExpenditureDataClientData ExpenditureData { get; set; }

        public string PaymentDocument { get; set; }

        public string PaymentDocumentNumber { get; set; }

        public string PaymentDocumentDate { get; set; }

        /// <summary>
        /// Сумма (отчет)
        /// </summary>
        public decimal ReportedSum { get; set; }

        /// <summary>
        /// Сумма (принято)
        /// </summary>
        public decimal AcceptedSum { get; set; }

        public int SyntheticAccount { get; set; }

        public decimal TaxableSum { get; set; }

        public long? ConfirmationDocumentId { get; set; }

        public TaxationSystemType? TaxationSystem { get; set; }
    }
}