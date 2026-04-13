using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(IWithdrawalFromAccountCreator))]
    class WithdrawalFromAccountCreator : IWithdrawalFromAccountCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly WithdrawalFromAccountApiClient apiClient;
        private readonly WithdrawalFromAccountEventWriter writer;

        public WithdrawalFromAccountCreator(
            IBaseDocumentCreator baseDocumentCreator,
            WithdrawalFromAccountApiClient apiClient,
            WithdrawalFromAccountEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(WithdrawalFromAccountSaveRequest request)
        {
            var baseDocumentCreateRequest = WithdrawalFromAccountMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}