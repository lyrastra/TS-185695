using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.SettlementAccounts;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models
{
    internal class PaymentFromCustomerAccPostingGenerateRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// Признак посредничества
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        public bool IsMainKontragent { get; set; }

        public SettlementAccount SettlementAccount { get; set; }

        public Kontragent Kontragent { get; set; }

        public Contract Contract { get; set; }

        public IReadOnlyCollection<BaseDocument> Documents { get; set; }
    }
}
