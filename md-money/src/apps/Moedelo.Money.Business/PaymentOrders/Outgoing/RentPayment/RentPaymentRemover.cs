using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [OperationType(OperationType.PaymentOrderOutgoingRentPayment)]
    [InjectAsSingleton(typeof(IRentPaymentRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class RentPaymentRemover : IRentPaymentRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly RentPaymentApiClient apiClient;
        private readonly RentPaymentEventWriter writer;

        public RentPaymentRemover(
            IClosedPeriodValidator closedPeriodValidator,
            RentPaymentApiClient apiClient,
            RentPaymentEventWriter writer)
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

        public Task DeleteByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            return documentBaseIds.RunParallelAsync(documentBaseId => DeleteAsync(documentBaseId, null));
        }
    }
}
