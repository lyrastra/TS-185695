using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [OperationType(OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class UnifiedBudgetaryPaymentActualizer :
        ConcreetePaymentOrderActualizerBase<UnifiedBudgetaryPaymentResponse, UnifiedBudgetaryPaymentSaveRequest, PaymentOrderSaveResponse>
    {

        public UnifiedBudgetaryPaymentActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IUnifiedBudgetaryPaymentReader reader,
            IUnifiedBudgetaryPaymentUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<UnifiedBudgetaryPaymentResponse, UnifiedBudgetaryPaymentSaveRequest> Mapper =>
            UnifiedBudgetaryPaymentMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(UnifiedBudgetaryPaymentSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            foreach (var subPayment in saveRequest.SubPayments)
            {
                subPayment.TaxPostings = new TaxPostingsData();
            }
            return Task.CompletedTask;
        }
    }
}
