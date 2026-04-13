# Moedelo.InfrastructureV2.RedisDataAccess

## Владелец

Architecture Team

## Назначение

Высокоуровневая инфраструктурная библиотека для работы с Redis в корпоративных .NET-приложениях на фреймворке .NET Framework 4.7.2. Предоставляет готовые к использованию Redis executors с интеграцией auditTrail, настроек из Consul и автоматическим key prefixing.

## Основные возможности

- Готовые реализации `IRedisDbExecuter` для различных Redis-баз (Token, Audit, Default, и др.)
- Интеграция с AuditTrail — автоматическое логирование всех Redis операций
- Автоматическое добавление префиксов к ключам (key prefixing)
- Получение настроек подключения из Consul
- Fluent API для Redis Batch операций

## Архитектура

Этот модуль является **верхним слоем** инфраструктуры Redis и предназначен для использования в прикладном коде:

- **Нижний слой**: `md-infrastructure-core-redisdataaccess` — низкоуровневая работа с StackExchange.Redis, используется только инфраструктурой
- **Верхний слой** (этот модуль): удобные абстракции для разработчиков

### Два типа Redis

В инфраструктуре используются два экземпляра Redis с разными уровнями гарантий сохранности данных:

- **Persistent Redis** (`RedisDbEnum`) — данные периодически сбрасываются на диск, есть существенная вероятность восстановления данных после критического сбоя. Используется для токенов, кодов авторизации и другой критичной информации.
- **Cache Redis** (`RedisCacheDbEnum`) — только для временных данных, потеря которых некритична. Используется для кэширования контекста, справочников, заголовков и т.д.

## Состав

- **Moedelo.InfrastructureV2.RedisDataAccess** — реализации executors (TokenRedisDbExecutor, AuditRedisDbExecutor, и др.)
- **Moedelo.InfrastructureV2.Domain** (частично) — интерфейсы `IRedisBatch`, `IRedisDbExecuter`, модели `RedisBatchResult`

## Зависимости

- `md-infrastructure-core-redisdataaccess` — низкоуровневая работа с Redis
- `Moedelo.InfrastructureV2.Audit` — интеграция audit
- `Moedelo.InfrastructureV2.Setting` — получение настроек из Consul

## Совместимость

- .NET Framework 4.7.2+
- .NET Standard 2.0+ (Domain)
- Требует настроенного Consul для получения настроек подключения

## Подключение к проекту

1. Убедитесь, что сабмодуль `md-infrastructurev2` подключен к корню вашего репозитория:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructurev2.git
   ```

2. Добавьте необходимые проекты в ваш solution (.sln):
   - `Moedelo.InfrastructureV2.Domain`
   - `Moedelo.InfrastructureV2.RedisDataAccess`

3. Настройка DI происходит автоматически через атрибуты `[InjectAsSingleton]`

## Быстрый старт (рекомендуемый подход)

### Шаг 1: Создание DbExecutor

Создайте наследника от `RedisDbExecuterBase` в вашем проекте:

```csharp
[InjectAsSingleton(typeof(IMyFeatureRedisDbExecuter))]
internal sealed class MyFeatureRedisDbExecutor : RedisDbExecuterBase, IMyFeatureRedisDbExecuter
{
    public MyFeatureRedisDbExecutor(
        ISettingRepository settingRepository,
        IRedisDbExecutor redisExecutor,
        IAuditTracer auditTracer)
        : base(
            redisExecutor,
            settingRepository,
            settingRepository.Get("RedisConnection"),  // или "RedisCacheConnection" для cache
            (int)RedisDbEnum.MyFeatureDb,              // или RedisCacheDbEnum для cache
            auditTracer)
    {
    }
}
```

При необходимости добавьте новый номер БД в `RedisDbEnum` (persistent) или `RedisCacheDbEnum` (cache).

### Шаг 2: Создание DAO

Создайте интерфейс и реализацию DAO в вашем проекте с методами только для вашего бизнес-контекста:

```csharp
public interface IMyFeatureDao
{
    Task<bool> SaveDataAsync(string userId, MyData data);
    Task<MyData> GetDataAsync(string userId);
    Task DeleteDataAsync(string userId);
}

