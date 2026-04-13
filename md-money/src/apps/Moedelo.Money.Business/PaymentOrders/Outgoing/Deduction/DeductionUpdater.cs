using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(IDeductionUpdater))]
    internal sealed class DeductionUpdater : IDeductionUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingDeduction;
        private readonly DeductionApiClient apiClient;
        private readonly DeductionEventWriter writer;
        private readonly DeductionProvideInAccountingFixer provideInAccountingFixer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IDeductionCreator creator;
        private readonly DeductionCustomAccPostingsSaver customAccPostingsSaver;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public DeductionUpdater(
            DeductionApiClient apiClient,
            DeductionEventWriter writer,
            DeductionProvideInAccountingFixer provideInAccountingFixer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IDeductionCreator creator,
            DeductionCustomAccPostingsSaver customAccPostingsSaver,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.provideInAccountingFixer = provideInAccountingFixer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(DeductionSaveRequest request)
        {
            await provideInAccountingFixer.FixAsync(request);

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

        private async Task RecreateOperationAsync(DeductionSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(DeductionSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);
            await customAccPostingsSaver.OverwriteAsync(request);
        }
    }
}