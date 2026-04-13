using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderDetailUpdater))]
    internal sealed class PaymentOrderDetailUpdater : IPaymentOrderDetailUpdater
    {
        private readonly IReadOnlyDictionary<OperationType, IConcretePaymentDetailUpdater> detailUpdaters;
        private readonly IPaymentOrderGetter getter;
        private readonly ILogger<PaymentOrderDetailUpdater> logger;

        
        public PaymentOrderDetailUpdater(
            IEnumerable<IConcretePaymentDetailUpdater> detailUpdaters,
            IPaymentOrderGetter getter,
            ILogger<PaymentOrderDetailUpdater> logger)
        {
            this.getter = getter;
            this.logger = logger;
            this.detailUpdaters = detailUpdaters.Select(x =>
                    new
                    {
                        x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                        Updater = x
                    })
                .ToDictionary(x => x.OperationType, x => x.Updater);
        }

        public async Task UpdateAsync(ChangeIsPaidRequestItem item)
        {
            try
            {
                var operationTypeResponses = await getter.GetOperationTypeByBaseIdsAsync(new []{item.DocumentBaseId});
            
                if (operationTypeResponses.Length <= 0)
                {
                    return;
                }
            
                var detailUpdater = GetDetailUpdater(operationTypeResponses.First().OperationType);
                await detailUpdater.UpdateAsync(item);
            }
            catch (NotImplementedException notImpException)
            {
                logger.LogError(notImpException, $"{notImpException.Message},  item: {item.ToJsonString()}.");
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"Failed to update payment order detail,  item: {item.ToJsonString()}.");
            }
        }
        
        private IConcretePaymentDetailUpdater GetDetailUpdater(OperationType operationType)
        {
            return detailUpdaters.TryGetValue(operationType, out var detailUpdater) ? 
                detailUpdater :  
                throw new NotImplementedException($"Implementation of IConcretePaymentDetailUpdater for operation type {operationType} ({(int)operationType}) is not found");
        }
    }
}