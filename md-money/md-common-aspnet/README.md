# md-common-aspnet

**Владелец/команда:** Команда Архитектуры  
Вопросы: через корпоративный чат или issue tracker

## Состав и проекты
- **Moedelo.Common.AspNet.HostedServices**: Базовые классы для создания hosted сервисов с поддержкой аудита и лидерства. TargetFramework: net8.0
- **Moedelo.Common.AspNet.Mvc**: Расширения и опции для быстрой настройки MVC-приложений (метод `AddMoedeloMvc`). TargetFramework: net8.0

## Назначение
Инфраструктурный сабмодуль для ASP.NET Core приложений: включает базовые hosted сервисы с интеграцией аудита/лидерства и готовое расширение `AddMoedeloMvc`, которое настраивает контроллеры, CORS и аудит.

## Основные возможности
- **MoedeloPeriodicHostedService**: Базовый класс для периодических задач с автоматическим аудитом
- **MoedeloRepeatingHostedService**: Базовый класс для повторяющихся задач с автоматическим аудитом
- **MoedeloExclusivePeriodicHostedService**: Базовый класс для эксклюзивных периодических задач с распределенным лидерством через Consul
- Автоматическая интеграция с системой аудита (auditTrail)
- Валидация LeadershipLockId на соответствие ограничениям Consul
- Расширения для логирования и валидации
- **AddMoedeloMvc**: единая точка регистрации MVC — включает CORS, audit middleware, `TimeProvider`, Newtonsoft.Json и опции инжекции контроллеров

## Быстрый старт
### Hosted Services
1. Подключите сабмодуль:
   ```
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-aspnet.git
   ```
2. Добавьте проект `Moedelo.Common.AspNet.HostedServices` в ваш .sln-файл
3. Создайте hosted сервис, наследуясь от одного из базовых классов:

```csharp
public class MyPeriodicHostedService : MoedeloPeriodicHostedService
{
    public MyPeriodicHostedService(IAuditTracer auditTracer, ILogger<MyPeriodicHostedService> logger) 
        : base(auditTracer, logger)
    {
    }

    protected override async Task DoExecuteTaskAsync(CancellationToken cancellationToken)
    {
        // Ваша бизнес-логика здесь
        logger.LogInformation("Выполняется периодическая задача");
        await ProcessBusinessLogicAsync(cancellationToken);
    }
}
```

### MVC
1. Добавьте проект `Moedelo.Common.AspNet.Mvc` в решение
2. Зарегистрируйте расширение в `Program.cs`/`Startup.cs`:
   ```csharp
   services.AddMoedeloMvc(options =>
   {
       options.InjectControllersAsSingleton = false;  // по умолчанию true
       options.RespectBrowserAcceptHeader = true;      // null — оставить настройки MVC без изменений
       options.ConvertEnumToString = false;            // по умолчанию false, true — сериализовать enum как строки
   });
   ```
3. При необходимости подключайте дополнительные фильтры/конфигурацию через возвращаемый `IMvcBuilder`

## Сравнение типов hosted сервисов

| Тип сервиса | Назначение | Особенности планирования | Когда использовать                                                             |
|-------------|------------|-------------------------|--------------------------------------------------------------------------------|
| **MoedeloPeriodicHostedService** | Простые периодические задачи | Выполняет задачу → ждет интервал → повторяет | Для большинства случаев, когда не критично точное время                        |
| **MoedeloRepeatingHostedService** | Задачи с точным расписанием | Вычисляет время следующего запуска и ждет до него | Когда требуется точное соблюдение интервалов между началами запусков |
| **MoedeloExclusivePeriodicHostedService** | Эксклюзивные задачи с лидерством | Только один экземпляр (лидер) выполняет задачу | Для задач, которые должны выполняться только одним экземпляром                 |

## Типы hosted сервисов

### MoedeloPeriodicHostedService
Базовый класс для периодических задач с простым интервальным планированием.

**Особенности:**
- Выполняет задачу, затем ждет заданный интервал времени
- Простое и предсказуемое поведение
- Подходит для большинства случаев периодических задач

