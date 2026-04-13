using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton(typeof(IRefundToCustomerUpdater))]
    internal sealed class RefundToCustomerUpdater : IRefundToCustomerUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingRefundToCustomer;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly RefundToCustomerApiClient apiClient;
        private readonly RefundToCustomerEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IRefundToCustomerCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public RefundToCustomerUpdater(
            ITaxationSystemTypeReader taxationSystemTypeReader,
            RefundToCustomerApiClient apiClient,
            RefundToCustomerEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IRefundToCustomerCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(RefundToCustomerSaveRequest request)
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

        private async Task RecreateOperationAsync(RefundToCustomerSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(RefundToCustomerSaveRequest request)
        {
            request.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                RefundToCustomerMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }
    }
}