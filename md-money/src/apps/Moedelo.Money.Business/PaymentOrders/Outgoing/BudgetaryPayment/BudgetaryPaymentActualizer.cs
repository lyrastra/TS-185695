using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [OperationType(OperationType.BudgetaryPayment)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class BudgetaryPaymentActualizer :
        ConcreetePaymentOrderActualizerBase<BudgetaryPaymentResponse, BudgetaryPaymentSaveRequest, PaymentOrderSaveResponse>
    {

        public BudgetaryPaymentActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IBudgetaryPaymentReader reader,
            IBudgetaryPaymentUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<BudgetaryPaymentResponse, BudgetaryPaymentSaveRequest> Mapper =>
            BudgetaryPaymentMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(BudgetaryPaymentSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            saveRequest.TaxPostings = new TaxPostingsData();
            return Task.CompletedTask;
        }
    }
}
