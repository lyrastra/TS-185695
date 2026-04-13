using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    [OperationType(OperationType.CurrencyBankFee)]
    [InjectAsSingleton(typeof(ICurrencyBankFeeRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    class CurrencyBankFeeRemover : ICurrencyBankFeeRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly CurrencyBankFeeApiClient apiClient;
        private readonly CurrencyBankFeeEventWriter writer;

        public CurrencyBankFeeRemover(
            IClosedPeriodValidator closedPeriodValidator,
            CurrencyBankFeeApiClient apiClient,
            CurrencyBankFeeEventWriter writer)
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