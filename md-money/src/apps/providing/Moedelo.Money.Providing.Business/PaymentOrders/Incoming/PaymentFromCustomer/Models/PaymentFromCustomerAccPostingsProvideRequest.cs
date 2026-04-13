using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models
{
    internal class PaymentFromCustomerAccPostingsProvideRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public bool IsMediation { get; set; }

        public int SettlementAccountId { get; set; }

        public Kontragent Kontragent { get; set; }

        public Contract Contract { get; set; }

        public IReadOnlyCollection<BaseDocument> BaseDocuments { get; set; }

        public bool IsMainKontragent { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsBadOperationState { get; set; }
    }
}
