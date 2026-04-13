# md-infrastructure-core-sqldataaccess

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Инфраструктурная библиотека для низкоуровневой работы с SQL-базами данных в корпоративных .NET-приложениях. Предоставляет сервисы и абстракции для выполнения SQL-запросов, управления скриптами, поддержки DI и расширения корпоративной инфраструктуры.

## Основные возможности
- Реализация ISqlDbExecutor (SqlDbExecutor)
- Чтение SQL-скриптов из embedded ресурсов (SqlScriptReader)
- Поддержка работы с временными таблицами, параметрами, GridReader
- BulkCopy операции для массовой вставки данных
- Поддержка выходных параметров (IOutParameterReader)
- Автоматическое исправление DateTimeKind.Unspecified на DateTimeKind.Local для корректной сериализации DateTime
- Интеграция с DI через атрибуты (InjectAsSingleton)
- Не содержит бизнес-логики, только инфраструктурные детали

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructure-core-sqldataaccess.git
   ```
2. Добавьте проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Infrastructure.SqlDataAccess.Abstractions
   - Moedelo.Infrastructure.SqlDataAccess
3. Для работы с SQL Server используйте контракты из md-common-sqldataaccess

## Зависимости
- md-infrastructure-core-dependencyinjection
- Требуется .NET 8.0+ (реализация и абстракции)

## Совместимость и ограничения
- Совместим с .NET 8.0+
- Не содержит бизнес-логики, только инфраструктурные компоненты
- Не предназначен для прямого использования в бизнес-коде — используйте через абстракции common-модуля

## Основные компоненты

### ISqlDbExecutor
Основной интерфейс для выполнения SQL-запросов:
- `QueryAsync<T>()` - выполнение запросов с возвратом коллекции
- `FirstOrDefaultAsync<T>()` - получение одного объекта
- `ExecuteAsync()` - выполнение команд без возврата данных
- `BulkCopyAsync()` - массовая вставка данных

### SqlScriptReader
Чтение SQL-скриптов из embedded ресурсов с поддержкой параметризации.

### Вспомогательные компоненты
- `GridReader` - работа с множественными результирующими наборами
- `IOutParameterReader` - чтение выходных параметров
- `TemporaryTable` - поддержка временных таблиц

## Состав и проекты
- Moedelo.Infrastructure.SqlDataAccess.Abstractions (абстракции, TargetFramework: net8.0)
- Moedelo.Infrastructure.SqlDataAccess (реализация, TargetFramework: net8.0)

## Документация и примеры
- Примеры использования — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)

## Best practices
- Скорее всего, в ваших приложениях нет необходимости использовать классы из этого репозитория напрямую — используйте функционал, предоставляемый md-common-sqldataaccess
- Используйте только через абстракции (ISqlDbExecutor и др.)
- Не размещайте бизнес-логику в инфраструктурном модуле
- Следите за актуальностью зависимостей и настройкой DI
- Для построения клиентов SQL используйте базовые классы из md-common-sqldataaccess
- При внесении изменений в код необходимо запускать unit-тесты для проверки корректности работы и предотвращения регрессий

### Настройка DateTimeKind для корректной сериализации
При получении `DateTime` из SQL Server через Dapper, значения имеют `DateTimeKind.Unspecified`, что может приводить к проблемам при сериализации в JSON. Рекомендуется включить автоматическое исправление:

```csharp
// В Startup.cs или Program.cs
using Moedelo.Infrastructure.SqlDataAccess.Extensions;

services.ConfigureMsSqlDbExecutor(options =>
{
    options.ReplaceUnspecifiedKindOfDateTimeByLocal = true;
});
```

Настройка применяется глобально для всех запросов через ISqlDbExecutor.

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория