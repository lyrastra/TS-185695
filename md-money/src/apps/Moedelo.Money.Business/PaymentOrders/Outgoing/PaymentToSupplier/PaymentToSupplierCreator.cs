using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierCreator))]
    class PaymentToSupplierCreator : IPaymentToSupplierCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly PaymentToSupplierApiClient apiClient;
        private readonly IPaymentToSupplierEventWriter eventWriter;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public PaymentToSupplierCreator(
            IBaseDocumentCreator baseDocumentCreator,
            PaymentToSupplierApiClient apiClient,
            IPaymentToSupplierEventWriter eventWriter,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(PaymentToSupplierSaveRequest request)
        {
            var baseDocumentCreateRequest = PaymentToSupplierMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await eventWriter.WriteCreatedEventAsync(request);

            if (request.OperationState.IsBadOperationState() == false)
            {
                await customTaxPostingsSaver.OverwriteAsync(
                    PaymentToSupplierMapper.MapToCustomTaxPostingsOverwriteRequest(request));
            }

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}