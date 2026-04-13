using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Commands
{
    public class ImportBudgetaryPayment : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        public int? KbkId { get; set; }

        public string KbkNumber { get; set; }

        public BudgetaryPaymentType PaymentType { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public BudgetaryPeriod Period { get; set; }

        public BudgetaryPayerStatus PayerStatus { get; set; }

        public BudgetaryPaymentBase PaymentBase { get; set; }

        public string DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public string Uin { get; set; }

        public BudgetaryRecipient Recipient { get; set; }

        public int? TradingObjectId { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

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
