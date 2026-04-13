using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment.Events
{
    public class BudgetaryPaymentCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        public string Destination { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        public int? KbkId { get; set; }

        public string KbkNumber { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public BudgetaryPeriod Period { get; set; }

        /// <summary>
        /// получатель платежа
        /// </summary>
        public string Recipient { get; set; }
    }
}
