using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.AdvanceStatement
{
    public class AdvanceStatementFinancialObjectDto
    {
        public long Id { get; set; }
        
        public long? DocumentBaseId { get; set; }

        public bool IsAdvance { get; set; }

        public AdvancePaymentDocumentTypes FinancialObjectType { get; set; }

        public PrimaryDocumentsTransferDirection Direction { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма платежного документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма связи с АО
        /// </summary>
        public decimal LinkSum { get; set; }

        public string OperationType { get; set; }

        public bool IsFixedAsset { get; set; }

        /// <summary>
        /// Привязан ли документ к командировке в Зарплате.
        /// Имеет значение только для документа на выдачу аванса.
        /// </summary>
        public bool LinkedToBusinessTrip { get; set; }
    }
}