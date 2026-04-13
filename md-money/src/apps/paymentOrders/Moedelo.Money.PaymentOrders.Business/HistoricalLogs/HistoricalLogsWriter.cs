using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs;
using Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs.Commands;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

namespace Moedelo.Money.PaymentOrders.Business.HistoricalLogs
{
    [InjectAsSingleton(typeof(IHistoricalLogsCommandWriter))]
    internal class HistoricalLogsCommandWriter : IHistoricalLogsCommandWriter
    {
        private readonly IOperationLogsCommandWriter commandWriter;

        public HistoricalLogsCommandWriter(
            IOperationLogsCommandWriter commandWriter)
        {
            this.commandWriter = commandWriter;
        }

        public Task WriteAsync(LogOperationType operationType, PaymentOrder paymentOrder)
        {
            if (paymentOrder == null)
            {
                throw new ArgumentNullException(nameof(paymentOrder));
            }

            var command = new WriteOperationLog
            {
                OperationType = operationType,
                ObjectType = LogObjectType.PaymentOrder,
                ObjectId = paymentOrder.DocumentBaseId,
                ObjectData = paymentOrder.ToJsonString()
            };
            return commandWriter.WriteLogAsync(command);
        }

        public Task WriteActualizeAsync(IReadOnlyCollection<ActualizeRequestItem> items)
        {
            return WriteMultipleRecordsAsync(
                items,
                item =>
                    new WriteOperationLog
                    {
                        OperationType = LogOperationType.Update,
                        ObjectType = LogObjectType.PaymentOrder,
                        ObjectId = item.DocumentBaseId,
                        ObjectData = new { item.DocumentBaseId, item.Date, PaidStatus = 6, ProvideInAccounting = true }.ToJsonString()
                    });
        }

        public Task WriteApprovedAsync(IReadOnlyCollection<long> baseIds)
        {
            return WriteMultipleRecordsAsync(
                baseIds,
                documentBaseId => new WriteOperationLog
                {
                    OperationType = LogOperationType.Update,
                    ObjectType = LogObjectType.PaymentOrder,
                    ObjectId = documentBaseId,
                    ObjectData = new { documentBaseId, OperationState = 0 }.ToJsonString()
                });
        }

        public async Task WriteOutsourceStateUpdatedAsync(int userId, IReadOnlyList<OutsourceStateUpdateResult> updated)
        {
            var commands = updated
                .Select(x => new IdentifiedWriteOperationLog
            {
                FirmId = x.FirmId,
                UserId = userId,
                OperationType = LogOperationType.Update,
                ObjectType = LogObjectType.PaymentOrder,
                ObjectId = x.DocumentBaseId,
                ObjectData = new { x.DocumentBaseId, x.OutsourceState }.ToJsonString()
            });

            await commands.RunParallelAsync(c => commandWriter.WriteLogAsync(c));
        }

        private Task WriteMultipleRecordsAsync<T>(IEnumerable<T> items, Func<T, WriteOperationLog> mapper)
        {
            return items.RunParallelAsync(item => commandWriter.WriteLogAsync(mapper(item)));
        }
    }
}
