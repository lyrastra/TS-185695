using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands
{
    public class ImportWithMissingEmployeePaymentToAccountablePerson : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        /// <summary>
        /// Правила импорта применённые к операции
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак необходисти не учитывать номер текущей платежки
        /// при генерировании нового номера
        /// </summary>
        public bool IsIgnoreNumber { get; set; }

        /// <summary>
        /// ИНН получателя платежа
        /// </summary>
        public string PayeeInn { get; set; }

        /// <summary>
        /// Счёт получателя платежа
        /// </summary>
        public string PayeeAccount { get; set; }

        /// <summary>
        /// ФИО получателя платежа
        /// </summary>
        public string PayeeName { get; set; }
        
        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
