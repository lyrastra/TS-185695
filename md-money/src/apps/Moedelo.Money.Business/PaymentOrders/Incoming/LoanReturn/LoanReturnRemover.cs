using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn
{
    [OperationType(OperationType.PaymentOrderIncomingLoanReturn)]
    [InjectAsSingleton(typeof(ILoanReturnRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class LoanReturnRemover : ILoanReturnRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly LoanReturnApiClient apiClient;
        private readonly LoanReturnEventWriter writer;

        public LoanReturnRemover(
            IClosedPeriodValidator closedPeriodValidator,
            LoanReturnApiClient apiClient,
            LoanReturnEventWriter writer)
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