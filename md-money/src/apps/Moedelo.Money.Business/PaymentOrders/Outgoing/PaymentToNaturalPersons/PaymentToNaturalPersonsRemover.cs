using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToNaturalPersons)]
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class PaymentToNaturalPersonsRemover : IPaymentToNaturalPersonsRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly PaymentToNaturalPersonsApiClient apiClient;
        private readonly PaymentToNaturalPersonsEventWriter eventWriter;

        public PaymentToNaturalPersonsRemover(
            IClosedPeriodValidator closedPeriodValidator,
            PaymentToNaturalPersonsApiClient apiClient,
            PaymentToNaturalPersonsEventWriter eventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var paymentOrder = await apiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);
            await Task.WhenAll(
                apiClient.DeleteAsync(documentBaseId),
                eventWriter.WriteDeletedEventAsync(paymentOrder, newDocumentBaseId)
            );
        }
    }
}