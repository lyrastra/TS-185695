using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IBudgetaryPaymentEventReaderBuilder OnCreated(Func<BudgetaryPaymentCreated, Task> onEvent);

        IBudgetaryPaymentEventReaderBuilder OnUpdated(Func<BudgetaryPaymentUpdated, Task> onEvent);

        IBudgetaryPaymentEventReaderBuilder OnDeleted(Func<BudgetaryPaymentDeleted, Task> onEvent);
    }
}
