using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment;

[InjectAsSingleton(typeof(IRentPaymentCommandReaderBuilder))]
internal sealed class RentPaymentCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IRentPaymentCommandReaderBuilder
{
    public RentPaymentCommandReaderBuilder(
        IMoedeloEntityCommandKafkaTopicReader reader,
        IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
        IExecutionInfoContextInitializer contextInitializer,
        IExecutionInfoContextAccessor contextAccessor,
        IAuditTracer auditTracer)
        : base(
            MoneyTopics.PaymentOrders.RentPayment.Command.Topic,
            MoneyTopics.PaymentOrders.RentPayment.EntityName,
            reader,
            replyWriter,
            contextInitializer,
            contextAccessor,
            auditTracer)
    {
    }

    public IRentPaymentCommandReaderBuilder OnImport(Func<ImportRentPayment, Task> onCommand)
    {
        OnCommand(onCommand);
        return this;
    }

    public IRentPaymentCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractRentPayment, Task> onCommand)
    {
        OnCommand(onCommand);
        return this;
    }

    public IRentPaymentCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorRentPayment, Task> onCommand)
    {
        OnCommand(onCommand);
        return this;
    }

    public IRentPaymentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateRentPayment, Task> onCommand)
    {
        OnCommand(onCommand);
        return this;
    }
}