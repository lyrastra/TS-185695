using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills
{
    /// <summary>
    /// Чтение событий Накладных из Продаж:
    /// <list type="bullet">
    ///     <item><term>SaleWaybillCreated (создание)</term></item>
    ///     <item><term>SaleWaybillUpdated (обновление)</term></item>
    ///     <item><term>SaleWaybillDeleted (удаление)</term></item>
    ///     <item><term>SaleWaybillPaymentsUpdated (обновление платежей)</term></item>
    ///     <item><term>SaleWaybillSignStatusUpdated (обновление признака "Подписан")</term></item>
    ///     <item><term>SaleWaybillLinkedInvoiceUpdated (обновлена связь со сч-ф)</term></item>
    ///     <item><term>SaleWaybillUseStampAndSignUpdated (обновление признака "Печать и подпись")</term></item>
    /// </list>
    /// </summary>
    public interface ISaleWaybillEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
    }
}