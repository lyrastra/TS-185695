using System.Threading;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands.PurseOperations;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(IPurseOperationChangeTaxationSystemCommandWriter))]
    internal sealed class PurseOperationChangeTaxationSystemCommandWriter : MoedeloKafkaTopicWriterBase, IPurseOperationChangeTaxationSystemCommandWriter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public PurseOperationChangeTaxationSystemCommandWriter(
            IExecutionInfoContextAccessor contextAccessor,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.contextAccessor = contextAccessor;
        }

        public Task WriteAsync(PurseOperationChangeTaxationSystemCommand command)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var key = context.FirmId.ToString();
            var value = new PurseOperationChangeTaxationSystemCommandMessageValue
            {
                DocumentBaseId = command.DocumentBaseId,
                TaxationSystemType = command.TaxationSystemType,
                Guid = command.Guid
            };
            return WriteAsync(MoneyTopics.Commands.PurseOperationChangeTaxationSystemCommand, key, value, CancellationToken.None);
        }
    }
}
