using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    /// <summary>
    /// Документ на выдачу аванса - РКО и Исх. ПП
    /// </summary>
    public class AdvanceDocumentClientData
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public string Date { get; set; }

        public decimal Sum { get; set; }

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

        /// <summary>
        /// Привязан ли документ к командировке в Зарплате.
        /// Имеет значение только для документа на выдачу аванса.
        /// </summary>
        public bool LinkedToBusinessTrip { get; set; }
    }
}