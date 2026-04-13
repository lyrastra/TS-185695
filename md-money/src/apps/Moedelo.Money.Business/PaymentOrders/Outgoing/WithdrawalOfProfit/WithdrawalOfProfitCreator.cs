using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [InjectAsSingleton(typeof(IWithdrawalOfProfitCreator))]
    class WithdrawalOfProfitCreator : IWithdrawalOfProfitCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly WithdrawalOfProfitApiClient apiClient;
        private readonly WithdrawalOfProfitEventWriter writer;

        public WithdrawalOfProfitCreator(
            IBaseDocumentCreator baseDocumentCreator,
            WithdrawalOfProfitApiClient apiClient,
            WithdrawalOfProfitEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(WithdrawalOfProfitSaveRequest request)
        {
            var baseDocumentCreateRequest = WithdrawalOfProfitMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}