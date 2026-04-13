# md-infrastructure-core-dependencyinjection

Инфраструктурная библиотека для автоматизированной и декларативной регистрации зависимостей в .NET-приложениях через атрибуты.

**Владелец:** Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Возможности

- **Декларативная регистрация** через атрибуты `[InjectAsSingleton]`, `[InjectAsTransient]`, `[InjectPerScope]`
- **Автоматическое определение** интерфейсов для регистрации
- **Фабричные методы** `Func<T>` для создания множественных экземпляров
- **Множественные реализации** через атрибут `[MultipleImplementationsPossible]`
- **Warmup-проверка** DI-конфигурации на старте приложения

## Быстрый старт

### 1. Подключение

Добавьте ссылки на проекты из папки `src/` в ваш solution:
- `Moedelo.Infrastructure.DependencyInjection.Abstractions`
- `Moedelo.Infrastructure.DependencyInjection`
- `Moedelo.Infrastructure.DependencyInjection.Warmup` (опционально)

### 2. Регистрация в Startup

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Регистрация всех сервисов с DI-атрибутами из сборок Moedelo.*
    services.RegisterByDIAttribute("Moedelo.*");
    
    // Опционально: проверка корректности DI-конфигурации на старте
    services.AddWarmup();
}
```

### 3. Использование атрибутов

```csharp
// Singleton - один экземпляр на приложение
[InjectAsSingleton(typeof(IConfigurationService))]
public class ConfigurationService : IConfigurationService 
{
    // ...
}

// Transient - новый экземпляр при каждом запросе
[InjectAsTransient(typeof(IEmailSender))]
public class EmailSender : IEmailSender 
{
    // ...
}

// Scoped - один экземпляр на HTTP-запрос (или scope)
[InjectPerScope(typeof(IOrderProcessor))]
public class OrderProcessor : IOrderProcessor 
{
    // ...
}
```

## Использование

### Автоматическое определение интерфейсов

Если не указывать тип интерфейса, библиотека автоматически зарегистрирует все интерфейсы из namespace `Moedelo.*`:

```csharp
// Автоматически зарегистрирует IUserService, IUserValidator
[InjectAsSingleton]
public class UserService : IUserService, IUserValidator
{
    // ...
}
```

### Множественные реализации

Для регистрации нескольких реализаций одного интерфейса пометьте интерфейс атрибутом:

```csharp
[MultipleImplementationsPossible]
public interface INotificationProvider { }

[InjectAsSingleton(typeof(INotificationProvider))]
public class EmailNotificationProvider : INotificationProvider { }

[InjectAsSingleton(typeof(INotificationProvider))]
public class SmsNotificationProvider : INotificationProvider { }

// Использование
public class NotificationService
{
    public NotificationService(IEnumerable<INotificationProvider> providers)
    {
        // Получите все зарегистрированные реализации
    }
}
```

### Регистрация фабрик (Advanced)

Для создания множественных независимых экземпляров (например, Kafka consumers, HTTP-клиенты) используйте параметр `registerFactory`:

```csharp
// Регистрация с фабрикой
[InjectAsTransient(typeof(IKafkaEventReader), registerFactory: true)]
public class KafkaEventReader : IKafkaEventReader 
{
    // ...
}

// Использование
public class EventProcessorService
{
    private readonly Func<IKafkaEventReader> readerFactory;
    
    public EventProcessorService(Func<IKafkaEventReader> readerFactory)
    {
        this.readerFactory = readerFactory;
    }
    
    public void StartProcessing()
    {
        // Создаем независимые экземпляры для параллельной обработки
        var reader1 = readerFactory();
        var reader2 = readerFactory();
        
        // Каждый reader будет работать независимо
    }
}
```

**Когда использовать фабрики:**
- ✅ Для Transient/Scoped сервисов, когда нужно создавать экземпляры по требованию
- ✅ Kafka consumers, WebSocket connections, временные HTTP-клиенты
- ❌ Не используйте для Singleton (нет смысла - всегда один экземпляр)

## Справочная информация

### Состав проектов

| Проект | Описание | Target Framework |
|--------|----------|------------------|
| `Moedelo.Infrastructure.DependencyInjection.Abstractions` | Атрибуты и абстракции | netstandard2.0 |
| `Moedelo.Infrastructure.DependencyInjection` | Реализация и расширения | net8.0 |
| `Moedelo.Infrastructure.DependencyInjection.Warmup` | HostedService для warmup | net8.0 |

### Зависимости

- Не зависит от других сабмодулей Moedelo
- Требует Microsoft.Extensions.DependencyInjection (транзитивно из ASP.NET Core)

### Совместимость

- .NET Standard 2.0+ (Abstractions)
- .NET 5/6/7/8+ (Implementation и Warmup)
- ASP.NET Core 5.0+

## Best Practices

### Выбор Lifetime

| Lifetime | Когда использовать | Примеры |
|----------|-------------------|---------|
| **Singleton** | Stateless сервисы, кэши, конфигурация | ConfigurationService, CacheManager |
| **Transient** | Легковесные stateless операции | Validators, Mappers, Builders |
| **Scoped** | Операции в рамках запроса, работа с БД | DbContext, UnitOfWork, RequestContext |

### Рекомендации

- ✅ Применяйте атрибуты к инфраструктурным и бизнес-сервисам
- ✅ Используйте `RegisterByDIAttribute` один раз в Startup
- ✅ Добавляйте `AddWarmup()` для проверки конфигурации на старте
- ✅ Используйте автоматическое определение интерфейсов где возможно
- ⚠️ Используйте `registerFactory: true` только для Transient/Scoped
- ⚠️ Не регистрируйте фабрики для Singleton - внедряйте сервис напрямую
- ❌ Не используйте атрибуты для регистрации внешних библиотек (используйте обычную регистрацию в Startup)

### Частые ошибки

**Проблема:** Сервис не находится в runtime
```csharp
// ❌ Интерфейс не из namespace Moedelo.*
[InjectAsSingleton]
public class MyService : IExternalLibraryInterface { }
```
```csharp
// ✅ Решение: укажите интерфейс явно
[InjectAsSingleton(typeof(IExternalLibraryInterface))]
public class MyService : IExternalLibraryInterface { }
```

**Проблема:** Циклические зависимости
- Используйте `Func<T>` или `Lazy<T>` для разрыва цикла
- Пересмотрите архитектуру - циклические зависимости часто указывают на проблему в дизайне

## Дополнительная документация

- Примеры использования - см. тесты в `tests/Moedelo.Infrastructure.DependencyInjection.Tests/DiTests.cs`
- Исходный код - см. `src/`

## Установка как сабмодуль

Если библиотека поставляется как git-сабмодуль:

```bash
git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructure-core-dependencyinjection.git
git submodule update --init --recursive
```

## Changelog

Основные изменения фиксируются в истории коммитов репозитория.

---

**Вопросы и поддержка:** Architecture Team
