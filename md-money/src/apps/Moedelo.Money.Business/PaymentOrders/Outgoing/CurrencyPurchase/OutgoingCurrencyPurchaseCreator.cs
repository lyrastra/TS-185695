using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IOutgoingCurrencyPurchaseCreator))]
    internal sealed class OutgoingCurrencyPurchaseCreator : IOutgoingCurrencyPurchaseCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly OutgoingCurrencyPurchaseApiClient apiClient;
        private readonly OutgoingCurrencyPurchaseEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public OutgoingCurrencyPurchaseCreator(
            IBaseDocumentCreator baseDocumentCreator,
            OutgoingCurrencyPurchaseApiClient apiClient,
            OutgoingCurrencyPurchaseEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(OutgoingCurrencyPurchaseSaveRequest request)
        {
            var baseDocumentCreateRequest = MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                OutgoingCurrencyPurchaseMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(OutgoingCurrencyPurchaseSaveRequest saveRequest)
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