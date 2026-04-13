using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyOther)]
    [InjectAsSingleton(typeof(ICurrencyOtherOutgoingRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class CurrencyOtherOutgoingRemover : ICurrencyOtherOutgoingRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly CurrencyOtherOutgoingApiClient apiClient;
        private readonly CurrencyOtherOutgoingEventWriter writer;

        public CurrencyOtherOutgoingRemover(
            IClosedPeriodValidator closedPeriodValidator,
            CurrencyOtherOutgoingApiClient apiClient,
            CurrencyOtherOutgoingEventWriter writer)
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