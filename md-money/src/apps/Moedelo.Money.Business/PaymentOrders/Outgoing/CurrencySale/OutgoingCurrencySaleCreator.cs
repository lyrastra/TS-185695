using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleCreator))]
    internal sealed class OutgoingCurrencySaleCreator : IOutgoingCurrencySaleCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly OutgoingCurrencySaleApiClient apiClient;
        private readonly OutgoingCurrencySaleEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public OutgoingCurrencySaleCreator(
            IBaseDocumentCreator baseDocumentCreator,
            OutgoingCurrencySaleApiClient apiClient,
            OutgoingCurrencySaleEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(OutgoingCurrencySaleSaveRequest request)
        {
            var baseDocumentCreateRequest = MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                OutgoingCurrencySaleMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(OutgoingCurrencySaleSaveRequest saveRequest)
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