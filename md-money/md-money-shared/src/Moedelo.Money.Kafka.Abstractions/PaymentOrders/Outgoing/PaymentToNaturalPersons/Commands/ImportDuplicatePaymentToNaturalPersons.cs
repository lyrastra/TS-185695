using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands
{
    public class ImportDuplicatePaymentToNaturalPersons : IEntityCommandData
    {
        public long DuplicateId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public PaymentToNaturalPersonsType PaymentType { get; set; }
        
        public decimal PaymentSum { get; set; }

        public Employee Employee { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        

        /// <summary>
        /// Правило импорта применённое к операции
        /// </summary>
        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
