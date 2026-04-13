using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class AdvancePaymentDocumentClientData
    {
        public long Id { get; set; }

        public long DocumentId { get; set; }

        /// <summary>
        /// Синоним для DocumentBaseId
        /// </summary>
        public long DocumentBaseId
        {
            get => this.DocumentId;
            set => this.DocumentId = value;
        }

        public string Number { get; set; }

        public string Date { get; set; }

        public Decimal Sum { get; set; }

        public AccountingDocumentType DocumentType { get; set; }

        /// <summary>
        /// Синоним для Type
        /// </summary>
        public AdvancePaymentDocumentTypes AdvancePaymentDocumentType
        {
            get => this.Type;
            set => this.Type = value;
        }

        public AdvancePaymentDocumentTypes Type { get; set; }

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