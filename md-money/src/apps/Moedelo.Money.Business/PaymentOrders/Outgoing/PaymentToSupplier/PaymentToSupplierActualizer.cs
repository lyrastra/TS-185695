using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToSupplier)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class PaymentToSupplierActualizer :
        ConcreetePaymentOrderActualizerBase<PaymentToSupplierResponse, PaymentToSupplierSaveRequest, PaymentOrderSaveResponse>
    {

        public PaymentToSupplierActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IPaymentToSupplierReader reader,
            IPaymentToSupplierUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<PaymentToSupplierResponse, PaymentToSupplierSaveRequest> Mapper =>
            PaymentToSupplierMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(PaymentToSupplierSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            saveRequest.TaxPostings = new TaxPostingsData();
            return Task.CompletedTask;
        }
    }
}
