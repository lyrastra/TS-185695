using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [OperationType(OperationType.PaymentOrderOutgoingRefundToCustomer)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class RefundToCustomerActualizer :
        ConcreetePaymentOrderActualizerBase<RefundToCustomerResponse, RefundToCustomerSaveRequest, PaymentOrderSaveResponse>
    {

        public RefundToCustomerActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IRefundToCustomerReader reader,
            IRefundToCustomerUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<RefundToCustomerResponse, RefundToCustomerSaveRequest> Mapper =>
            RefundToCustomerMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(RefundToCustomerSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            saveRequest.TaxPostings = new TaxPostingsData();
            return Task.CompletedTask;
        }
    }
}
