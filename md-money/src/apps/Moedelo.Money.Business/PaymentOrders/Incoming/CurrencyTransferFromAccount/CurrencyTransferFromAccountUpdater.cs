using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferFromAccountUpdater))]
    internal sealed class CurrencyTransferFromAccountUpdater : ICurrencyTransferFromAccountUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingCurrencyTransferFromAccount;
        private readonly CurrencyTransferFromAccountApiClient apiClient;
        private readonly CurrencyTransferFromAccountEventWriter writer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ICurrencyTransferFromAccountCreator creator;
        private readonly CurrencyTransferFromAccountToDtoMapper dtoMapper;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public CurrencyTransferFromAccountUpdater(
            CurrencyTransferFromAccountApiClient apiClient,
            CurrencyTransferFromAccountEventWriter writer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            ICurrencyTransferFromAccountCreator creator,
            CurrencyTransferFromAccountToDtoMapper dtoMapper,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.dtoMapper = dtoMapper;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(CurrencyTransferFromAccountSaveRequest request)
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

        private async Task RecreateOperationAsync(CurrencyTransferFromAccountSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(CurrencyTransferFromAccountSaveRequest request)
        {
            var dto = await dtoMapper.MapAsync(request);
            await apiClient.UpdateAsync(dto);
            await writer.WriteUpdatedEventAsync(request);
        }
    }
}