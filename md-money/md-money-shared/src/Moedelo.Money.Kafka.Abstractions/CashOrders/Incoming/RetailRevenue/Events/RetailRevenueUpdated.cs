using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue.Events
{
    public class RetailRevenueUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string ZReportNumber { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        public decimal? PaidCardSum { get; set; }

        public ContractorBase Contractor { get; set; }

        /// <summary>
        /// Выбранная СНО в операции
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

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