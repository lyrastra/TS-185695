using Moedelo.HistoricalLogs.Enums;
using Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs;
using Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs.Commands;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.CashOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.CashOrders.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.HistoricalLogs
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

        public Task WriteAsync(LogOperationType operationType, CashOrder cashOrder)
        {
            if (cashOrder == null)
            {
                throw new ArgumentNullException(nameof(cashOrder));
            }

            var command = new WriteOperationLog
            {
                OperationType = operationType,
                ObjectType = LogObjectType.CashOrder,
                ObjectId = cashOrder.DocumentBaseId,
                ObjectData = cashOrder.ToJsonString()
            };
            
            return commandWriter.WriteLogAsync(command);
        }
    }
}