using System.Threading;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(IChangeTaxationSystemCommandWriter))]
    internal sealed class ChangeTaxationSystemCommandWriter : MoedeloKafkaTopicWriterBase, IChangeTaxationSystemCommandWriter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public ChangeTaxationSystemCommandWriter(
            IExecutionInfoContextAccessor contextAccessor,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.contextAccessor = contextAccessor;
        }

        public Task WriteAsync(ChangeTaxationSystemCommand command)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var key = context.FirmId.ToString();
            var value = new ChangeTaxationSystemCommandMessageValue
            {
                DocumentBaseIds = command.DocumentBaseIds,
                TaxationSystemType = command.TaxationSystemType,
                Guid = command.Guid
            };
            return WriteAsync(MoneyTopics.Commands.ChangeTaxationSystemCommand, key, value, CancellationToken.None);
        }
    }
}
