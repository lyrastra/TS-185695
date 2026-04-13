using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IBudgetaryPaymentCommandReaderBuilder OnImport(Func<ImportBudgetaryPayment, Task> onCommand);
        IBudgetaryPaymentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateBudgetaryPayment, Task> onCommand);
    }
}