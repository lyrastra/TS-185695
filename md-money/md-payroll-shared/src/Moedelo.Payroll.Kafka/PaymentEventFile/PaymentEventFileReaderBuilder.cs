using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions.Events;

namespace Moedelo.Payroll.Kafka.PaymentEventFile
{
    [InjectAsSingleton]
    internal sealed class PaymentEventFileReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentEventFileReaderBuilder
    {
        private const string EntityType = "PaymentEventFile";

        public PaymentEventFileReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  PayrollTopics.Events.PaymentEventFile,
                  EntityType,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentEventFileReaderBuilder OnPaymentEventFileCreated(Func<PaymentEventFileCreated, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
        
        public IPaymentEventFileReaderBuilder OnAutoPaymentsErrorOccurred(Func<AutoPaymentError, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}