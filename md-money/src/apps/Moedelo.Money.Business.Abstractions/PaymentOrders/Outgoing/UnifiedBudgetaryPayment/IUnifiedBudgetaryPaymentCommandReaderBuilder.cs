using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IUnifiedBudgetaryPaymentCommandReaderBuilder OnImport(Func<ImportUnifiedBudgetaryPayment, Task> onCommand);
        IUnifiedBudgetaryPaymentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateUnifiedBudgetaryPayment, Task> onCommand);
    }
}