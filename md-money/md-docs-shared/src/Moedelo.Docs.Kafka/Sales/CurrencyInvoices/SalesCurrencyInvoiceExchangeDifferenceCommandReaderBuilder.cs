using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices;
using Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Commands;
using Moedelo.Docs.Kafka.Abstractions.Topics.ByApps;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Sales.CurrencyInvoices
{
    /// <summary>
    /// Reader для команд на перерасчет курсовой разницы при изменении платежа
    /// </summary>
    [InjectAsSingleton(typeof(ISalesCurrencyInvoiceExchangeDifferenceCommandReaderBuilder))]
    public class SalesCurrencyInvoiceExchangeDifferenceCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ISalesCurrencyInvoiceExchangeDifferenceCommandReaderBuilder
    {
        public SalesCurrencyInvoiceExchangeDifferenceCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                CurrencyInvoiceTopics.Sales.Command.RecalculateExchangeDifference,
                nameof(CurrencyInvoiceTopics.Sales.Command.RecalculateExchangeDifference),
                reader,
                replyWriter,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ISalesCurrencyInvoiceExchangeDifferenceCommandReaderBuilder OnCommand(Func<RecalculateSalesCurrencyDocsExchangeDifference, Task> onCommand)
        {
            OnCommand<RecalculateSalesCurrencyDocsExchangeDifference>(onCommand);
            return this;
        }
    }
}