# md-infrastructure-core-aspnet

**Владелец/команда:** Команда Архитектуры

## Назначение
Инфраструктурные компоненты для ASP.NET Core-приложений: middleware для обработки ошибок и CORS, фоновые задачи (Hosted Services), расширения для DI-контейнера и ApplicationBuilder, атрибуты валидации моделей, интеграция со Swagger, абстракции для очередей фоновых задач и вспомогательные классы.

## Основные возможности
- Middleware для обработки ошибок и CORS
- Реализация фоновых задач (Hosted Services: периодические, очередные, повторяющиеся)
- Автоматическая регистрация Hosted Services через атрибут `[InjectAsHostedService]`
- Constraint'ы для маршрутизации enum'ов (EnumRouteConstraint<TEnum>)
- Автоматическая регистрация enum constraint'ов через RegisterEnumRouteConstraints
- Расширения для DI-контейнера, ApplicationBuilder, HttpContext
- Атрибуты валидации для моделей (email, телефон, ИНН, ОГРН, коллекции и др.)
- ActionResult и модели для унификации API-ответов
- Интеграция и расширения для Swagger (настройка, фильтры, опции)
- Абстракции для очередей фоновых задач
- Вспомогательные классы и расширения

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructure-core-aspnet.git
   ```
2. Добавьте необходимые проекты из папки `src/` в ваш solution (.sln) через Visual Studio или вручную.
3. Используйте нужные компоненты в своем проекте.

> Подробнее о структуре, best practices и интеграции — см. корпоративную документацию и примеры в исходном коде.

## Документация и примеры

### Автоматическая регистрация Hosted Services

#### Атрибут [InjectAsHostedService]

Атрибут `[InjectAsHostedService]` позволяет автоматически регистрировать hosted сервисы в DI-контейнере без явного вызова `services.AddHostedService<T>()`.

**Условия автоматической регистрации:**
1. Тип является неабстрактным классом
2. Тип реализует интерфейс `IHostedService`
3. Тип отмечен атрибутом `[InjectAsHostedService]`
4. Тип находится в указанной сборке

**Пример использования:**
```csharp
[InjectAsHostedService]
public class MyPeriodicHostedService : MoedeloPeriodicHostedService
{
    public MyPeriodicHostedService(IAuditTracer auditTracer, ILogger<MyPeriodicHostedService> logger) 
        : base(auditTracer, logger)
    {
    }

    protected override async Task DoExecuteTaskAsync(CancellationToken cancellationToken)
    {
        // Ваша бизнес-логика здесь
        await ProcessBusinessLogicAsync(cancellationToken);
    }
}
```

#### Метод AddHostedServicesFromAssembly

Для автоматической регистрации всех hosted сервисов из сборки используйте метод `AddHostedServicesFromAssembly`:

```csharp
// В Startup.cs или Program.cs
services.AddHostedServicesFromAssembly(typeof(Startup).Assembly);
```

**Преимущества автоматической регистрации:**
- Меньше boilerplate кода
- Автоматическое обнаружение новых hosted сервисов
- Снижение вероятности забыть зарегистрировать сервис
- Более чистый код в Startup.cs/Program.cs

### Маршрутизация с enum constraint'ами

#### Автоматическая регистрация enum constraint'ов

Метод `RegisterEnumRouteConstraints` автоматически регистрирует constraint'ы для всех enum'ов в указанных сборках:

```csharp
services.RegisterEnumRouteConstraints(typeof(ServiceType).Assembly);
```

#### Использование в контроллерах

```csharp
[HttpGet("service/{serviceType:ServiceType}/client/{clientId:int}")]
public IActionResult GetByServiceType(ServiceType serviceType, int clientId)
{
    // serviceType автоматически валидируется как значение enum ServiceType
}
```

**Поддерживаемые форматы значений:**
- Строковые значения: `Moedelo`, `PartnershipPortal`
- Числовые значения: `1`, `2`
- Регистр не учитывается для строковых значений

### Фоновая обработка задач через очередь

#### Регистрация QueuedHostedService

Для фонового выполнения некритичных задач используйте связку `AddQueuedHostedService` + `IBackgroundTaskQueue`:

```csharp
// В Startup.cs или Program.cs
services.AddQueuedHostedService();
```

`IBackgroundTaskQueue` автоматически регистрируется как Singleton через атрибут `[InjectAsSingleton]`.

#### Использование в контроллерах и сервисах

```csharp
public class MyController : ControllerBase
{
    private readonly IBackgroundTaskQueue backgroundTaskQueue;

