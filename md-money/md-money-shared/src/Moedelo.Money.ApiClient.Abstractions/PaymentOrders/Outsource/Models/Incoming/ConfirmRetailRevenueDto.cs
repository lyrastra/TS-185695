using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming
{
    /// <summary>
    /// Розничная выручка: подтвердить операцию в "Массовой работе с выписками" 
    /// </summary>
    [OperationType(OperationType.MemorialWarrantRetailRevenue)]
    public class ConfirmRetailRevenueDto : IConfirmPaymentOrderDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
        public string Description { get; set; }
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Дата отгрузки (для организаций)
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Комиссия (эквайринг)
        /// </summary>
        public decimal? AcquiringCommissionSum { get; set; }

        /// <summary>
        /// Дата комиссии
        /// </summary>
        public DateTime? AcquiringCommissionDate { get; set; }

        /// <summary>
        /// НДС комиссии (эквайринга)
        /// </summary>
        public NdsDto AcquiringCommissionNds { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Признак: посредничество
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Идентификатор патента (для TaxationSystemType = Patent)
        /// </summary>
        public long? PatentId { get; set; }
    }
}