[InjectAsSingleton(typeof(IMyFeatureDao))]
internal sealed class MyFeatureDao : IMyFeatureDao
{
    private readonly IMyFeatureRedisDbExecuter redisExecutor;
    
    public MyFeatureDao(IMyFeatureRedisDbExecuter redisExecutor)
    {
        this.redisExecutor = redisExecutor;
    }
    
    public async Task<bool> SaveDataAsync(string userId, MyData data)
    {
        var key = $"myfeature:{userId}";
        return await redisExecutor.SetValueForKeyAsync(key, data, TimeSpan.FromHours(1));
    }
    
    public async Task<MyData> GetDataAsync(string userId)
    {
        var key = $"myfeature:{userId}";
        return await redisExecutor.GetValueByKeyAsync<MyData>(key);
    }
    
    public async Task DeleteDataAsync(string userId)
    {
        var key = $"myfeature:{userId}";
        await redisExecutor.DeleteKeyAsync(key);
    }
}
```

### Шаг 3: Использование в бизнес-логике

Используйте DAO в классах бизнес-слоя:

```csharp
public class MyBusinessService
{
    private readonly IMyFeatureDao myFeatureDao;
    
    public MyBusinessService(IMyFeatureDao myFeatureDao)
    {
        this.myFeatureDao = myFeatureDao;
    }
    
    public async Task ProcessUserDataAsync(string userId)
    {
        var data = await myFeatureDao.GetDataAsync(userId);
        // бизнес-логика
        await myFeatureDao.SaveDataAsync(userId, updatedData);
    }
}
```

**Примечание:** Исторически встречаются разные подходы к работе с Redis, но описанный выше является рекомендуемым.

## Основные операции

Базовый интерфейс `IRedisDbExecuter` (см. `Moedelo.InfrastructureV2.Domain/Interfaces/Redis/IRedisDbExecuter.cs`) предоставляет следующие группы операций:

- **String операции** — установка/получение строковых значений, инкремент/декремент
- **Key операции** — удаление, проверка существования, установка TTL, получение времени жизни
- **Sorted Set операции** — добавление/удаление элементов с score, получение по диапазону score/rank, подсчет количества
- **Hash операции** — работа с полями хэша (установка/получение/удаление одного или нескольких полей)
- **List операции** — добавление в начало/конец списка, получение элементов, удаление
- **Set операции** — добавление/удаление элементов множества, получение всех элементов
- **Distributed Lock** — распределенная блокировка для синхронизации между процессами
- **Pub/Sub** — публикация/подписка на каналы
- **Batch операции** — группировка нескольких команд в один round-trip (см. раздел ниже)

## Redis Batch Operations

Batch API позволяет группировать несколько команд и отправлять их за один round-trip к серверу, повышая производительность.

### Базовое использование

```csharp
await redisExecutor.CreateBatch("MyBatchOperation")
    .SortedSetRemoveRangeByScore(key, double.NegativeInfinity, now)  // cleanup без результата
    .SortedSetAdd(key, score, member, out var added)                  // с результатом
    .SortedSetLength(key, out var count)                              // с результатом
    .ExecuteAsync();

// Получаем результаты после выполнения
var wasAdded = added.Value;
var itemCount = count.Value;
```

### Extension методы для упрощения синтаксиса

Для операций, где результат обычно не требуется, доступны extension методы без `out` параметра:

```csharp
using Moedelo.InfrastructureV2.Domain.Extensions;

await redisExecutor.CreateBatch("Cleanup")
    .KeyDelete(oldKey1)                              // без out _ — чище!
    .KeyDelete(oldKey2)
    .SortedSetRemoveRangeByScore(key, min, max)
    .ExecuteAsync();
