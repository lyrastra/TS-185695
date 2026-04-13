using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models
{
    class PaymentToSupplierAccPostingsProvideRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public Kontragent Kontragent { get; set; }

        public bool IsMainKontragent { get; set; }

        public Contract Contract { get; set; }

        public IReadOnlyCollection<BaseDocument> BaseDocuments { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsBadOperationState { get; set; }
    }
}
