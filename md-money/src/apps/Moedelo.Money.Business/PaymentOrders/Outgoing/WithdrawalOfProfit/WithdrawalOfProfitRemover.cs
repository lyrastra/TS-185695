using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [OperationType(OperationType.PaymentOrderOutgoingWithdrawalOfProfit)]
    [InjectAsSingleton(typeof(IWithdrawalOfProfitRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class WithdrawalOfProfitRemover : IWithdrawalOfProfitRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly WithdrawalOfProfitApiClient apiClient;
        private readonly WithdrawalOfProfitEventWriter writer;

        public WithdrawalOfProfitRemover(
            IClosedPeriodValidator closedPeriodValidator,
            WithdrawalOfProfitApiClient apiClient,
            WithdrawalOfProfitEventWriter writer)
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