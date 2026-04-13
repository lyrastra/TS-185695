using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Payroll.Kafka.Abstractions.Events;

namespace Moedelo.Payroll.Kafka.Abstractions
{
    public interface IFirmSalarySettingReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IFirmSalarySettingReaderBuilder OnChanged(Func<FirmSalarySettingChanged, Task> onEvent);

        IFirmSalarySettingReaderBuilder OnChangedAutoPaymentSetting(Func<AutoPaymentSettingChanged, Task> onEvent);

        IFirmSalarySettingReaderBuilder OnChangedChargeSettings(Func<FirmChargeSettingChanged, Task> onEvent);
    }
}