using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierUpdater))]
    internal sealed class CurrencyPaymentToSupplierUpdater : ICurrencyPaymentToSupplierUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier;
        private readonly CurrencyPaymentToSupplierApiClient apiClient;
        private readonly CurrencyPaymentToSupplierEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ICurrencyPaymentToSupplierCreator creator;
        private readonly ILinksReader linksReader;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public CurrencyPaymentToSupplierUpdater(
            CurrencyPaymentToSupplierApiClient apiClient,
            CurrencyPaymentToSupplierEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            ICurrencyPaymentToSupplierCreator creator,
            ILinksReader linksReader,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.linksReader = linksReader;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(CurrencyPaymentToSupplierSaveRequest request)
        {
            try
            {
                var existsLinks = await linksReader.GetLinksWithDocumentsAsync(request.DocumentBaseId);
                request.OldDocumentLinks = existsLinks.Select(el => new DocumentLinkSaveRequest
                {
                    DocumentBaseId = el.Document.Id,
                    LinkSum = el.Sum
                }).ToArray();

                var operationType = await paymentOrderGetter.GetOperationTypeAsync(request.DocumentBaseId);                if (operationType != OperationType)                    throw new OperationMismatchTypeExcepton { ActualType = operationType };                await UpdateOperationAsync(request);
            }
            catch (OperationMismatchTypeExcepton omtex)
            {
                await RecreateOperationAsync(request, omtex.ActualType);
            }

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private async Task RecreateOperationAsync(CurrencyPaymentToSupplierSaveRequest request, OperationType oldOperationType)
        {
            var oldDocumentBaseId = request.DocumentBaseId;
            var response = await creator.CreateAsync(request);
            await remover.DeleteAsync(oldDocumentBaseId, response.DocumentBaseId);
            await operationEventWriter.WriteOperationTypeChangedEventAsync(
                oldDocumentBaseId,
                oldOperationType,
                response.DocumentBaseId,
                OperationType);
        }

        private async Task UpdateOperationAsync(CurrencyPaymentToSupplierSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                CurrencyPaymentToSupplierMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }
    }
}