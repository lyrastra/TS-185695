using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.Abstractions.CashOrders;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderRemover))]
    internal sealed class CashOrderRemover : ICashOrderRemover
    {
        private readonly ILogger logger;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ICashOrderGetter cashOrderGetter;
        private readonly ICashOrderApiClient cashOrderApiClient;

        private readonly Dictionary<OperationType, IConcreteCashOrderRemover> removers;
        public CashOrderRemover(
            ILogger<CashOrderRemover> logger,
            IExecutionInfoContextAccessor contextAccessor,
            ICashOrderGetter cashOrderGetter,
            ICashOrderApiClient cashOrderApiClient,
            IEnumerable<IConcreteCashOrderRemover> removers)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.cashOrderGetter = cashOrderGetter;
            this.cashOrderApiClient = cashOrderApiClient;
            this.removers = removers.Select(x =>
                 new
                 {
                     x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                     Remover = x
                 })
                .ToDictionary(x => x.OperationType, x => x.Remover);
        }

        public async Task DeleteAsync(long documentBaseId)
        {
            var operationType = await cashOrderGetter.GetOperationTypeAsync(documentBaseId);
            var remover = GetRemover(operationType);
            await remover.DeleteAsync(documentBaseId);
        }

        public async Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.Count <= 0)
            {
                return;
            }

            var operationTypeResponses = await cashOrderGetter.GetOperationTypeByBaseIdsAsync(documentBaseIds);
            if (operationTypeResponses.Length <= 0)
            {
                return;
            }

            var (newDelete, oldDelete) = GetOperationTypesForDelete(operationTypeResponses);

            // todo: по возможности переписать на массовое удаление
            await newDelete
                .RunParallelAsync(operationTypeResponse =>
                {
                    var remover = GetRemover(operationTypeResponse.OperationType);

                    return DeleteWithCatchAsync(remover, operationTypeResponse);
                });

            if (oldDelete.Count <= 0)
            {
                return;
            }
            var context = contextAccessor.ExecutionInfoContext;
            await cashOrderApiClient.DeleteAsync(context.FirmId, context.UserId, oldDelete);
        }

        private
            (IReadOnlyCollection<OperationTypeResponse> newDelete,
            IReadOnlyCollection<long> oldDelete)
            GetOperationTypesForDelete(IReadOnlyCollection<OperationTypeResponse> operationTypeResponses)
        {
            var operationsByTypesMap = operationTypeResponses
                .GroupBy(x => x.OperationType)
                .ToDictionary(x => x.Key);

            var operationTypesForNewDelete = new List<OperationTypeResponse>(operationTypeResponses.Count);
            var operationTypesForOldDelete = new List<long>(operationTypeResponses.Count);

            foreach (var operationTypeResponse in operationTypeResponses)
            {
                if (removers.ContainsKey(operationTypeResponse.OperationType))
                {
                    operationTypesForNewDelete.Add(operationTypeResponse);
                }
                else
                {
                    operationTypesForOldDelete.Add(operationTypeResponse.DocumentBaseId);
                }
            }

            return (operationTypesForNewDelete, operationTypesForOldDelete);
        }

        private IConcreteCashOrderRemover GetRemover(OperationType operationType)
        {
            if (removers.TryGetValue(operationType, out var remover))
            {
                return remover;
            }
            throw new NotImplementedException($"Implementation of IConcreteCashOrderRemover for operation type {operationType} ({(int)operationType}) is not found");
        }

        public async Task DeleteWithCatchAsync(IConcreteCashOrderRemover remover, OperationTypeResponse operationTypeResponse)
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
            catch (OperationNotFoundException)
            {
                //throw;
            }
        }
    }
}