    public MyController(IBackgroundTaskQueue backgroundTaskQueue)
    {
        this.backgroundTaskQueue = backgroundTaskQueue;
    }

    [HttpPost("process")]
    public IActionResult ProcessData([FromBody] ProcessDataRequest request)
    {
        // Добавляем задачу в очередь для фонового выполнения
        backgroundTaskQueue.QueueBackgroundWorkItem(async cancellationToken =>
        {
            await ProcessDataAsync(request.Data, cancellationToken);
        });

        return Ok(new { Message = "Задача добавлена в очередь" });
    }

    private async Task ProcessDataAsync(string data, CancellationToken cancellationToken)
    {
        // Длительная обработка данных
        await Task.Delay(5000, cancellationToken);
        // Логика обработки...
    }
}
```

**Особенности:**
- Задачи выполняются последовательно, по одной
- Не гарантирует выполнение всех задач (например, при остановке приложения)
- Подходит для некритичных задач, которые можно выполнить в фоне
- Автоматическое логирование ошибок выполнения задач

## Состав и проекты
- Moedelo.Infrastructure.AspNetCore.Abstractions (Moedelo.Infrastructure.AspNetCore.Abstractions.csproj): абстракции и интерфейсы для инфраструктурных компонентов ASP.NET Core, TargetFramework: netstandard2.0
- Moedelo.Infrastructure.AspNetCore (Moedelo.Infrastructure.AspNetCore.csproj): базовые middleware, hosted services, расширения и вспомогательные классы для ASP.NET Core, TargetFramework: net8.0
- Moedelo.Infrastructure.AspNetCore.Mvc (Moedelo.Infrastructure.AspNetCore.Mvc.csproj): расширения для ASP.NET Core MVC, ActionResult, модели и атрибуты, TargetFramework: net8.0
- Moedelo.Infrastructure.AspNetCore.Swagger (Moedelo.Infrastructure.AspNetCore.Swagger.csproj): интеграция и расширения для Swagger/OpenAPI, TargetFramework: net8.0
- Moedelo.Infrastructure.AspNetCore.Validation (Moedelo.Infrastructure.AspNetCore.Validation.csproj): атрибуты валидации для моделей, TargetFramework: netstandard2.0
- Moedelo.Infrastructure.AspNetCore.Xss (Moedelo.Infrastructure.AspNetCore.Xss.csproj): компоненты для защиты от XSS-уязвимостей, TargetFramework: netstandard2.0

## Зависимости
- md-infrastructure-core-dependencyinjection
- md-infrastructure-core-json
- Требуется .NET 8.0 для большинства проектов, отдельные библиотеки поддерживают netstandard2.0

## Совместимость и ограничения
- Совместим с ASP.NET Core 8.0+ (основные проекты)
- Некоторые проекты (Abstractions, Validation, Xss) поддерживают netstandard2.0
- Не содержит бизнес-логики, только инфраструктурные компоненты
- Не рекомендуется создавать жёсткие зависимости на бизнес-логику приложений

## Документация и примеры
Исходный код содержит подробные XML-комментарии и примеры использования.

## Best practices
- Не размещайте бизнес-логику в инфраструктурном модуле
- Используйте только необходимые компоненты
- Следите за актуальностью зависимостей

## Поддержка и контакты
- Владелец/команда: Команда Архитектуры

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория.