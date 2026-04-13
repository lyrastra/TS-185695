# md-common-consul

**Владелец/команда:** Команда Архитектуры

## Назначение
Инфраструктурные компоненты для .NET-приложений: регистрация и обнаружение сервисов в Consul Service Discovery, работа с KV-хранилищем Consul (чтение, запись, удаление ключей).

## Основные возможности
- Регистрация и обнаружение сервисов в Consul Service Discovery
- Работа с KV-хранилищем Consul (чтение/запись/удаление)
- Управление лидерством (leadership) для обеспечения эксклюзивного выполнения задач

## Быстрый старт
1. Подключить сабмодуль:
   ```
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-consul.git
   ```
2. Добавить нужные проекты из сабмодуля в ваш solution (.sln) через Visual Studio или вручную.
3. Добавить ProjectReference в нужные .csproj:
   ```xml
   <ProjectReference Include="..\..\md-common-consul\src\Moedelo.Common.Consul\Moedelo.Common.Consul.csproj" />
   ```
4. Настроить параметры подключения к Consul в конфигурации приложения (см. пример в src/ или документации к вашему DI-контейнеру).

## Зависимости
- md-infrastructure-core-consul
- md-infrastructure-core-dependencyinjection
- md-common-settings
- md-common-audit
- Требуется .NET 8.0 (см. TargetFramework в .csproj соответствующих проектов)

## Совместимость и ограничения
- Совместим с сервисами и инфраструктурой, поддерживающими Consul
- Не содержит бизнес-логики, только инфраструктурные компоненты
- Не рекомендуется создавать жёсткие зависимости на бизнес-логику приложений

## Состав и проекты
- Moedelo.Common.Consul.Abstractions (Moedelo.Common.Consul.Abstractions.csproj): абстракции и интерфейсы для работы с Consul (TargetFramework: net8.0)
- Moedelo.Common.Consul (Moedelo.Common.Consul.csproj): реализация Moedelo.Common.Consul.Abstractions для использования в net8+ приложениях (TargetFramework: net8.0)
- Moedelo.Common.Consul.ServiceDiscovery (Moedelo.Common.Consul.ServiceDiscovery.csproj): сервисы и расширения для service discovery через Consul (TargetFramework: net8.0)
- Moedelo.Common.Consul.AspNetCore (Moedelo.Common.Consul.AspNetCore.csproj): вспомогательные инструменты для использования в ASP.NET Core-приложениях (TargetFramework: net8.0)

## Управление лидерством (Leadership)

### Концепция
Система управления лидерством позволяет обеспечить эксклюзивное выполнение задач в распределенной среде. Только один экземпляр сервиса может быть лидером для конкретной задачи, что предотвращает дублирование операций и конфликты при параллельной обработке.

### Принцип работы
1. **Захват лидерства**: Сервис пытается захватить лидерство через Consul KV с использованием сессий
2. **Эксклюзивное выполнение**: Только лидер выполняет критическую задачу
3. **Автоматическое освобождение**: При завершении работы или потере соединения лидерство автоматически освобождается
4. **Перевыборы**: При потере лидера другие экземпляры могут захватить лидерство

### Пример использования

```csharp
public class RulingSystem : IRulingSystem, IAsyncDisposable
{
    private const string LeadershipName = "KafkaConsumersBalancing";
    private readonly IMoedeloServiceLeadershipService leadershipService;
    private readonly ILogger logger;
    private bool? isMaster = null;

    public RulingSystem(
        IMoedeloServiceLeadershipService leadershipService,
        ILogger<RulingSystem> logger)
    {
        this.leadershipService = leadershipService;
        this.logger = logger;
    }

    public async Task UpdateStatusAsync(CancellationToken cancellationToken)
    {
        var prevValue = isMaster;

        try
        {
            // Попытка захватить лидерство
            isMaster = await leadershipService.AcquireLeadershipAsync(LeadershipName, cancellationToken);

            if (prevValue != isMaster)
            {
                if (IsMaster)
                {
                    logger.LogInformation("Стал мастером. SessionId={SessionId}", 
                        leadershipService.ConsulSessionId);
                }
                else
                {
                    logger.LogInformation("Не мастер. SessionId={SessionId}", 
                        leadershipService.ConsulSessionId);
                }
            }

            // Выполнение эксклюзивной задачи только лидером
            if (IsMaster)
            {
                await ExecuteExclusiveTaskAsync(cancellationToken);
            }
        }
        catch
        {
            isMaster = null;
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        // Освобождение лидерства при завершении работы
        if (IsMaster)
        {
            await leadershipService
                .ReleaseLeadershipAsync(LeadershipName, CancellationToken.None)
                .AsTask()
                .WaitIgnoringExceptionAsync(TimeSpan.FromSeconds(1));
        }
    }

    public bool IsMaster => isMaster ?? false;
}
```

### Использование в Hosted Services

Для периодических задач рекомендуется использовать `MoedeloExclusivePeriodicHostedService` из md-common-aspnet.


### Структура ключей в Consul
Лидерство хранится в Consul KV по пути:
```
{Environment}/runtime/election/{Domain}/{AppName}/{LeadershipName}/leader
```

Где значение содержит информацию о лидере: `{MachineName}:{ProcessId}`

## Документация и примеры
- Подробные примеры использования и интеграции см. в исходном коде проектов и в тестах (если есть)

## Best practices (опционально)
- Не размещайте бизнес-логику в инфраструктурном модуле
- Используйте только необходимые компоненты
- Следите за актуальностью зависимостей

## Поддержка и контакты
- Владелец/команда: Команда Архитектуры

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория.

## Архитектурные различия: common vs infrastructure

> **Важно!**
>
> - **md-common-consul** — предоставляет только общие контракты, интерфейсы и модели для работы с Consul. Не содержит инфраструктурных реализаций, не зависит от платформенных деталей. Используется для унификации взаимодействия между сервисами и слоями приложения.
> - **md-infrastructure-core-consul** — содержит инфраструктурные реализации, сервисы, DI-обёртки и интеграцию с Consul API. Подключается только в инфраструктурных слоях, не используется напрямую в бизнес-логике.
>
> Соблюдение этого разделения позволяет поддерживать чистую архитектуру, минимизировать связность и облегчает сопровождение корпоративных приложений.
