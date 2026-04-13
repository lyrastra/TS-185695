using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyOther)]
    [InjectAsSingleton(typeof(ICurrencyOtherIncomingRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class CurrencyOtherIncomingRemover : ICurrencyOtherIncomingRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly CurrencyOtherIncomingApiClient apiClient;
        private readonly CurrencyOtherIncomingEventWriter writer;

        public CurrencyOtherIncomingRemover(
            IClosedPeriodValidator closedPeriodValidator,
            CurrencyOtherIncomingApiClient apiClient,
            CurrencyOtherIncomingEventWriter writer)
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