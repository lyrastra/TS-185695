# md-common-audit

**Владелец/команда:** Команда Архитектуры  
Вопросы: через корпоративный чат или issue tracker

## Состав и проекты
- **Moedelo.Common.Audit**: Базовые компоненты и сервисы для аудита. TargetFramework: netstandard2.0
- **Moedelo.Common.Audit.Middleware**: Middleware для интеграции аудита с ASP.NET Core 8+. TargetFramework: net8.0
- **Moedelo.Common.Audit.EmptyMock**: Заглушки для тестирования и подмены аудита. TargetFramework: netstandard2.0
- **Moedelo.Common.Audit.Abstractions**: Абстракции и интерфейсы для расширения и внедрения аудита. TargetFramework: netstandard2.0

## Назначение
Инфраструктурный сабмодуль для централизованного аудита действий и событий в корпоративных приложениях. Предоставляет компоненты для логирования аудита, интеграции с middleware и поддержки сквозной трассировки.

## Основные возможности
- Реализация инфраструктуры аудита для .NET-приложений
- Компоненты для записи и хранения событий аудита
- Middleware для автоматического аудита HTTP-запросов
- Абстракции и интерфейсы для расширения
- Примеры интеграции с корпоративными системами логирования

## Быстрый старт
1. Подключите сабмодуль:
   ```
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-audit.git
   ```
2. Добавьте проекты из src/Moedelo.Common.Audit и/или src/Moedelo.Common.Audit.Middleware в ваш .sln-файл.
3. Интегрируйте аудит в приложение (Startup.cs):
   ```csharp
   app.UseAuditApiHandlerTrace(null, null);
   ```

## Зависимости
- md-common-apachekafka
- md-common-redisdataaccess
- md-infrastructure-core-dependencyinjection
- md-infrastructure-core-json
- Требуется .NET 6.0 или выше

## Совместимость и ограничения
- Совместим с ASP.NET Core и .NET 6+
- Не содержит бизнес-логики, только инфраструктурные компоненты
- Не рекомендуется модифицировать код напрямую — используйте расширения и абстракции

## Документация и примеры
- [Использование AuditTrail в HTTP](docs/AUDIT-TRAIL-HTTP.md) - документация по трейсингу HTTP запросов
- [Конфигурация AuditTrail Middleware](docs/MIDDLEWARE-CONFIGURATION.md) - настройка middleware для ASP.NET Core
- [Рекомендации для внешних клиентов API](docs/EXTERNAL-CLIENTS-GUIDE.md) - использование заголовка MD-AuditTrail-Context
- [Примеры использования в тестах](tests/Moedelo.Common.Audit.Tests/)
- [Архитектурная документация по сабмодулям](https://gitlab.mdtest.org/development/md-templates.wiki/arhitektura/submodules)

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория