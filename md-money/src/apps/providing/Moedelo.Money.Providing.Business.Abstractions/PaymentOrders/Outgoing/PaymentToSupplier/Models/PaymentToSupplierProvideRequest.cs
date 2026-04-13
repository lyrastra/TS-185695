using Moedelo.Money.Providing.Business.Abstractions.Models;
using System;
using System.Collections.Generic;
using Moedelo.Money.Providing.Business.Abstractions.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models
{
    public class PaymentToSupplierProvideRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// По договору 
        /// </summary>
        public long? ContractBaseId { get; set; }

        public decimal Sum { get; set; }

        public bool IncludeNds { get; set; }

        public decimal? NdsSum { get; set; }

        /// <summary>
        /// Список первичных документов
        /// </summary>
        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; }

        /// <summary>
        /// Связанные счета-фактуры
        /// </summary>
        public IReadOnlyCollection<InvoiceLink> InvoiceLinks { get; set; }

        /// <summary>
        /// Резерв (данная величина уменьшает сумму, покрываемую первичными документами)
        /// </summary>
        public decimal? ReserveSum { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        public bool IsMainKontragent { get; set; }

        /// <summary>
        /// Признак: проводится ли в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        public bool IsManualTaxPostings { get; set; }

        public bool IsBadOperationState { get; set; }

        public long ProvidingStateId { get; set; }
        
        /// <summary>
        /// Какое событие обрабатывается
        /// </summary>
        public HandleEventType EventType { get; set; }
    }
}
