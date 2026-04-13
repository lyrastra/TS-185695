using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Payroll.Kafka.Abstractions.Events;

namespace Moedelo.Payroll.Kafka.Abstractions
{
    public interface IAutoPaymentReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IAutoPaymentReaderBuilder OnWorkContractWithDebt(
            Func<WorkContractWithDebtEvent, Task> onEvent);

        IAutoPaymentReaderBuilder OnSfrInjuredWithDebt(Func<SfrInjuredWithDebtEvent, Task> onEvent);
    }
}