using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    [InjectAsSingleton(typeof(IIncomingCurrencySaleCreator))]
    internal sealed class IncomingCurrencySaleCreator : IIncomingCurrencySaleCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly IncomingCurrencySaleApiClient apiClient;
        private readonly IncomingCurrencySaleEventWriter writer;

        public IncomingCurrencySaleCreator(
            IBaseDocumentCreator baseDocumentCreator,
            IncomingCurrencySaleApiClient apiClient,
            IncomingCurrencySaleEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(IncomingCurrencySaleSaveRequest request)
        {
            var baseDocumentCreateRequest = MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(IncomingCurrencySaleSaveRequest saveRequest)
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