**Пример использования:**
```csharp
public class DataProcessingHostedService : MoedeloPeriodicHostedService
{
    public DataProcessingHostedService(IAuditTracer auditTracer, ILogger<DataProcessingHostedService> logger) 
        : base(auditTracer, logger)
    {
    }

    protected override TimeSpan Interval => TimeSpan.FromMinutes(5); // Каждые 5 минут

    protected override async Task DoExecuteTaskAsync(CancellationToken cancellationToken)
    {
        // Обработка данных
        logger.LogInformation("Обработка данных запущена");
        await ProcessDataAsync(cancellationToken);
    }
}
```

**Когда использовать:**
- Для простых периодических задач без строгих требований к времени выполнения
- Когда не критично точное соблюдение расписания
- Когда важно сколько именно пройдёт времени между окончание предыдущего шага и началом следующего
- Для задач с переменным временем выполнения

### MoedeloRepeatingHostedService
Базовый класс для повторяющихся задач с более точным контролем времени выполнения.

**Ключевые отличия от MoedeloPeriodicHostedService:**

1. **Точное планирование времени**: `RepeatingHostedService` вычисляет точное время следующего запуска и ждет до этого момента, даже если задача выполнилась быстрее интервала.

2. **Обработка перекрытия интервалов**: Если задача выполняется дольше заданного интервала, поведение регулируется настройкой `IntervalOverlapHandlingRule`:
   - `WaitForNextInterval` (по умолчанию): ждать до начала следующего интервала
   - `RunImmediately`: запустить следующий шаг незамедлительно

3. **Предсказуемое расписание**: Гарантирует, что задачи будут **стартовать** через равные промежутки времени, независимо от того, как много времени занимает то или иное выполнение

**Пример использования:**
```csharp
public class ScheduledDataSyncHostedService : MoedeloRepeatingHostedService
{
    public ScheduledDataSyncHostedService(IAuditTracer auditTracer, ILogger<ScheduledDataSyncHostedService> logger) 
        : base(auditTracer, logger)
    {
    }

    protected override TimeSpan Interval => TimeSpan.FromMinutes(30); // Каждые 30 минут

    protected override async Task DoExecuteTaskAsync(CancellationToken cancellationToken)
    {
        // Синхронизация данных в точно заданное время
        logger.LogInformation("Синхронизация данных запущена в {time}", DateTime.Now);
        await SyncDataAsync(cancellationToken);
    }
}
```

**Когда использовать:**
- Когда требуется точное соблюдение расписания выполнения
- Для задач, которые должны **стартовать** через равные промежутки времени (например, каждые 15 минут)
- Когда важно избежать накопления задержек в расписании

### MoedeloExclusivePeriodicHostedService
Базовый класс для эксклюзивных периодических задач с распределенным лидерством. Только один экземпляр сервиса (лидер) выполняет задачу.

```csharp
public class ExclusiveDataHostedService : MoedeloExclusivePeriodicHostedService
{
    public ExclusiveDataHostedService(
        IMoedeloServiceLeadershipService leadershipService,
        IAuditTracer auditTracer, 
        ILogger<ExclusiveDataHostedService> logger) 
        : base(leadershipService, auditTracer, logger)
    {
    }

    protected override string LeadershipLockId => "exclusive-data-processing";

    protected override async Task ExecuteTaskExclusivelyAsync(CancellationToken cancellationToken)
    {
        // Выполняется только лидером
        logger.LogInformation("Эксклюзивная обработка данных");
        await ProcessExclusiveDataAsync(cancellationToken);
    }
}
```

## Валидация LeadershipLockId

Класс `MoedeloExclusivePeriodicHostedService` включает валидацию свойства `LeadershipLockId` на соответствие ограничениям Consul.

### Ограничения Consul на имена ключей:
- **Не может быть пустой строкой** или содержать только пробелы
- **Не может содержать символы**: `< > : " | ? * \ /`
- **Не может начинаться или заканчиваться** точкой или пробелом
- **Не может содержать управляющие символы** (ASCII 0-31)
- **Максимальная длина**: 512 символов
- **Рекомендуемые символы**: буквы, цифры, дефисы, подчеркивания и точки

