using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class FinancialDocumentClientData
    {
        public string Date { get; set; }

        public PrimaryDocumentsTransferDirection Direction { get; set; }

        public long? DocumentBaseId { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public AdvancePaymentDocumentTypes Type { get; set; }

        public long Id { get; set; }

        public string OperationType { get; set; }

        public bool IsFixedAsset { get; set; }

        /// <summary>
        /// Привязан ли документ к командировке в Зарплате.
        /// Имеет значение только для документа на выдачу аванса.
        /// </summary>
        public bool LinkedToBusinessTrip { get; set; }
    }
}