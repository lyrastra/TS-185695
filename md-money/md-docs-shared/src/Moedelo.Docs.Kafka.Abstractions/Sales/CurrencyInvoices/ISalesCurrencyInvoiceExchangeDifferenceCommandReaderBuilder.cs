using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Commands;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices
{
    /// <summary>
    /// Страктура Reader-а для команд на перерасчет курсовой разницы при изменении платежа
    /// </summary>
    public interface ISalesCurrencyInvoiceExchangeDifferenceCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ISalesCurrencyInvoiceExchangeDifferenceCommandReaderBuilder OnCommand(Func<RecalculateSalesCurrencyDocsExchangeDifference, Task> onCommand);
    }
}