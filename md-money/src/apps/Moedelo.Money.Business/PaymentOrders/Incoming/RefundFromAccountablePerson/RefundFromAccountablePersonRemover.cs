using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [OperationType(OperationType.PaymentOrderIncomingRefundFromAccountablePerson)]
    [InjectAsSingleton(typeof(IRefundFromAccountablePersonRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class RefundFromAccountablePersonRemover : IRefundFromAccountablePersonRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly RefundFromAccountablePersonApiClient apiClient;
        private readonly RefundFromAccountablePersonEventWriter writer;

        public RefundFromAccountablePersonRemover(
            IClosedPeriodValidator closedPeriodValidator,
            RefundFromAccountablePersonApiClient apiClient,
            RefundFromAccountablePersonEventWriter writer)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.apiClient = apiClient;
            this.writer = writer;

        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var paymentOrder = await apiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);
            await apiClient.DeleteAsync(documentBaseId);
            await writer.WriteDeletedEventAsync(paymentOrder, newDocumentBaseId);
        }
    }
}