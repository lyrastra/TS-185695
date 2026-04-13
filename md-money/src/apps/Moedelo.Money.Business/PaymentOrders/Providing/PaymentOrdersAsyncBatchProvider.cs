using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Providing
{
    [InjectAsSingleton(typeof(IPaymentOrdersAsyncBatchProvider))]
    internal sealed class PaymentOrdersAsyncBatchProvider : IPaymentOrdersAsyncBatchProvider
    {
        private const int BatchSize = 1000;

        private readonly PaymentOrdersBatchProvideCommandWriter batchProvideCommandWriter;
        private readonly PaymentOrderProvidingStateSetter providingStateSetter;

        public PaymentOrdersAsyncBatchProvider(
            PaymentOrderProvidingStateSetter providingStateSetter,
            PaymentOrdersBatchProvideCommandWriter batchProvideCommandWriter)
        {
            this.batchProvideCommandWriter = batchProvideCommandWriter;
            this.providingStateSetter = providingStateSetter;
        }

        public async Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            // создаем команды по ограниченному количеству операций,
            // т.к. иначе выпадем из времени обработки команды
            foreach (var operationBaseIdsChunk in documentBaseIds.Distinct().Chunk(BatchSize))
            {
                // cоздаем фейковое состояние проведения, чтобы сэмулировать очередь,
                // пока не начнутся проведения отдельных операция
                var batchProvidingStateId = await providingStateSetter.SetStateAsync(DateTime.UtcNow.Ticks);

                var batchProvideCommand = new PaymentOrdersBatchProvideCommand
                {
                    DocumentBaseIds = operationBaseIdsChunk,
                    ProvidingStateId = batchProvidingStateId
                };
                await batchProvideCommandWriter.WriteAsync(batchProvideCommand);
            }
        }
    }
}
