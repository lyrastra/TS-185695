using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue
{
    [OperationType(OperationType.PaymentOrderOutgoingLoanIssue)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class LoanIssueActualizer :
        ConcreetePaymentOrderActualizerBase<LoanIssueResponse, LoanIssueSaveRequest, PaymentOrderSaveResponse>
    {

        public LoanIssueActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            ILoanIssueReader reader,
            ILoanIssueUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<LoanIssueResponse, LoanIssueSaveRequest> Mapper => LoanIssueMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(LoanIssueSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            saveRequest.TaxPostings = new TaxPostingsData();
            return Task.CompletedTask;
        }
    }
}
