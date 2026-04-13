using System.Threading;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands.CashOrders;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(ICashOrderChangeTaxationSystemCommandWriter))]
    internal sealed class CashOrderChangeTaxationSystemCommandWriter : MoedeloKafkaTopicWriterBase, ICashOrderChangeTaxationSystemCommandWriter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public CashOrderChangeTaxationSystemCommandWriter(
            IExecutionInfoContextAccessor contextAccessor,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.contextAccessor = contextAccessor;
        }

        public Task WriteAsync(CashOrderChangeTaxationSystemCommand command)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var key = context.FirmId.ToString();
            var value = new CashOrderChangeTaxationSystemCommandMessageValue
            {
                DocumentBaseId = command.DocumentBaseId,
                TaxationSystemType = command.TaxationSystemType,
                Guid = command.Guid
            };
            return WriteAsync(MoneyTopics.Commands.CashOrderChangeTaxationSystemCommand, key, value, CancellationToken.None);
        }
    }
}
