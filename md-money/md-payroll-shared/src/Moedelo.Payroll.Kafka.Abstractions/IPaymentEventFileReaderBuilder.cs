using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Payroll.Kafka.Abstractions.Events;

namespace Moedelo.Payroll.Kafka.Abstractions
{
    public interface IPaymentEventFileReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentEventFileReaderBuilder OnPaymentEventFileCreated(Func<PaymentEventFileCreated, Task> onEvent);
        
        IPaymentEventFileReaderBuilder OnAutoPaymentsErrorOccurred(Func<AutoPaymentError, Task> onEvent);
    }
}