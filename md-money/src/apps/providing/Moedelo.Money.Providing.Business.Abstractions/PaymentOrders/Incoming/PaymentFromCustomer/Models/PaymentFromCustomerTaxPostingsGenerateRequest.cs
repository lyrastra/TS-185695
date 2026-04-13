using System;
using System.Collections.Generic;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models
{
    public class PaymentFromCustomerTaxPostingsGenerateRequest
    {
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
        public bool MediationIncludeNds { get; set; }
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
        /// Связанные документы
        /// </summary>
        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}