### Примеры валидных идентификаторов:
```csharp
protected override string LeadershipLockId => "valid-lock-id";
protected override string LeadershipLockId => "valid_lock_id";
protected override string LeadershipLockId => "valid.lock.id";
protected override string LeadershipLockId => "ValidLockId";
protected override string LeadershipLockId => "valid123";
```

### Примеры невалидных идентификаторов:
```csharp
// ❌ Пустая строка
protected override string LeadershipLockId => "";

// ❌ Недопустимые символы
protected override string LeadershipLockId => "in<valid";
protected override string LeadershipLockId => "in:valid";

// ❌ Начинается с точки или пробела
protected override string LeadershipLockId => ".invalid";
protected override string LeadershipLockId => " invalid";
```

## Настройка MVC через AddMoedeloMvc

`AddMoedeloMvc` — расширение `IServiceCollection`, которое используется как единая точка инициализации MVC:

- подключает CORS (`services.AddCors()`);
- регистрирует `TimeProvider.System`;
- добавляет Moedelo Audit Trail middleware;
- настраивает контроллеры через `AddControllers`, включая Newtonsoft.Json с `DefaultContractResolver`;
- по умолчанию регистрирует контроллеры как Singleton (можно отключить);
- позволяет переопределить `RespectBrowserAcceptHeader`;
- опция `ConvertEnumToString` для сериализации enum как строк в JSON.

```csharp
services.AddMoedeloMvc(options =>
{
    options.InjectControllersAsSingleton = true;    // значение по умолчанию
    options.RespectBrowserAcceptHeader = false;     // null — оставить настройки MVC без изменений
    options.ConvertEnumToString = false;            // значение по умолчанию, true — сериализовать enum как строки
});
```

Метод возвращает `IMvcBuilder`, поэтому поверх базовой конфигурации можно добавлять фильтры, форматтеры, Swagger и прочие расширения.

## Зависимости
- md-common-audit
- md-common-consul
- md-infrastructure-core-aspnet
- Microsoft.Extensions.Logging
- Microsoft.Extensions.Hosting

## Совместимость и ограничения
- TargetFramework: net8.0
- Совместим с ASP.NET Core 8.0+
- Использует Consul KV для реализации эксклюзивности (управление лидерством)
- Автоматическая интеграция с системой аудита

## Регистрация в DI контейнере

### Ручная регистрация

```csharp
// В Startup.cs или Program.cs
services.AddHostedService<MyPeriodicHostedService>();
services.AddHostedService<ExclusiveDataHostedService>();
```

### Автоматическая регистрация (рекомендуется)

Используйте атрибут `[InjectAsHostedService]` из `md-infrastructure-core-aspnet` для автоматической регистрации:

```csharp
[InjectAsHostedService]
public class MyPeriodicHostedService : MoedeloPeriodicHostedService
{
    // Реализация...
}

[InjectAsHostedService]
public class ExclusiveDataHostedService : MoedeloExclusivePeriodicHostedService
{
    // Реализация...
}
```

Затем зарегистрируйте все hosted сервисы из сборки:

```csharp
// В Startup.cs или Program.cs
services.AddHostedServicesFromAssembly(typeof(Startup).Assembly);
```

**Преимущества автоматической регистрации:**
- Меньше boilerplate кода
- Автоматическое обнаружение новых hosted сервисов
- Снижение вероятности забыть зарегистрировать сервис

## Документация и примеры
- Hosted services: [тесты](tests/Moedelo.Common.AspNet.HostedServices.Tests/)
- MVC: см. `src/Moedelo.Common.AspNet.Mvc/Extensions/ServiceCollectionExtensions.cs` и `MoedeloMvcOptions`

## Best practices
- Используйте `MoedeloExclusivePeriodicHostedService` для задач, которые должны выполняться только одним экземпляром
- Следуйте ограничениям Consul при выборе `LeadershipLockId`
- Базовые классы автоматически обрабатывают ошибки: логируют их и записывают в auditTrail. Обрабатывайте исключения в `DoExecuteTaskAsync` только если это является частью бизнес-логики
- Используйте `CancellationToken` для корректного завершения задач
- Переопределяйте `Interval` для настройки частоты выполнения

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория
