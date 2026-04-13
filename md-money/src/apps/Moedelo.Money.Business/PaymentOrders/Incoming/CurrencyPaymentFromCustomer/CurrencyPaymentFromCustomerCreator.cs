using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerCreator))]
    internal sealed class CurrencyPaymentFromCustomerCreator : ICurrencyPaymentFromCustomerCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly CurrencyPaymentFromCustomerApiClient apiClient;
        private readonly CurrencyPaymentFromCustomerEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;

        public CurrencyPaymentFromCustomerCreator(
            IBaseDocumentCreator baseDocumentCreator,
            CurrencyPaymentFromCustomerApiClient apiClient,
            CurrencyPaymentFromCustomerEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            ITaxationSystemTypeReader taxationSystemTypeReader)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(CurrencyPaymentFromCustomerSaveRequest request)
        {
            if (request.TaxationSystemType == null)
            {
                request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year).ConfigureAwait(false);
            }

            var baseDocumentCreateRequest = CurrencyPaymentFromCustomerMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                CurrencyPaymentFromCustomerMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}