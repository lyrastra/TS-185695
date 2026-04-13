using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(ITransferFromAccountCreator))]
    internal sealed class TransferFromAccountCreator : ITransferFromAccountCreator
    {
        private readonly ITransferFromAccountValidator validator;
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly TransferFromAccountApiClient apiClient;
        private readonly TransferFromAccountEventWriter writer;

        public TransferFromAccountCreator(
            ITransferFromAccountValidator validator,
            IBaseDocumentCreator baseDocumentCreator,
            TransferFromAccountApiClient apiClient,
            TransferFromAccountEventWriter writer)
        {
            this.validator = validator;
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(TransferFromAccountSaveRequest request)
        {
            await validator.ValidateAsync(request).ConfigureAwait(false);

            var baseDocumentCreateRequest = TransferFromAccountMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse
            {
                DocumentBaseId = request.DocumentBaseId
            };
        }
    }
}