using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentUpdater))]
    internal sealed class RentPaymentUpdater : IRentPaymentUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingRentPayment;
        private readonly RentPaymentApiClient apiClient;
        private readonly RentPaymentEventWriter eventWriter;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IRentPaymentCreator creator;
        private readonly RentPeriodsReader rentPeriodsReader;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public RentPaymentUpdater(
            RentPaymentApiClient apiClient,
            RentPaymentEventWriter eventWriter,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IRentPaymentCreator creator,
            RentPeriodsReader rentPeriodsReader,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.rentPeriodsReader = rentPeriodsReader;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(RentPaymentSaveRequest request)
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

        private async Task RecreateOperationAsync(RentPaymentSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(RentPaymentSaveRequest request)
        {
            await apiClient.UpdateAsync(request);

            request.RentPeriods = await rentPeriodsReader.GetAsync(request.RentPeriods);
            await eventWriter.WriteUpdatedEventAsync(request);
        }
    }
}
