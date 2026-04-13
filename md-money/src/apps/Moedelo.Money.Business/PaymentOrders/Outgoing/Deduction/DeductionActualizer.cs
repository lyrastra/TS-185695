using System;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [OperationType(OperationType.PaymentOrderOutgoingDeduction)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class DeductionActualizer :
        ConcreetePaymentOrderActualizerBase<DeductionResponse, DeductionSaveRequest, PaymentOrderSaveResponse>
    {
        private readonly DeductionAccPostingsGetter deductionAccPostingsGetter;
        
        public DeductionActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IDeductionReader reader,
            IDeductionUpdater updater,
            DeductionAccPostingsGetter deductionAccPostingsGetter)
            : base(closedPeriodValidator, reader, updater)
        {
            this.deductionAccPostingsGetter = deductionAccPostingsGetter;
        }

        protected override Func<DeductionResponse, DeductionSaveRequest> Mapper =>
            DeductionMapper.MapToSaveRequest;

        protected override async Task ActualizeAsync(DeductionSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            saveRequest.AccountingPosting =
                await deductionAccPostingsGetter.GetAsync(DeductionMapper.MapToAccPostingRequest(saveRequest));
        }
    }
}
