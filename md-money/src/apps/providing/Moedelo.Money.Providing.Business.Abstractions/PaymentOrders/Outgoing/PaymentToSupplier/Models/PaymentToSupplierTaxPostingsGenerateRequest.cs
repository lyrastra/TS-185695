using Moedelo.Money.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models
{
    public class PaymentToSupplierTaxPostingsGenerateRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        public string KontragentName { get; set; }

        public bool IncludeNds { get; set; }

        public decimal? NdsSum { get; set; }

        /// <summary>
        /// Связанные документы
        /// </summary>
        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; }

        public bool IsPaid { get; set; }
    }
}
