using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToAccountablePerson)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class PaymentToAccountablePersonActualizer :
        ConcreetePaymentOrderActualizerBase<PaymentToAccountablePersonResponse, PaymentToAccountablePersonSaveRequest, PaymentOrderSaveResponse>
    {

        public PaymentToAccountablePersonActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IPaymentToAccountablePersonReader reader,
            IPaymentToAccountablePersonUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<PaymentToAccountablePersonResponse, PaymentToAccountablePersonSaveRequest> Mapper =>
            PaymentToAccountablePersonMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(PaymentToAccountablePersonSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            saveRequest.TaxPostings = new TaxPostingsData();
            return Task.CompletedTask;
        }
    }
}
