using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements
{
    public interface IAdvanceStatementsEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IAdvanceStatementsEventReaderBuilder OnPaymentToSupplierCreated(Func<PaymentToSupplierAdvanceStatementCreatedMessage, Task> onEvent);

        IAdvanceStatementsEventReaderBuilder OnPaymentToSupplierUpdated(Func<PaymentToSupplierAdvanceStatementUpdatedMessage, Task> onEvent);

        IAdvanceStatementsEventReaderBuilder OnDeleted(Func<AdvanceStatementDeletedMessage, Task> onEvent);
        
        IAdvanceStatementsEventReaderBuilder OnBusinessTripCreated(Func<BusinessTripAdvanceStatementCreatedMessage, Task> onEvent);

        IAdvanceStatementsEventReaderBuilder OnBusinessTripUpdated(Func<BusinessTripAdvanceStatementUpdatedMessage, Task> onEvent);

        IAdvanceStatementsEventReaderBuilder OnGoodsAndServicesCreated(Func<GoodsAndServicesAdvanceStatementCreatedMessage, Task> onEvent);

        IAdvanceStatementsEventReaderBuilder OnGoodsAndServicesUpdated(Func<GoodsAndServicesAdvanceStatementUpdatedMessage, Task> onEvent);
    }
}
