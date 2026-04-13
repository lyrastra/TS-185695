using Moedelo.AccountingStatements.Enums;
using System;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.PaymentForDocuments.Events
{
    /// <summary>
    /// Событие по созданию бухсправки "Признание предоплаты оплатой"
    /// </summary>
    public class PaymentForDocumentCreatedMessage
    {
        /// <summary>
        /// DocumentBaseId бухсправки
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата бухсправки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма бухсправки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Ид контрагента из п/п и первичного документа
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// DocumentBaseId договора из п/п и первичного документа
        /// </summary>
        public long? ContractBaseId { get; set; }

        /// <summary>
        /// DocumentBaseId входящего платежа
        /// </summary>
        public long PaymentBaseId { get; set; }

        /// <summary>
        /// DocumentBaseId первичного документа (покупки)
        /// </summary>
        public long PrimaryDocBaseId { get; set; }

        public PaymentForDocumentType Type { get; set; }
    }
}