using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Common.Kafka.Models;

namespace Moedelo.Common.Kafka.Extensions;

/// <summary>
/// Набор методов-расширений для IConsulHttpApiClient, используемых для целей автобалансировки консьюмеров Kafka 
/// </summary>
internal static class ConsulHttpApiClientKafkaBalancingExtensions
{
    /// <summary>
    /// Уведомление о том, что сервис приступил к балансировке - приведению количества консьюмеров в соответствие с выделенной квотой 
    /// </summary>
    /// <param name="consulApiClient">http-клиент для работы с консулом</param>
    /// <param name="context">Контекст балансировки</param>
    /// <param name="state">текущее состояние балансировки</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    internal static ValueTask NotifyAboutQuotaIsBeingAppliedAsync(
        this IMoedeloConsulApiClient consulApiClient,
        ServiceKafkaBalanceContext context,
        KafkaRebalanceRequirements state,
        CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = "NotifyAboutQuotaIsBeingApplied";
        
        var value = new ServiceKafkaBalanceState
        {
            Status = StatusWatchConsumer.Awaiting,
            Run = state.RunningCount,
            Quota = state.Quota,
            At = DateTime.Now
        }; 
        
        return consulApiClient.SaveKeyJsonValueAsync(context.ConsulKeyPath, value, cancellationToken, auditTrailSpanName);
    }

    /// <summary>
    /// Уведомление о том, что сервис закончил балансировку - количества консьюмеров соответствует выделенной квоте 
    /// </summary>
    /// <param name="consulApiClient">http-клиент для работы с консулом</param>
    /// <param name="context">Контекст балансировки</param>
    /// <param name="consumersCount">количество запущенных консьюмеров</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    internal static ValueTask NotifyAboutQuotaHasBeenAppliedAsync(
        this IMoedeloConsulApiClient consulApiClient,
        ServiceKafkaBalanceContext context,
        int consumersCount,
        CancellationToken cancellationToken)
    {
        const string auditTrailSpanName = "NotifyAboutQuotaHasBeenApplied";
        
        var value = new ServiceKafkaBalanceState
        {
            Status = StatusWatchConsumer.Working,
            Run = consumersCount,
            Quota = null,
            At = DateTime.Now
        }; 

        return consulApiClient.SaveKeyJsonValueAsync(
            context.ConsulKeyPath, value, cancellationToken, auditTrailSpanName);
    }
}
