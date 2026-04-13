using System;
using System.Collections.Generic;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models
{
    internal class PaymentFromCustomerPostingsBusinessModel
    {
        /// <summary>
        /// СНУ из реквизитов
        /// </summary>
        public TaxationSystemType TaxationSystem { get; set; }

        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        public string KontragentName { get; set; }

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

        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        public IReadOnlyCollection<SalesStatement> Statements { get; set; } = Array.Empty<SalesStatement>();

        public IReadOnlyCollection<SalesWaybill> Waybills { get; set; } = Array.Empty<SalesWaybill>();

        public IReadOnlyCollection<SalesUpd> Upds { get; set; } = Array.Empty<SalesUpd>();
    }
}
