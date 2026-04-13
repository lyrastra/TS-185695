using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IBudgetaryPaymentEventReaderBuilder OnCreated(Func<BudgetaryPaymentCreated, Task> onEvent);
        IBudgetaryPaymentEventReaderBuilder OnUpdated(Func<BudgetaryPaymentUpdated, Task> onEvent);
        IBudgetaryPaymentEventReaderBuilder OnDeleted(Func<BudgetaryPaymentDeleted, Task> onEvent);

        IBudgetaryPaymentEventReaderBuilder OnProvideRequired(Func<BudgetaryPaymentProvideRequired, Task> onEvent);
        IBudgetaryPaymentEventReaderBuilder OnUpdateAfterAccountingStatementCreated(Func<BudgetaryPaymentUpdateAfterAccountingStatementCreated, Task> onEvent);

    }
}