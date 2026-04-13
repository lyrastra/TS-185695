using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer)]
    [InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class CurrencyPaymentFromCustomerRemover : ICurrencyPaymentFromCustomerRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly CurrencyPaymentFromCustomerApiClient apiClient;
        private readonly CurrencyPaymentFromCustomerEventWriter writer;

        public CurrencyPaymentFromCustomerRemover(
            IClosedPeriodValidator closedPeriodValidator,
            CurrencyPaymentFromCustomerApiClient apiClient,
            CurrencyPaymentFromCustomerEventWriter writer)
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