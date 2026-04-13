using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [OperationType(OperationType.MemorialWarrantAccrualOfInterest)]
    [InjectAsSingleton(typeof(IAccrualOfInterestProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class AccrualOfInterestProvider : IAccrualOfInterestProvider, IConcretePaymentOrderProvider
    {
        private readonly IAccrualOfInterestReader reader;
        private readonly AccrualOfInterestEventWriter eventWriter;
        private readonly IClosedPeriodValidator closedPeriodValidator;

        public AccrualOfInterestProvider(
            IAccrualOfInterestReader reader,
            AccrualOfInterestEventWriter eventWriter,
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
