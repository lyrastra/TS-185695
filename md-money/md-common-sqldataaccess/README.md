# md-common-sqldataaccess

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Библиотека общих абстракций и базовых компонентов для построения высокоуровневых клиентов SQL-баз данных в корпоративных .NET-приложениях. Предоставляет универсальные базовые классы и интерфейсы для работы с SQL, не завися от конкретной инфраструктурной реализации.

## Основные возможности
- Абстрактный базовый класс для работы с SQL (`MoedeloSqlDbExecutorBase`) и контракт `IMoedeloSqlDbExecutorBase`
- Универсальные методы для выполнения SQL-запросов (`QueryAsync`, `HashSetQueryAsync`, bulk copy и др.)
- Модели запросов с поддержкой именования audit span (см. раздел ниже) и расширения для сборки спанов (`AuditSpanBuilderExtensions`)
- Расширения для работы с временными таблицами и `DataTable` (`TemporaryTableExtensions`, `DataTableExtensions`, `TemporaryTableDump`)
- Интеграция с аудитом (`IAuditTracer`) и корпоративными настройками (`ISettingRepository`)
- Не содержит прямой работы с ADO.NET — делегирует инфраструктурному слою

## Модели запросов

Библиотека предоставляет специализированные модели запросов (`MssqlQueryObject`, `MssqlQueryObjectWithDynamicParams`, `MssqlBulkCopyQueryObject`), которые являются обертками над базовыми инфраструктурными интерфейсами (`IQueryObject`, `IQueryObjectWithDynamicParams`, `IBulkCopyQueryObject`) с добавленной функциональностью настройки аудита.

**Зачем это нужно**: все модели позволяют задать фиксированное имя для audit span через интерфейс `IAuditTrailSpanNameSource`. Без этого имя формируется автоматически на основе информации о месте вызова, включая номер строки кода, что может приводить к изменению имени спана в системе audit trail при редактировании исходного кода доменного dao.

**Доступные модели**:
- `MssqlQueryObject` — обертка над `IQueryObject`
- `MssqlQueryObjectWithDynamicParams` — обертка над `IQueryObjectWithDynamicParams` с поддержкой динамических параметров
- `MssqlBulkCopyQueryObject` — обертка над `IBulkCopyQueryObject` для массового копирования данных

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-sqldataaccess.git
   ```
2. Добавьте проект `Moedelo.Common.SqlDataAccess.Abstractions` из папки `src/` в ваш solution (.sln).
3. Для работы требуется подключение и настройка инфраструктурных зависимостей (см. md-infrastructure-core-sqldataaccess, md-infrastructure-core-dependencyinjection).
4. Реализуйте собственный клиент SQL, унаследовавшись от MoedeloSqlDbExecutorBase:
   ```csharp
   [InjectAsSingleton(typeof(IMyDomainSqlDbExecutor))]
   internal sealed class MyDomainSqlDbExecutor : MoedeloSqlDbExecutorBase, IMyDomainSqlDbExecutor
   {
       public MyDomainSqlDbExecutor(
           ISqlDbExecutor sqlDbExecutor,
           ISettingRepository settingRepository,
           IAuditTracer auditTracer)
           : base(sqlDbExecutor,
               settingRepository.GetDomainConnectionString(), // пример extension method для получения строки подключения
               auditTracer)
       {
       }
   }
   ```

## Зависимости
- md-common-audit
- md-common-settings
- md-infrastructure-core-sqldataaccess (абстракции)

## Совместимость и ограничения
- TargetFramework: `net8.0`. Совместим с .NET 8+.
- Не содержит реализации низкоуровневых операций с SQL — только абстракции и базовые паттерны
- Не предназначен для прямого использования в инфраструктурных сервисах

## Состав и проекты
- `Moedelo.Common.SqlDataAccess.Abstractions` — базовые абстракции и расширения (TargetFramework: net8.0)
- Дополнительные утилиты не поставляются: расширения и модели входят в основную сборку

## Документация и примеры
- Примеры использования — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)

## Best practices
- Для большинства сценариев предпочтительно использовать только абстракции и базовые классы из этого модуля, не обращаясь к инфраструктурным деталям напрямую
- Не реализуйте низкоуровневую работу с SQL — используйте инфраструктурные сервисы
- Для DI используйте только абстракции (ISqlDbExecutor и др.)
- Следите за актуальностью зависимостей и настройкой инфраструктуры

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория
