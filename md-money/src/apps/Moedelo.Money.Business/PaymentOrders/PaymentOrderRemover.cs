using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Domain.Operations;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderRemover))]
    internal sealed class PaymentOrderRemover : IPaymentOrderRemover
    {
        private readonly ILogger logger;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IPaymentOrderApiClient paymentOrderApiClient;
        private readonly Dictionary<OperationType, IConcretePaymentOrderRemover> removers;
        private readonly Dictionary<OperationType, IConcretePaymentOrderDeletedEventWriter> deletedEventWriters;

        public PaymentOrderRemover(
            ILogger<PaymentOrderRemover> logger,
            IPaymentOrderGetter paymentOrderGetter,
            IPaymentOrderApiClient paymentOrderApiClient,
            IEnumerable<IConcretePaymentOrderRemover> removers,
            IEnumerable<IConcretePaymentOrderDeletedEventWriter> deletedEventWriters)
        {
            this.logger = logger;
            this.paymentOrderGetter = paymentOrderGetter;
            this.paymentOrderApiClient = paymentOrderApiClient;
            this.removers = removers.Select(x =>
                 new
                 {
                     x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                     Remover = x
                 })
                .ToDictionary(x => x.OperationType, x => x.Remover);
            this.deletedEventWriters = deletedEventWriters.Select(x =>
                 new
                 {
                     x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                     EventWriter = x
                 })
                .ToDictionary(x => x.OperationType, x => x.EventWriter);
        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var operationType = await paymentOrderGetter.GetOperationTypeAsync(documentBaseId);
            var remover = GetRemover(operationType);
            await remover.DeleteAsync(documentBaseId, newDocumentBaseId);
        }

        public async Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.Count <= 0)
            {
                return;
            }

            var operationTypeResponses = await paymentOrderGetter.GetOperationTypeByBaseIdsAsync(documentBaseIds);
            if (operationTypeResponses.Length <= 0)
            {
                return;
            }

            // todo: по возможности переписать на массовое удаление
            await operationTypeResponses
                .RunParallelAsync(operationTypeResponse =>
                {
                    var remover = GetRemover(operationTypeResponse.OperationType);

                    return DeleteWithCatchAsync(remover, operationTypeResponse);
                });
        }

        public async Task DeleteInvalidAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.Count <= 0)
            {
                return;
            }

            var operationTypeResponses = await paymentOrderGetter.GetOperationTypeByBaseIdsAsync(documentBaseIds);
            if (operationTypeResponses.Length <= 0)
            {
                return;
            }

            var deletedBaseIds = await paymentOrderApiClient.DeleteInvalidAsync(documentBaseIds);
            if (deletedBaseIds.Length <= 0)
            {
                return;
            }

            var deletedOperationTypeResponses = operationTypeResponses.Where(x => deletedBaseIds.Contains(x.DocumentBaseId)).ToArray();


            await deletedOperationTypeResponses
                .RunParallelAsync(operationTypeResponse =>
                    GetDeletedEventWriter(operationTypeResponse.OperationType)
                        ?.WriteDeletedEventAsync(operationTypeResponse.DocumentBaseId)
                    ?? Task.CompletedTask);
        }

        private IConcretePaymentOrderRemover GetRemover(OperationType operationType)
        {
            if (removers.TryGetValue(operationType, out var remover))
            {
                return remover;
            }
            throw new NotImplementedException($"Implementation of IConcretePaymentOrderRemover for operation type {operationType} ({(int)operationType}) is not found");
        }

        private IConcretePaymentOrderDeletedEventWriter GetDeletedEventWriter(OperationType operationType)
        {
            if (deletedEventWriters.TryGetValue(operationType, out var eventWriter))
            {
                return eventWriter;
            }
            logger.LogWarning($"Implementation of IConcretePaymentOrderDeletedEventWriter for operation type {operationType} ({(int)operationType}) is not found");
            return null;
        }

        public async Task DeleteWithCatchAsync(IConcretePaymentOrderRemover remover, OperationTypeResponse operationTypeResponse)
        {
            try
            {
                await remover.DeleteAsync(operationTypeResponse.DocumentBaseId);
            }
            catch (BusinessValidationException vex)
            {
                // игнорируем валидацию, тк она проверяет только закрытый период
                // а операции из закрытого периода удалять нельзя
                logger.LogWarning(vex, $"Unable to remove operation with base id {operationTypeResponse.DocumentBaseId}", operationTypeResponse);
            }
            catch (OperationNotFoundException onfex)
            {
                if (operationTypeResponse.OperationType == OperationType.PaymentOrderIncomingTransferFromAccount ||
                    operationTypeResponse.OperationType == OperationType.PaymentOrderOutgoingTransferToAccount)
                {
                    // если удалять пару связанных операций перевода, то после удаления первой, вторая удалится автоматически
                    // при удалении второй будет 404 код из-за того, что обе операции уже были удалены
                    // fixme: пока организую такой костыль, потом нужно будет что-нибудь придумать
                    logger.LogWarning(onfex, $"Unable to remove operation with base id {operationTypeResponse.DocumentBaseId}", operationTypeResponse);
                    return;
                }
                //throw;
            }
        }
    }
}