```

Подробнее см. `Moedelo.InfrastructureV2.Domain.Extensions.RedisBatchExtensions`.

### Когда использовать Batch

✅ **Используйте:**
- Несколько независимых операций в одном методе
- Cleanup операции (удаление устаревших данных)
- Групповые операции для снижения числа round-trips к Redis

❌ **Не используйте:**
- Для одной операции
- Когда следующая операция зависит от результата предыдущей (используйте условную логику между await)

**Важно:** Batch не обеспечивает атомарность и транзакционность — это просто группировка команд для оптимизации сетевых обращений.

## Особенности реализации

### Audit Integration

Все Redis операции автоматически логируются в AuditTrail с метаданными:
- Тип операции (команда Redis)
- Ключи
- Время выполнения
- Ошибки (если были)

Для batch операций указывайте осмысленное имя спана auditTrail, которое должно быть уникальным в рамках вашего приложения:
```csharp
redisExecutor.CreateBatch("MyBusinessService.TokenCleanup")  // будет в audit логах
```

### Key Prefixing

В некоторых тестовых окружениях настроено автоматическое добавление префикса к ключам, что позволяет независимо хранить данных нескольких окружений в одном экземпляре Redis.

### Distributed Lock

```csharp
var lockKey = "lock:operation:userId";
var queryObject = new RedisQueryObject 
{ 
    Expiry = TimeSpan.FromSeconds(30),
    RetryCount = 3,
    RetryDelay = TimeSpan.FromMilliseconds(100)
};

var executed = await redisExecutor.DistributedLockRunAsync(lockKey, async () =>
{
    // Критическая секция — выполнится только одним процессом
    await ProcessDataAsync();
}, queryObject);
```

## Примеры из практики

### Сохранение с ограничением количества элементов

```csharp
public async Task<bool> SaveUserTokenAsync(Guid tokenGuid, int userId, DateTime expiryTime, int maxTokens)
{
    var key = $"tokens:user:{userId}";
    var score = ToUnixTimestamp(expiryTime);
    var now = ToUnixTimestamp(DateTime.Now);
    
    await redisExecutor.CreateBatch("SaveUserToken")
        .SortedSetRemoveRangeByScore(key, double.NegativeInfinity, now)  // удаляем истекшие
        .SortedSetAdd(key, score, tokenGuid.ToString(), out var added)
        .SortedSetLength(key, out var count)
        .ExecuteAsync();
    
    // Ограничиваем количество токенов
    if (count.Value > maxTokens)
    {
        await redisExecutor.SortedSetRemoveRangeByRankAsync(key, 0, -(maxTokens + 1));
    }
    
    return added.Value;
}
```

### Массовое удаление связанных данных

```csharp
public Task DeleteUserDataAsync(int userId)
{
    return redisExecutor.CreateBatch("DeleteUserData")
        .KeyDelete($"tokens:user:{userId}")
        .KeyDelete($"sessions:user:{userId}")
        .KeyDelete($"cache:user:{userId}")
        .ExecuteAsync();
}
```

## Best Practices

1. **Используйте специализированные executors** вместо `IDefaultRedisDbExecuter` — лучше разделение ответственности
2. **Используйте Batch для множественных операций** — экономит round-trips к Redis
3. **Используйте extension методы** для cleanup операций — улучшает читаемость кода
4. **Всегда указывайте осмысленное audit span name** в `CreateBatch()` — помогает в отладке
5. **Не забывайте `ConfigureAwait(false)`** в библиотечном коде
6. **Устанавливайте TTL** для временных данных — избегайте утечек памяти в Redis

## Доступные Redis Executors

Модуль предоставляет готовые executors для работы с различными базами данных Redis. 

При создании нового executor добавьте номер БД в соответствующий enum:
- `RedisDbEnum` — для persistent Redis (критичные данные с периодическим сохранением на диск)
- `RedisCacheDbEnum` — для cache Redis (временные данные, потеря которых некритична)

Полный список интерфейсов см. в `Moedelo.InfrastructureV2.Domain/Interfaces/Redis/`

## Документация

- [Корпоративная Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)
- Примеры использования — см. unit тесты в `../../tests/`

## Поддержка

Architecture Team — см. [CODEOWNERS](../../CODEOWNERS)
