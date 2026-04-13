using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Upds
{
    /// <summary>
    /// Чтение событий УПД из Покупок:
    /// <list type="bullet">
    ///     <item><term>PurchaseUpdCreated (создание)</term></item>
    ///     <item><term>PurchaseUpdUpdated (обновление)</term></item>
    ///     <item><term>PurchaseUpdDeleted (удаление)</term></item>
    ///     <item><term>PurchaseUpdNdsDeductionsUpdated (изменение вычетов НДС, из виджета "НДС" у ОСНО)</term></item>
    /// </list>
    /// </summary>
    public interface IPurchaseUpdEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
    }
}