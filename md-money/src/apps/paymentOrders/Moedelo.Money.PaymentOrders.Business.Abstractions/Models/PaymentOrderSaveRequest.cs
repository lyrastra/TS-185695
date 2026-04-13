using System.Collections.Generic;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Models
{
    public class PaymentOrderSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public PaymentOrder PaymentOrder { get; set; }

        public BudgetaryFields BudgetaryFields { get; set; }

        public KontragentRequisites KontragentRequisites { get; set; }

        public IReadOnlyCollection<WorkerPayment> ChargePayments { get; set; }

        public IReadOnlyCollection<RentPeriod> RentPeriods { get; set; }

        public DeductionFields DeductionFields { get; set; }
        
        public IReadOnlyCollection<UnifiedBudgetarySubPayment> UnifiedBudgetarySubPayments { get; set; }
    }
}
