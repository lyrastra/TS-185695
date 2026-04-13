using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToNaturalPersons)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class PaymentToNaturalPersonsActualizer :
        ConcreetePaymentOrderActualizerBase<PaymentToNaturalPersonsResponse, PaymentToNaturalPersonsSaveRequest, PaymentOrderSaveResponse>
    {
        public PaymentToNaturalPersonsActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IPaymentToNaturalPersonsReader reader,
            IPaymentToNaturalPersonsUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<PaymentToNaturalPersonsResponse, PaymentToNaturalPersonsSaveRequest> Mapper =>
            PaymentToNaturalPersonsMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(PaymentToNaturalPersonsSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            return Task.CompletedTask;
        }
    }
}
