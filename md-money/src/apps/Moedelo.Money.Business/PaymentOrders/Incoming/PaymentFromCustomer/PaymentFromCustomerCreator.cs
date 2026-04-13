using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerCreator))]
    class PaymentFromCustomerCreator : IPaymentFromCustomerCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly PaymentFromCustomerApiClient apiClient;
        private readonly IPaymentFromCustomerEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public PaymentFromCustomerCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            PaymentFromCustomerApiClient apiClient,
            IPaymentFromCustomerEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(PaymentFromCustomerSaveRequest request)
        {
            if (request.TaxationSystemType == null)
            {
                request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            }

            var baseDocumentCreateRequest = PaymentFromCustomerMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                PaymentFromCustomerMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}