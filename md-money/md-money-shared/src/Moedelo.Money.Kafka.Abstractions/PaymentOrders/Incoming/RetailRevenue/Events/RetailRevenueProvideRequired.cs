using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Events
{
    public class RetailRevenueProvideRequired : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public DateTime SaleDate { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }
        
        /// <summary>
        /// НДС по комиссии (не по сумме операции!)
        /// </summary>
        public Nds Nds { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Комиссия (эквайринг)
        /// </summary>
        public decimal? AcquiringCommissionSum { get; set; }

        /// <summary>
        /// Дата комиссии
        /// </summary>
        public DateTime? AcquiringCommissionDate { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }

        /// <summary>
        /// Идентификатор патента (для TaxationSystemType = Patent)
        /// </summary>
        public long? PatentId { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: посреднечество
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }
    }
}