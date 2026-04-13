using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierUpdater))]
    internal sealed class PaymentToSupplierUpdater : IPaymentToSupplierUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingPaymentToSupplier;
        private readonly PaymentToSupplierApiClient apiClient;
        private readonly IPaymentToSupplierEventWriter eventWriter;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IPaymentToSupplierCreator creator;
        private readonly PaymentToSupplierLinksGetter linksGetter;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public PaymentToSupplierUpdater(
            PaymentToSupplierApiClient apiClient,
            IPaymentToSupplierEventWriter eventWriter,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IPaymentToSupplierCreator creator,
            PaymentToSupplierLinksGetter linksGetter,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.linksGetter = linksGetter;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(PaymentToSupplierSaveRequest request)
        {
            try
            {
                var operationType = await paymentOrderGetter.GetOperationTypeAsync(request.DocumentBaseId);                if (operationType != OperationType)                    throw new OperationMismatchTypeExcepton { ActualType = operationType };                await UpdateOperationAsync(request);
            }
            catch (OperationMismatchTypeExcepton omtex)
            {
                await RecreateOperationAsync(request, omtex.ActualType);
            }

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        public Task SetReserveAsync(SetReserveRequest request)
        {
            return eventWriter.WriteSetReserveEventAsync(request);
        }

        private async Task RecreateOperationAsync(PaymentToSupplierSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(PaymentToSupplierSaveRequest request)
        {
            var invoicelinksResponse = await linksGetter.GetInvoicesLinkAsync(request.DocumentBaseId);
            var invoiceLinks = invoicelinksResponse.GetOrThrow()?.ToArray() ?? Array.Empty<InvoiceLink>();
            if (invoiceLinks.Length > 0)
            {
                var invoiceLinkSaveRequests = invoiceLinks.Select(x => new InvoiceLinkSaveRequest
                {
                    DocumentBaseId = x.DocumentBaseId,
                }).ToArray();
                request.InvoiceLinks = invoiceLinkSaveRequests;
            }

            await apiClient.UpdateAsync(request);
            await eventWriter.WriteUpdatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                PaymentToSupplierMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }
    }
}