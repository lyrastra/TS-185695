using System;
using Moedelo.Common.Kafka.Models;

namespace Moedelo.Common.Kafka.Extensions;

internal static class ServiceKafkaBalanceStateExtensions
{
    internal static KafkaRebalanceRequirements GetRebalanceRequirements(
        this ServiceKafkaBalanceState info,
        int runningConsumerCount)
    {
        if (info == null)
        {
            // информация отсутствует - надо обновить данные о запущенных консьюмерах
            return KafkaRebalanceRequirements.UpdateRunningInfo(runningConsumerCount);
        }

        if (info.Quota == null)
        {
            // квота не задана - ничего делать не надо
            return KafkaRebalanceRequirements.Empty;
        }

        return new KafkaRebalanceRequirements
        {
            Quota = info.Quota.Value,
            RunningCount = runningConsumerCount,
            StartNewCount = info.GetMissedCount(runningConsumerCount),
            StopCount = info.GetRedundantCount(runningConsumerCount),
            NeedToSetWorkingStatus = true 
        };
    }

    // количество лишних (= запущено больше, чем надо по квоте)
    private static int GetRedundantCount(
        this ServiceKafkaBalanceState info,
        int runningConsumerCount)
    {
        if (!info.Quota.HasValue)
        {
            return 0;
        }

        return Math.Max(0, runningConsumerCount - info.Quota.Value);
    }
        
    // количество отсутствующих (= запущено меньше, чем надо по квоте)
    private static int GetMissedCount(this ServiceKafkaBalanceState info, int runningConsumerCount)
    {
        if (!info.Quota.HasValue)
        {
            return 0;
        }

        return Math.Max(0, info.Quota.Value - runningConsumerCount);
    }

}