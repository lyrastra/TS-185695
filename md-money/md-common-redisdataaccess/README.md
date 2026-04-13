# md-common-redisdataaccess

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Библиотека общих абстракций и базовых компонентов для построения высокоуровневых клиентов Redis в корпоративных .NET-приложениях. Предоставляет универсальные базовые классы и интерфейсы для работы с Redis, не завися от конкретной инфраструктурной реализации.

## Основные возможности
- Абстрактный базовый класс для работы с Redis (MoedeloRedisDbExecutorBase)
- Контракт для реализации (IMoedeloRedisDbExecutorBase)
- CRUD-операции для строк, списков, словарей, множеств
- Поддержка сериализации/десериализации объектов
- Интеграция с аудитом (IAuditTracer) и корпоративными настройками (ISettingRepository)
- Поддержка распределённых блокировок (distributed lock)
- Не содержит прямой работы с Redis — делегирует инфраструктурному слою

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-redisdataaccess.git
   ```
2. Добавьте проект `Moedelo.Common.Redis.Abstractions` из папки `src/` в ваш solution (.sln).
3. Для работы требуется подключение и настройка инфраструктурных зависимостей (см. md-infrastructure-core-redisdataaccess, md-infrastructure-core-dependencyinjection).
4. Реализуйте собственный клиент Redis, унаследовавшись от MoedeloRedisDbExecutorBase:
   ```csharp
   [InjectAsSingleton(typeof(IMyRedisDbExecutor))]
   internal sealed class MyRedisDbExecutor : MoedeloRedisDbExecutorBase, IMyRedisDbExecutor
   {
       public MyRedisDbExecutor(
           IRedisDbExecuter redisDbExecutor,
           ISettingRepository settingRepository,
           IAuditTracer auditTracer)
           : base(redisDbExecutor,
               settingRepository,
               settingRepository.GetRedisCacheConnectionSetting(),
               settingRepository.GetAuthorizationRedisDbNumber(),
               auditTracer)
       {
       }
   }
   ```

## Зависимости
- md-common-audit
- md-common-settings
- md-infrastructure-core-json
- md-infrastructure-core-redisdataaccess (абстракции)

## Совместимость и ограничения
- Совместим с .NET Standard 2.0+
- Не содержит реализации низкоуровневых операций с Redis — только абстракции и базовые паттерны
- Не предназначен для прямого использования в инфраструктурных сервисах

## Состав и проекты
- Moedelo.Common.Redis.Abstractions (базовые абстракции, TargetFramework: netstandard2.0)

## Документация и примеры
- Примеры использования — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)

## Best practices
- Используйте только абстракции и базовые классы для построения собственных клиентов Redis
- Не реализуйте низкоуровневую работу с Redis — используйте инфраструктурные сервисы
- Для DI используйте только абстракции (IRedisDbExecuter и др.)
- Следите за актуальностью зависимостей и настройкой инфраструктуры
- Для большинства сценариев используйте только абстракции и базовые классы из этого модуля, не обращаясь к инфраструктурным деталям напрямую

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория
