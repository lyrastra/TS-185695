# md-common-logging

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Инфраструктурная библиотека для централизованного логирования, расширяемого сбора диагностических данных и интеграции с корпоративными сервисами аудита, контекста выполнения и HTTP. Предоставляет расширения для Microsoft.Extensions.Logging, поддержку ExtraLog (дополнительные поля), интеграцию с ExecutionContext, Audit и HttpContext.

## Основные возможности
- Базовые расширения для Microsoft.Extensions.Logging
- Поддержка ExtraLog: расширяемые поля для логирования (Audit, ExecutionContext, HttpContext, ExtraData)
- Интеграция с корпоративным ExecutionContext и Audit
- Расширения для логирования в web-приложениях (HttpContext)
- Гибкая настройка через конфигурацию и DI

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-logging.git
   ```
2. Добавьте нужные проекты из папки `src/` в ваш solution (.sln).
3. Зарегистрируйте необходимые расширения и провайдеры ExtraLog в Startup.cs/Program.cs:
   ```csharp
   // Пример для ASP.NET Core
   builder.Services.AddLogging(logging =>
   {
       logging.AddConsole();
       logging.AddDebug();
       // Добавьте нужные ExtraLog-провайдеры
       logging.AddExtraLogExecutionContext();
       logging.AddExtraLogAudit();
       logging.AddExtraLogHttpContext();
       logging.AddExtraLogExtraData();
   });
   ```
4. Используйте ILogger<T> для логирования с дополнительными полями:
   ```csharp
   // Корректный пример с шаблонами сообщений
   logger.LogInformation("User {UserId} performed {Action}", 123, "Login");
   // Или с вложенным объектом (если поддерживается вашей системой логирования)
   logger.LogInformation("User action {@Extra}", new { UserId = 123, Action = "Login" });
   ```

## Зависимости
- md-common-settings
- md-infrastructure-core-json
- md-infrastructure-core-dependencyinjection
- md-common-audit (через ExtraLog.Audit)
- md-common-executioncontext (через ExtraLog.ExecutionContext)
- Требуется .NET 8.0 (ядро, HttpContext), .NET Standard 2.0 (остальные)

## Совместимость и ограничения
- Совместим с .NET 8.0+, .NET Standard 2.0+
- Не содержит бизнес-логики, только инфраструктурные компоненты для логирования
- Не рекомендуется использовать для хранения бизнес-логики

## Состав и проекты
- Moedelo.Common.Logging (ядро, TargetFramework: net8.0)
- Moedelo.Common.Logging.ExtraLog.Abstractions (абстракции для ExtraLog, TargetFramework: netstandard2.0)
- Moedelo.Common.Logging.ExtraLog.Audit (интеграция с аудитом, TargetFramework: netstandard2.0)
- Moedelo.Common.Logging.ExtraLog.ExecutionContext (интеграция с ExecutionContext, TargetFramework: netstandard2.0)
- Moedelo.Common.Logging.ExtraLog.HttpContext (интеграция с HttpContext, TargetFramework: net8.0)
- Moedelo.Common.Logging.ExtraLog.ExtraData (дополнительные поля, TargetFramework: netstandard2.0)

## Документация и примеры
- Примеры использования и интеграции — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/logirovanie-i-middleware)

## Best practices
- Используйте ExtraLog-провайдеры для расширения диагностических данных
- Для интеграции с корпоративным аудитом и ExecutionContext подключайте соответствующие ExtraLog-проекты
- Не размещайте бизнес-логику в инфраструктурном модуле логирования
- Следите за актуальностью зависимостей и конфигурации

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория