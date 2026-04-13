using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Providing
{
    [InjectAsSingleton(typeof(IPaymentOrderProvider))]
    internal sealed class PaymentOrderProvider : IPaymentOrderProvider
    {
        private readonly ILogger logger;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IReadOnlyDictionary<OperationType, IConcretePaymentOrderProvider> providers;
        private readonly IReadOnlyDictionary<OperationType, IConcretePaymentOrderBatchProvider> batchProviders;

        public PaymentOrderProvider(
            ILogger<PaymentOrderProvider> logger,
            IPaymentOrderGetter paymentOrderGetter,
            IEnumerable<IConcretePaymentOrderProvider> providers,
            IEnumerable<IConcretePaymentOrderBatchProvider> batchProviders)
        {
            this.logger = logger;
            this.paymentOrderGetter = paymentOrderGetter;
            this.providers = providers.Select(x =>
                 new
                 {
                     x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                     Provider = x
                 })
                .ToDictionary(x => x.OperationType, x => x.Provider);

            this.batchProviders = batchProviders.Select(x =>
                 new
                 {
                     x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                     Provider = x
                 })
                .ToDictionary(x => x.OperationType, x => x.Provider);
        }

        public async Task ProvideAsync(long documentBaseId)
        {
            try
            {
                var operationType = await paymentOrderGetter.GetOperationTypeAsync(documentBaseId);
                var provider = GetProvider(operationType);
                await provider.ProvideAsync(documentBaseId);
            }
            catch (OperationNotFoundException)
            {
                // игнорируем удаленные операции
            }
        }

        public async Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            // убираем возможные дубликаты
            documentBaseIds = documentBaseIds.Distinct().ToArray();
            if (documentBaseIds.Count == 0)
            {
                return;
            }

            var operationTypeResponses = await paymentOrderGetter.GetOperationTypeByBaseIdsAsync(documentBaseIds);
            if (operationTypeResponses.Length == 0)
            {
                return;
            }

            var (massiveProvide, singleProvide) = GetOperationTypesForProvide(operationTypeResponses);

            // todo: создать providingStateId сразу для всех операций за раз

            await massiveProvide.RunParallelAsync(operation =>
            {
                var provider = batchProviders[operation.Key];
                return provider.ProvideAsync(operation.Value);
            });
            
            await singleProvide.RunParallelAsync(operationTypeResponse =>
            {
                var provider = GetProvider(operationTypeResponse.OperationType);
                return ProvideWithCatchAsync(provider, operationTypeResponse);
            });
        }

        private
            (IReadOnlyDictionary<OperationType, IReadOnlyCollection<long>> massiveProvide,
            IReadOnlyCollection<OperationTypeResponse> singleProvide)
            GetOperationTypesForProvide(IReadOnlyCollection<OperationTypeResponse> operationTypeResponses)
        {
            var operationsByTypesMap = operationTypeResponses
                .GroupBy(x => x.OperationType)
                .ToDictionary(x => x.Key);

            var operationTypesForMassiveProvide = new Dictionary<OperationType, IReadOnlyCollection<long>>(batchProviders.Count);
            var operationTypesForSingleProvide = new List<OperationTypeResponse>(operationTypeResponses.Count);

            foreach (var operationsByTypeMap in operationsByTypesMap)
            {
                if (batchProviders.ContainsKey(operationsByTypeMap.Key))
                {
                    var baseIdsByTypeForMassiveProvide = operationsByTypeMap.Value
                        .Select(x => x.DocumentBaseId)
                        .ToArray();
                    operationTypesForMassiveProvide.Add(operationsByTypeMap.Key, baseIdsByTypeForMassiveProvide);
                }
                else
                {
                    operationTypesForSingleProvide.AddRange(operationsByTypeMap.Value);
                }
            }

            return (operationTypesForMassiveProvide, operationTypesForSingleProvide);
        }

        private IConcretePaymentOrderProvider GetProvider(OperationType operationType)
        {
            if (providers.TryGetValue(operationType, out var provider))
            {
                return provider;
            }
            // BP-8683 Обращение к старому бэкенду удалено окончательно.
            // Важно! Перепроведение реализовано не для всех операций.
            // Реализованы операции, связанные с другими документами (и перепроводящиеся через них),
            // либо самые часто используемые, либо те, для которых проведение было реализовано изначально.
            // Для остальных перепроведение автоматически не вызывается.
            // Если появится необходимость перепровести нереализованные операции вручную через апи,
            // то вам придеться реализовать для них проведение на событиях самостоятельно..
            throw new NotImplementedException($"Implementation of IConcretePaymentOrderProvider for operation type {operationType} ({(int)operationType}) is not found");
        }

        private async Task ProvideWithCatchAsync(IConcretePaymentOrderProvider provider, OperationTypeResponse operationTypeResponse)
        {
            try
            {
                await provider.ProvideAsync(operationTypeResponse.DocumentBaseId);
            }
            catch (BusinessValidationException vex)
            {
                // игнорируем валидацию, т.к. она проверяет только закрытый период
                // а операции из закрытого периода перепроводить нельзя
                logger.LogWarning(vex, $"Unable to remove operation with base id {operationTypeResponse.DocumentBaseId}", operationTypeResponse);
            }
        }
    }
}
