using System.Threading;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(IPaymentOrderChangeTaxationSystemCommandWriter))]
    internal sealed class PaymentOrderChangeTaxationSystemCommandWriter : MoedeloKafkaTopicWriterBase, IPaymentOrderChangeTaxationSystemCommandWriter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public PaymentOrderChangeTaxationSystemCommandWriter(
            IExecutionInfoContextAccessor contextAccessor,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.contextAccessor = contextAccessor;
        }

        public Task WriteAsync(PaymentOrderChangeTaxationSystemCommand command)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var key = context.FirmId.ToString();
            var value = new PaymentOrderChangeTaxationSystemCommandMessageValue
            {
                DocumentBaseId = command.DocumentBaseId,
                TaxationSystemType = command.TaxationSystemType,
                Guid = command.Guid
            };
            return WriteAsync(MoneyTopics.Commands.PaymentOrderChangeTaxationSystemCommand, key, value, CancellationToken.None);
        }
    }
}
