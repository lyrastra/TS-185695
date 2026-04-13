using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier)]
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class CurrencyPaymentToSupplierRemover : ICurrencyPaymentToSupplierRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly CurrencyPaymentToSupplierApiClient apiClient;
        private readonly CurrencyPaymentToSupplierEventWriter writer;

        public CurrencyPaymentToSupplierRemover(
            IClosedPeriodValidator closedPeriodValidator,
            CurrencyPaymentToSupplierApiClient apiClient,
            CurrencyPaymentToSupplierEventWriter writer)
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