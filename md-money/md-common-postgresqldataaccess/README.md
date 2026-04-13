# md-common-postgresqldataaccess

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Библиотека общих абстракций и базовых компонентов для построения высокоуровневых клиентов PostgreSQL в корпоративных .NET-приложениях. Предоставляет универсальные базовые классы и интерфейсы для работы с PostgreSQL, не завися от конкретной инфраструктурной реализации.

## Основные возможности
- Абстрактный базовый класс для работы с PostgreSQL (`MoedeloPostgreSqlDbExecutorBase`) и контракт `IMoedeloPostgreSqlDbExecutorBase`
- Универсальные методы для выполнения SQL-запросов, работы с транзакциями, временными таблицами и параметрами
- Модели запросов с поддержкой именования audit span (см. раздел ниже) и расширения для построения audit span (`AuditSpanBuilderExtensions`)
- Интеграция с аудитом (`IAuditTracer`) и корпоративными настройками (`ISettingRepository`)
- Вспомогательные утилиты для типовых операционных сценариев (очистка данных, расширения для `IMoedeloPostgreSqlDbExecutorBase`)
- Не содержит прямой работы с ADO.NET — делегирует инфраструктурному слою

## Модели запросов

Библиотека предоставляет специализированные модели запросов (`PsqlQueryObject`, `PsqlQueryObjectWithDynamicParams`, `PsqlBulkCopyQueryObject`), которые являются обертками над базовыми инфраструктурными интерфейсами (`IQueryObject`, `IQueryObjectWithDynamicParams`, `IBulkCopyQueryObject`) с добавленной функциональностью настройки аудита.

**Зачем это нужно**: все модели позволяют задать фиксированное имя для audit span через интерфейс `IAuditTrailSpanNameSource`. Без этого имя формируется автоматически на основе информации о месте вызова, включая номер строки кода, что может приводить к изменению имени спана в системе audit trail при редактировании исходного кода доменного dao.

**Доступные модели**:
- `PsqlQueryObject` — обертка над `IQueryObject`
- `PsqlQueryObjectWithDynamicParams` — обертка над `IQueryObjectWithDynamicParams` с поддержкой динамических параметров
- `PsqlBulkCopyQueryObject` — обертка над `IBulkCopyQueryObject` для массового копирования данных

## Вспомогательные утилиты

Дополнительный проект `Moedelo.Common.PostgreSqlDataAccess.Utils` предоставляет:
- `MoedeloPostgreSqlDbExecutorBaseExtensions` — расширения для `IMoedeloPostgreSqlDbExecutorBase`, включая метод `CleanFirmDataAutomaticallyAsync` для автоматической зачистки данных удалённых фирм по всем таблицам `public` с колонкой `firm_id`.
- `Scripts/CleanUpFirmData.sql` — встроенный SQL-скрипт, который ищет релевантные таблицы, удаляет строки и возвращает статистику.
- `TableFirmDataCleanUpResult` — контракт результата очистки с информацией о таблице, количестве удалённых строк и ошибках.

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-postgresqldataaccess.git
   ```
2. Добавьте проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Common.PostgreSQLDataAccess.Abstractions
   - Moedelo.Common.PostgreSqlDataAccess.Utils (по необходимости)
3. Для работы требуется подключение и настройка инфраструктурных зависимостей (см. md-infrastructure-core-postgresqldataaccess, md-infrastructure-core-dependencyinjection).
4. Реализуйте собственный клиент PostgreSQL, унаследовавшись от MoedeloPostgreSqlDbExecutorBase:
   ```csharp
   [InjectAsSingleton(typeof(IMyPostgreSqlDbExecutor))]
   internal sealed class MyPostgreSqlDbExecutor : MoedeloPostgreSqlDbExecutorBase, IMyPostgreSqlDbExecutor
   {
       public MyPostgreSqlDbExecutor(
           IPostgreSqlDbExecutor sqlDbExecutor,
           ISettingRepository settingRepository,
           IAuditTracer auditTracer)
           : base(sqlDbExecutor,
               settingRepository.GetConnectionString(), // пример extension method для получения строки подключения
               auditTracer)
       {
       }
   }
   ```
5. Для автоматической зачистки данных подключите `Moedelo.Common.PostgreSqlDataAccess.Utils` и используйте `CleanFirmDataAutomaticallyAsync`, предоставив список идентификаторов фирм и `ISqlScriptReader`.

## Зависимости
- md-common-audit
- md-common-settings
- md-infrastructure-core-postgresqldataaccess (абстракции)

## Совместимость и ограничения
- TargetFramework: `netstandard2.0` (оба проекта). Совместим с .NET Standard 2.0+ / .NET 6+.
- Не содержит реализации низкоуровневых операций с PostgreSQL — только абстракции и базовые паттерны
- Не предназначен для прямого использования в инфраструктурных сервисах

## Состав и проекты
- `Moedelo.Common.PostgreSQLDataAccess.Abstractions` — базовые абстракции, модели запросов и расширения аудита (TargetFramework: netstandard2.0)
- `Moedelo.Common.PostgreSqlDataAccess.Utils` — расширения, модели результатов и встроенные SQL-скрипты (TargetFramework: netstandard2.0)

## Документация и примеры
- Примеры использования — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)

## Best practices
- Для большинства сценариев предпочтительно использовать только абстракции и базовые классы этого модуля, не обращаясь к инфраструктурным деталям напрямую
- Не реализуйте низкоуровневую работу с PostgreSQL — используйте инфраструктурные сервисы
- Для DI используйте только абстракции (IPostgreSqlDbExecutor и др.)
- Следите за актуальностью зависимостей и настройкой инфраструктуры

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория
