using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferFromAccountCreator))]
    internal sealed class CurrencyTransferFromAccountCreator : ICurrencyTransferFromAccountCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly CurrencyTransferFromAccountApiClient apiClient;
        private readonly CurrencyTransferFromAccountEventWriter writer;
        private readonly CurrencyTransferFromAccountToDtoMapper dtoMapper;

        public CurrencyTransferFromAccountCreator(
            IBaseDocumentCreator baseDocumentCreator,
            CurrencyTransferFromAccountApiClient apiClient,
            CurrencyTransferFromAccountEventWriter writer,
            CurrencyTransferFromAccountToDtoMapper dtoMapper)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.dtoMapper = dtoMapper;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(CurrencyTransferFromAccountSaveRequest request)
        {
            var baseDocumentCreateRequest = MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            var dto = await dtoMapper.MapAsync(request);
            await apiClient.CreateAsync(dto).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse
            {
                DocumentBaseId = request.DocumentBaseId
            };
        }

        private static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(CurrencyTransferFromAccountSaveRequest request)
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