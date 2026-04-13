using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Events
{
    public class AccrualOfInterestUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}