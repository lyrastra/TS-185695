using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferToAccountCreator))]
    internal sealed class CurrencyTransferToAccountCreator : ICurrencyTransferToAccountCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly CurrencyTransferToAccountApiClient apiClient;
        private readonly CurrencyTransferToAccountEventWriter writer;
        private readonly CurrencyTransferToAccountToDtoMapper dtoMapper;

        public CurrencyTransferToAccountCreator(
            IBaseDocumentCreator baseDocumentCreator,
            CurrencyTransferToAccountApiClient apiClient,
            CurrencyTransferToAccountEventWriter writer,
            CurrencyTransferToAccountToDtoMapper dtoMapper)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.dtoMapper = dtoMapper;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(CurrencyTransferToAccountSaveRequest request)
        {
            var baseDocumentCreateRequest = MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            var dto = await dtoMapper.MapAsync(request).ConfigureAwait(false);
            await apiClient.CreateAsync(dto).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(CurrencyTransferToAccountSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }
    }
}