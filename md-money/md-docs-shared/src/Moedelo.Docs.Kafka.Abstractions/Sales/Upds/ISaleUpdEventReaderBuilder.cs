using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Upds
{
    /// <summary>
    /// Чтение событий УПД из Продаж:
    /// <list type="bullet">
    ///     <item><term>SaleUpdCreated (создание)</term></item>
    ///     <item><term>SaleUpdUpdated (обновление)</term></item>
    ///     <item><term>SaleUpdDeleted (удаление)</term></item>
    ///     <item><term>SaleUpdPaymentsUpdated (обновление платежей)</term></item>
    ///     <item><term>SaleUpdBillUpdated (обновление счета)</term></item>
    ///     <item><term>SaleUpdSignStatusUpdated (обновление признака "Подписан")</term></item>
    ///     <item><term>SaleUpdStampAndSignUpdated (обновление признака "Печать и подпись")</term></item>
    /// </list>
    /// </summary>
    public interface ISaleUpdEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
    }
}