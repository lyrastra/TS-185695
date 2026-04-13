using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee
{
    [OperationType(OperationType.PaymentOrderIncomingMediationFee)]
    [InjectAsSingleton(typeof(IMediationFeeProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class MediationFeeProvider : IMediationFeeProvider, IConcretePaymentOrderProvider
    {
        private readonly IMediationFeeReader reader;
        private readonly MediationFeeEventWriter eventWriter;
        private readonly IClosedPeriodValidator closedPeriodValidator;

        public MediationFeeProvider(
            IMediationFeeReader reader,
            MediationFeeEventWriter eventWriter,
            IClosedPeriodValidator closedPeriodValidator)
        {
            this.reader = reader;
            this.eventWriter = eventWriter;
            this.closedPeriodValidator = closedPeriodValidator;
        }

        public async Task ProvideAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (response.OperationState.IsBadOperationState())
            {
                return;
            }
            await closedPeriodValidator.ValidateAsync(response.Date);
            await eventWriter.WriteProvideRequiredEventAsync(response);
        }
    }
}
