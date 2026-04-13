using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models
{
    public class PaymentFromCustomerProvideRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int SettlementAccountId { get; set; }

        public int KontragentId { get; set; }

        public string KontragentName { get; set; }

        /// <summary>
        /// По договору 
        /// </summary>
        public long? ContractBaseId { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public bool IncludeNds { get; set; }
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// НДС для посредничества
        /// </summary>
        public decimal? MediationNdsSum { get; set; }

        /// <summary>
        /// Признак посредничества
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Вознаграждение посредника
        /// </summary>
        public decimal? MediationCommissionSum { get; set; }

        /// <summary>
        /// Счета
        /// </summary>
        public IReadOnlyCollection<BillLink> BillLinks { get; set; }

        /// <summary>
        /// Связанные документы
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

        public TaxationSystemType TaxationSystemType { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        public bool IsMainKontragent { get; set; }

        /// <summary>
        /// Признак: проводится ли в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public bool IsManualTaxPostings { get; set; }

        public bool IsBadOperationState { get; set; }
        
        /// <summary>
        /// Идентификатор состояния проведения операции
        /// </summary>
        public long ProvidingStateId { get; set; }

        /// <summary>
        /// Какое событие обрабатывается
        /// </summary>
        public HandleEventType EventType { get; set; }
    }
}
