namespace Moedelo.Common.Kafka.Models;

public struct KafkaRebalanceRequirements
{
    public static KafkaRebalanceRequirements Empty => new ();
        
    public static KafkaRebalanceRequirements UpdateRunningInfo(int runningConsumerCount) => new () 
    {
        Quota = runningConsumerCount,
        RunningCount = runningConsumerCount,
        StartNewCount = 0,
        StopCount = 0,
        NeedToSetWorkingStatus = true 
    }; 

    /// <summary>
    /// Сколько надо стартовать новых консьюмеров
    /// </summary>
    public int StartNewCount { get; set; } 

    /// <summary>
    /// Количество консьюмеров, которые надо остановить
    /// </summary>
    public int StopCount { get; set; }

    /// <summary>
    /// Необходимо обновить статус
    /// </summary>
    public bool NeedToSetWorkingStatus { get; set; }

    /// <summary>
    /// Выставленная квота
    /// </summary>
    public int Quota { get; set; }

    /// <summary>
    /// Количество запущенных консьюмеров
    /// </summary>
    public int RunningCount { get; set; }

    public bool IsEmpty()
    {
        return StartNewCount == 0 && StopCount == 0 && !NeedToSetWorkingStatus;
    }
}