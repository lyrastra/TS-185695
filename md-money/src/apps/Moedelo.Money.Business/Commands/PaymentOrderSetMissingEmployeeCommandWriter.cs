using System.Threading;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders.MissingEmployee;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(IPaymentOrderSetMissingEmployeeCommandWriter))]
    internal sealed class PaymentOrderSetMissingEmployeeCommandWriter : MoedeloKafkaTopicWriterBase, IPaymentOrderSetMissingEmployeeCommandWriter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public PaymentOrderSetMissingEmployeeCommandWriter(
            IExecutionInfoContextAccessor contextAccessor,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.contextAccessor = contextAccessor;
        }

        public Task WriteAsync(PaymentOrderSetMissingEmployeeCommand command)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var key = context.FirmId.ToString();
            var value = new PaymentOrderSetMissingEmployeeCommandMessageValue
            {
                EmployeeId = command.EmployeeId,
                DocumentBaseIds = command.DocumentBaseIds
            };

            return WriteAsync(MoneyTopics.Commands.PaymentOrderSetMissingEmployeeCommand, key, value, CancellationToken.None);
        }
    }
}
