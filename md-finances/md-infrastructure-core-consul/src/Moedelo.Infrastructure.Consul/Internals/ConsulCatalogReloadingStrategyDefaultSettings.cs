using System;

namespace Moedelo.Infrastructure.Consul.Internals;

internal static class ConsulCatalogReloadingStrategyDefaultSettings
{
    /// <summary>
    /// тайминги пауз между попытками загрузить повторно неизменяемый каталог
    /// </summary>
    public static TimeSpan[] StaticCatalogReloadingDelays => new[]
    {
        TimeSpan.FromMinutes(1), // нет ошибок (этот элемент не будет никогда выбираться)
        TimeSpan.FromSeconds(1), // одна ошибка подряд
        TimeSpan.FromSeconds(1), // две ошибки подряд
        TimeSpan.FromSeconds(1), // три ошибки подряд
        TimeSpan.FromSeconds(2), // четыре ошибки подряд
        TimeSpan.FromSeconds(30) // пять и более ошибок подряд
    };

    /// <summary>
    /// тайминги пауз между попытками загрузить повторно каталог, за которым ведётся наблюдение изменений
    /// </summary>
    public static TimeSpan[] WatchedCatalogReloadingDelays
    {
        get
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            return new[]
            {
                TimeSpan.FromMinutes(1), // нет ошибок (этот элемент не будет никогда выбираться)
                // сначала делаем несколько быстрых попыток перезагрузиться
                TimeSpan.FromMilliseconds(random.Next(500, 1000)),
                TimeSpan.FromMilliseconds(random.Next(500, 1000)),
                TimeSpan.FromMilliseconds(random.Next(500, 1000)),
                // потом несколько попыток с большей паузой
                TimeSpan.FromMilliseconds(random.Next(3000, 5000)),
                TimeSpan.FromMilliseconds(random.Next(3000, 5000)),
                TimeSpan.FromMilliseconds(random.Next(3000, 5000)),
                // и уходим в режим проверки доступности consul раз в полминуты
                TimeSpan.FromSeconds(random.Next(30, 40))
            };
        }
    }
}
