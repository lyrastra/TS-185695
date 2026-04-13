using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Waybills
{
    /// <summary>
    /// Чтение событий Накладных из Покупок:
    /// <list type="bullet">
    ///     <item><term>PurchaseWaybillCreated (создание)</term></item>
    ///     <item><term>PurchaseWaybillUpdated (обновление)</term></item>
    ///     <item><term>PurchaseWaybillDeleted (удаление)</term></item>
    ///     <item><term>PurchaseWaybillLinkedInvoiceUpdated (обновлена связь со сч-ф)</term></item>
    /// </list>
    /// </summary>
    public interface IPurchaseWaybillEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
    }
}