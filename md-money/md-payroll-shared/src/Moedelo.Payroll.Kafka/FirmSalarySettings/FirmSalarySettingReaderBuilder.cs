using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions.Events;

namespace Moedelo.Payroll.Kafka.FirmSalarySettings
{
    [InjectAsSingleton]
    internal sealed class FirmSalarySettingReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IFirmSalarySettingReaderBuilder
    {
        private const string EntityType = "FirmSalarySetting";

        public FirmSalarySettingReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  PayrollTopics.Events.FirmSalarySetting,
                  EntityType,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IFirmSalarySettingReaderBuilder OnChanged(Func<FirmSalarySettingChanged, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
        
        public IFirmSalarySettingReaderBuilder OnChangedAutoPaymentSetting(Func<AutoPaymentSettingChanged, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IFirmSalarySettingReaderBuilder OnChangedChargeSettings(Func<FirmChargeSettingChanged, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}