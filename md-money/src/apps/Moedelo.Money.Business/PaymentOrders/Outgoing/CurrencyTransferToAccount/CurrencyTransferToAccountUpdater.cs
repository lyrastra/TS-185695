using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferToAccountUpdater))]
    internal sealed class CurrencyTransferToAccountUpdater : ICurrencyTransferToAccountUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingCurrencyTransferToAccount;
        private readonly CurrencyTransferToAccountApiClient apiClient;
        private readonly CurrencyTransferToAccountEventWriter writer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ICurrencyTransferToAccountCreator creator;
        private readonly CurrencyTransferToAccountToDtoMapper dtoMapper;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public CurrencyTransferToAccountUpdater(
            CurrencyTransferToAccountApiClient apiClient,
            CurrencyTransferToAccountEventWriter writer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            ICurrencyTransferToAccountCreator creator,
            CurrencyTransferToAccountToDtoMapper dtoMapper,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.dtoMapper = dtoMapper;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(CurrencyTransferToAccountSaveRequest request)
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

        private async Task RecreateOperationAsync(CurrencyTransferToAccountSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(CurrencyTransferToAccountSaveRequest request)
        {
            var dto = await dtoMapper.MapAsync(request);
            await apiClient.UpdateAsync(dto);
            await writer.WriteUpdatedEventAsync(request);
        }
    }
}