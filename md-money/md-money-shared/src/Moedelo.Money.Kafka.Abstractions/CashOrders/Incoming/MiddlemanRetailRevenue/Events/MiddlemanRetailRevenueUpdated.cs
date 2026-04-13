using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MiddlemanRetailRevenue.Events
{
    public class MiddlemanRetailRevenueUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        public ContractorBase Contractor { get; set; }

        public long ContractBaseId { get; set; }

        /// <summary>
        /// В т.ч. оплаченно картой
        /// </summary>
        public decimal? PaidCardSum { get; set; }

        /// <summary>
        /// Мое вознаграждение
        /// </summary>
        public decimal? MyReward { get; set; }

        public string Destination { get; set; }

        public string Comment { get; set; }

        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        /// <summary>
        /// Тип кассового ордера, который был до сохранения (если была смена типа)
        /// </summary>
        public OperationType OldOperationType { get; set; }
    }
}