using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions.Events;

namespace Moedelo.Payroll.Kafka.AutoPayment
{
    [InjectAsSingleton(typeof(IAutoPaymentReaderBuilder))]
    public class AutoPaymentReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IAutoPaymentReaderBuilder
    {
        private const string EntityType = "AutoPayment";

        public AutoPaymentReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                PayrollTopics.Events.AutoPayment,
                EntityType,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IAutoPaymentReaderBuilder OnWorkContractWithDebt(Func<WorkContractWithDebtEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
        
        public IAutoPaymentReaderBuilder OnSfrInjuredWithDebt(Func<SfrInjuredWithDebtEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}