using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [OperationType(OperationType.PaymentOrderOutgoingLoanRepayment)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class LoanRepaymentActualizer :
        ConcreetePaymentOrderActualizerBase<LoanRepaymentResponse, LoanRepaymentSaveRequest, PaymentOrderSaveResponse>
    {

        public LoanRepaymentActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            ILoanRepaymentReader reader,
            ILoanRepaymentUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<LoanRepaymentResponse, LoanRepaymentSaveRequest> Mapper =>
            LoanRepaymentMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(LoanRepaymentSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            saveRequest.TaxPostings = new TaxPostingsData();
            return Task.CompletedTask;
        }
    }
}
