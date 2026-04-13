# Moedelo.InfrastructureV2.Injection.LightInject

## Назначение

Реализация системы Dependency Injection на базе библиотеки [LightInject](https://github.com/seesharper/LightInject) для корпоративных .NET-приложений (.NET Framework 4.7.2).

Модуль предоставляет автоматическую регистрацию зависимостей через атрибуты и базовые классы для настройки DI контейнера.

## Основные компоненты

### DIInstaller

Базовый абстрактный класс для настройки DI контейнера. Предоставляет:
- Два контейнера: `statefulContainer` (для сервисов с состоянием) и `statelessContainer` (для stateless сервисов)
- Методы регистрации: `RegisterSingleton`, `RegisterTransient`, `RegisterPerWebRequest`
- Автоматическую регистрацию через атрибуты: `RegisterByDIAttribute`
- Управление scope: `BeginScope()`

### BaseAutoDiInstaller

Абстрактный класс, автоматически регистрирующий все зависимости из библиотек `Moedelo.*` по атрибутам. Загружает сборки из указанного пути и регистрирует классы, помеченные атрибутами `[InjectAsSingleton]`, `[InjectAsTransient]`, `[InjectAsPerWebRequest]`.

### AppAutoDiInstaller

Реализация `BaseAutoDiInstaller` для не-Web приложений (консоли, сервисы). Используется в приложениях, где нет веб-контекста.

## Использование

### Автоматическая регистрация через атрибуты

```csharp
[InjectAsSingleton(typeof(IMyService))]
public class MyService : IMyService
{
    // ...
}

[InjectAsSingleton(typeof(IBaseService), typeof(IDerivedService))]
public class DerivedService : IDerivedService
{
    // ...
}
```

### Инициализация в приложении

```csharp
var installer = new AppAutoDiInstaller(logger);
installer.Initialize();

// Резолвинг зависимостей
var service = installer.GetInstance<IMyService>();
```

## Известные проблемы

См. [KNOWN_ISSUES.md](./KNOWN_ISSUES.md) для информации о проблемах и ограничениях.

## Документация

- [LightInject GitHub](https://github.com/seesharper/LightInject)
- [Корпоративная Wiki: Архитектура](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura)
