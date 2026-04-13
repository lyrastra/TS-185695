using System;

namespace Moedelo.Common.Kafka.Models;

public class ServiceKafkaBalanceState
{
    /// <summary>
    /// Статус серсива
    /// </summary>
    public StatusWatchConsumer Status { get; set; }
    /// <summary>
    /// Количество запущенных консьюмеров (заполняется сервисом)
    /// </summary>
    public int Run { get; set; }
    /// <summary>
    /// Квота на консьюмеров (заполняется оркестратором)
    /// </summary>
    public int? Quota { get; set; }
    /// <summary>
    /// Время фиксации состояния (техническое поле)
    /// </summary>
    public DateTime At { get; set; }
}