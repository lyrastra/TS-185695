using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Moedelo.Money.Business.Payroll;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderWithMissingEmployeeUpdater))]
    internal class PaymentOrderWithMissingEmployeeUpdater : IPaymentOrderWithMissingEmployeeUpdater
    {
        private readonly ILogger logger;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly Dictionary<OperationType, IConcretePaymentOrderWithMissingEmployeeUpdater> updaters;
        private readonly IEmployeeReader employeeReader;

        public PaymentOrderWithMissingEmployeeUpdater(
            ILogger<PaymentOrderWithMissingEmployeeUpdater> logger,
            IPaymentOrderGetter paymentOrderGetter,
            IEnumerable<IConcretePaymentOrderWithMissingEmployeeUpdater> updaters, IEmployeeReader employeeReader)
        {
            this.logger = logger;
            this.paymentOrderGetter = paymentOrderGetter;
            this.employeeReader = employeeReader;
            this.updaters = updaters.Select(x =>
                    new
                    {
                        x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                        Updater = x
                    })
                .ToDictionary(x => x.OperationType, x => x.Updater);
        }

        public async Task UpdateAsync(int employeeId, long[] paymentOrdersBaseIds)
        {
            var employee = await employeeReader.GetByIdAsync(employeeId);

            if (employee == null)
            {
                throw new InvalidOperationException($"Employee with Id {employeeId} not found");
            }

            foreach (var paymentOrderBaseId in paymentOrdersBaseIds)
            {
                try
                {
                    var operationType = await paymentOrderGetter.GetOperationTypeAsync(paymentOrderBaseId);
                    if (updaters.TryGetValue(operationType, out var updater) == false)
                    {
                        throw new NotImplementedException(
                            $"Implementation of IConcretePaymentOrderWithMissingEmployeeUpdater for operation type {operationType} ({(int) operationType}) is not found");
                    }

                    await updater.UpdateAsync(paymentOrderBaseId, employee);
                }
                catch (OperationNotFoundException)
                {
                    logger.LogWarning(
                        $"Unable to change missing worker for payment order with DocumentBaseId = {paymentOrderBaseId}. Operation not found");
                }
            }
        }
    }
}