using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [InjectAsSingleton(typeof(ILoanRepaymentUpdater))]
    internal sealed class LoanRepaymentUpdater : ILoanRepaymentUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingLoanRepayment;
        private readonly LoanRepaymentApiClient apiClient;
        private readonly LoanRepaymentEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ILoanRepaymentCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public LoanRepaymentUpdater(
            LoanRepaymentApiClient apiClient,
            LoanRepaymentEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            ILoanRepaymentCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(LoanRepaymentSaveRequest request)
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

        private async Task RecreateOperationAsync(LoanRepaymentSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(LoanRepaymentSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                LoanRepaymentMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }
    }
}