using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.AdvanceStatement
{
    public class AdvancePaymentDocumentDto
    {
        public long Id { get; set; }

        public long DocumentId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public AdvancePaymentDocumentTypes Type { get; set; }

        public AccountingDocumentType AccountingDocumentType { get; set; }

        public PrimaryDocumentsTransferDirection Direction { get; set; }

        /// <summary>
        /// Только для биза. FinancialOperation.Type
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Только для биза.
        /// </summary>
        public bool IsFixedAsset { get; set; }
    }
}