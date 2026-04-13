using System;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerUpdater))]
    internal sealed class PaymentFromCustomerUpdater : IPaymentFromCustomerUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingPaymentFromCustomer;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly PaymentFromCustomerApiClient apiClient;
        private readonly IPaymentFromCustomerEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IPaymentFromCustomerCreator creator;
        private readonly PaymentFromCustomerLinksGetter linksGetter;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public PaymentFromCustomerUpdater(
            ITaxationSystemTypeReader taxationSystemTypeReader,
            PaymentFromCustomerApiClient apiClient,
            IPaymentFromCustomerEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IPaymentFromCustomerCreator creator,
            PaymentFromCustomerLinksGetter linksGetter,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.linksGetter = linksGetter;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(PaymentFromCustomerSaveRequest request)
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
            return writer.WriteSetReserveEventAsync(request);
        }

        private async Task RecreateOperationAsync(PaymentFromCustomerSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(PaymentFromCustomerSaveRequest request)
        {
            if (request.TaxationSystemType == null)
            {
                request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            }

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
            await writer.WriteUpdatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                PaymentFromCustomerMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }
    }
}