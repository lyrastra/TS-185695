using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Common.Kafka.Abstractions;

/// <summary>
/// Автоматический балансировщик количества консьюмеров Kafka
/// </summary>
public interface IKafkaConsumerBalancer
{
    /// <summary>
    /// Запустить автоматическую балансировку консьюмеров
    /// </summary>
    /// <param name="autoBalanceSettings">настройки автобалансировки</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <typeparam name="TMessage">тип сообщения</typeparam>
    /// <returns>поток, ожидающий отзыва CancellationToken</returns>
    ValueTask StartAutoBalanceAsync<TMessage>(
        ConsumerAutoBalanceSettings<TMessage> autoBalanceSettings,
        CancellationToken cancellationToken) where TMessage : MoedeloKafkaMessageValueBase;
}