using System;

namespace Moedelo.Common.Kafka.Models;

public enum StatusWatchConsumer
{
    /// <summary>
    /// Запущен и готов к получению квоты (выставляется приложением)
    /// </summary>
    [Obsolete("Больше не нужен. Заменён на Working с Count = 0")]
    Created = 0,
    /// <summary>
    /// Работает согласно выданной квоте (выставляется приложением)
    /// </summary>
    Working = 1,
    /// <summary>
    /// Идёт применение выданной квоты (выставляется приложением)
    /// </summary>
    Awaiting = 2,
    /// <summary>
    /// Выдана квота (выставляется оркестратором)
    /// </summary>
    Granted = 3
}