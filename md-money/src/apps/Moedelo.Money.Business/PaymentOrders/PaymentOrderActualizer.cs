using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderActualizer))]
    internal sealed class PaymentOrderActualizer : IPaymentOrderActualizer
    {
        private readonly ILogger<PaymentOrderActualizer> logger;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly Dictionary<OperationType, IConcreetePaymentOrderActualizer> actualizers;

        public PaymentOrderActualizer(
            ILogger<PaymentOrderActualizer> logger,
            IPaymentOrderGetter paymentOrderGetter,
            IEnumerable<IConcreetePaymentOrderActualizer> actualizers)
        {
            this.logger = logger;
            this.paymentOrderGetter = paymentOrderGetter;
            this.actualizers = actualizers.Select(x =>
                 (
                     x.GetType().GetCustomAttribute<OperationTypeAttribute>()!.OperationType,
                     Actualizer: x
                 ))
                .ToDictionary(x => x.OperationType, x => x.Actualizer);
        }

        public async Task ActualizeAsync(IReadOnlyCollection<ActualizeRequestItem> items)
        {
            if (items.Count <= 0)
            {
                return;
            }

            var itemsByDocumentBaseIds = items
                .GroupBy(x => x.DocumentBaseId)
                .ToDictionary(
                    k => k.Key,
                    v => v.MinBy(x => x.Date)
                );

            var operationTypeResponses = await paymentOrderGetter.GetOperationTypeByBaseIdsAsync(itemsByDocumentBaseIds.Keys);
            if (operationTypeResponses.Length <= 0)
            {
                return;
            }

            await operationTypeResponses.RunParallelAsync(operationTypeResponse =>
            {
                if (!actualizers.TryGetValue(operationTypeResponse.OperationType, out var actualizer))
                {
                    return Task.CompletedTask;
                }
                
                var item = itemsByDocumentBaseIds[operationTypeResponse.DocumentBaseId];
                return ActualizeWithCatchAsync(actualizer, item);
            });
        }
        
        private async Task ActualizeWithCatchAsync(
            IConcreetePaymentOrderActualizer actualizer,
            ActualizeRequestItem request)
        {
            try
            {
                await actualizer.ActualizeAsync(request);
            }
            catch (BusinessValidationException vex)
            {
                // игнорируем валидацию, т.к. она проверяет только закрытый период
                // а операции из закрытого периода актуализировать нельзя
                logger.LogWarning(vex, $"Unable to actualize operation with base id {request.DocumentBaseId} by {actualizer.GetType().FullName}");
            }
        }
    }
}
