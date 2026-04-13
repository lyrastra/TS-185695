using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsUpdater))]
    internal sealed class PaymentToNaturalPersonsUpdater : IPaymentToNaturalPersonsUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingPaymentToNaturalPersons;
        private readonly PaymentToNaturalPersonsApiClient apiClient;
        private readonly PaymentToNaturalPersonsEventWriter writer;
        private readonly IPaymentOrderRemover remover;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IPaymentToNaturalPersonsCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public PaymentToNaturalPersonsUpdater(
            PaymentToNaturalPersonsApiClient apiClient,
            PaymentToNaturalPersonsEventWriter writer,
            IPaymentOrderRemover remover,
            IPaymentOrderGetter paymentOrderGetter,
            IPaymentToNaturalPersonsCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.remover = remover;
            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(PaymentToNaturalPersonsSaveRequest request)
        {
            try
            {
                var operationType = await paymentOrderGetter.GetOperationTypeAsync(request.DocumentBaseId);
                if (operationType != OperationType)
                    throw new OperationMismatchTypeExcepton { ActualType = operationType };

                var isCurrentlyPaid = await paymentOrderGetter.GetIsPaidAsync(request.DocumentBaseId);
                request.IsPaidStatusChanged = request.IsPaid != isCurrentlyPaid;

                await UpdateOperationAsync(request);
            }
            catch (OperationMismatchTypeExcepton omtex)
            {
                await RecreateOperationAsync(request, omtex.ActualType);
            }

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private async Task RecreateOperationAsync(PaymentToNaturalPersonsSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(PaymentToNaturalPersonsSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);
        }
    }
}