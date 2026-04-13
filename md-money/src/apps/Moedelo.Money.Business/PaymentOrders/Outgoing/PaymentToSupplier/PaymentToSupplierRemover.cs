using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToSupplier)]
    [InjectAsSingleton(typeof(IPaymentToSupplierRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class PaymentToSupplierRemover : IPaymentToSupplierRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly PaymentToSupplierApiClient apiClient;
        private readonly IPaymentToSupplierEventWriter eventWriter;

        public PaymentToSupplierRemover(
            IClosedPeriodValidator closedPeriodValidator,
            PaymentToSupplierApiClient apiClient,
            IPaymentToSupplierEventWriter eventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var paymentOrder = await apiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);
            await apiClient.DeleteAsync(documentBaseId);
            await eventWriter.WriteDeletedEventAsync(paymentOrder, newDocumentBaseId);
        }
    }
}