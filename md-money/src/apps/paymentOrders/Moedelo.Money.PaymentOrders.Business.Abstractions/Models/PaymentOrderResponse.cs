using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Collections.Generic;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Models
{
    public class PaymentOrderResponse
    {
        public PaymentOrder PaymentOrder { get; set; }
        public PaymentOrderSnapshot PaymentOrderSnapshot { get; set; }
        public IReadOnlyCollection<WorkerPayment> ChargePayments { get; set; }
        public IReadOnlyCollection<RentPeriod> RentPeriods { get; set; }
        public IReadOnlyCollection<UnifiedBudgetarySubPayment> UnifiedBudgetarySubPayments { get; set; }
    }
}
