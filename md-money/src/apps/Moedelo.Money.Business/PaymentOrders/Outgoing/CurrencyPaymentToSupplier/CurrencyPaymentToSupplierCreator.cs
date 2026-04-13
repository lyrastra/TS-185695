using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierCreator))]
    internal sealed class CurrencyPaymentToSupplierCreator : ICurrencyPaymentToSupplierCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly CurrencyPaymentToSupplierApiClient apiClient;
        private readonly CurrencyPaymentToSupplierEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public CurrencyPaymentToSupplierCreator(
            IBaseDocumentCreator baseDocumentCreator,
            CurrencyPaymentToSupplierApiClient apiClient,
            CurrencyPaymentToSupplierEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(CurrencyPaymentToSupplierSaveRequest request)
        {
            var baseDocumentCreateRequest = MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                CurrencyPaymentToSupplierMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(CurrencyPaymentToSupplierSaveRequest request)
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