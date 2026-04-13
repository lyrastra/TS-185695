using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [OperationType(OperationType.PaymentOrderOutgoingRentPayment)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class RentPaymentActualizer :
        ConcreetePaymentOrderActualizerBase<RentPaymentResponse, RentPaymentSaveRequest, PaymentOrderSaveResponse>
    {

        public RentPaymentActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IRentPaymentReader reader,
            IRentPaymentUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<RentPaymentResponse, RentPaymentSaveRequest> Mapper =>
            RentPaymentMapper.Map;

        protected override Task ActualizeAsync(RentPaymentSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            return Task.CompletedTask;
        }
    }
}
