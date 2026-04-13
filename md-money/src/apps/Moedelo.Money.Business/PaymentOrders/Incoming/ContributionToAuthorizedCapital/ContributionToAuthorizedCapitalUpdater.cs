using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton(typeof(IContributionToAuthorizedCapitalUpdater))]
    internal sealed class ContributionToAuthorizedCapitalUpdater : IContributionToAuthorizedCapitalUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingContributionToAuthorizedCapital;
        private readonly ContributionToAuthorizedCapitalApiClient apiClient;
        private readonly ContributionToAuthorizedCapitalEventWriter writer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IContributionToAuthorizedCapitalCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public ContributionToAuthorizedCapitalUpdater(
            ContributionToAuthorizedCapitalApiClient apiClient,
            ContributionToAuthorizedCapitalEventWriter writer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IContributionToAuthorizedCapitalCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(ContributionToAuthorizedCapitalSaveRequest request)
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

        private async Task RecreateOperationAsync(ContributionToAuthorizedCapitalSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(ContributionToAuthorizedCapitalSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);
        }
    }
}