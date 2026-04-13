using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IIncomingCurrencyPurchaseCreator))]
    internal sealed class IncomingCurrencyPurchaseCreator : IIncomingCurrencyPurchaseCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly IncomingCurrencyPurchaseApiClient apiClient;
        private readonly IncomingCurrencyPurchaseEventWriter writer;
        private readonly IncomingCurrencyPurchaseToDtoMapper dtoMapper;

        public IncomingCurrencyPurchaseCreator(
            IBaseDocumentCreator baseDocumentCreator,
            IncomingCurrencyPurchaseApiClient apiClient,
            IncomingCurrencyPurchaseEventWriter writer,
            IncomingCurrencyPurchaseToDtoMapper dtoMapper)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.dtoMapper = dtoMapper;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(IncomingCurrencyPurchaseSaveRequest request)
        {
            var baseDocumentCreateRequest = MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            var dto = await dtoMapper.MapAsync(request);

            await apiClient.CreateAsync(dto);
            await writer.WriteCreatedEventAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(IncomingCurrencyPurchaseSaveRequest saveRequest)
        {
            return new BaseDocumentCreateRequest
            {
                Sum = saveRequest.Sum,
                Date = saveRequest.Date,
                Number = saveRequest.Number
            };
        }
    }
}