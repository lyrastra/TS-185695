# md-infrastructure-core-redisdataaccess

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Инфраструктурная библиотека для низкоуровневой работы с Redis в корпоративных .NET-приложениях. Предоставляет сервисы и абстракции для выполнения операций с Redis, управления connection pool, поддержки DI и расширения корпоративной инфраструктуры.

## Основные возможности
- Реализация IRedisDbExecuter (RedisDbExecutor)
- Управление connection pool (ConnectionMultiplexerPool)
- CRUD-операции для строк, списков, словарей, множеств, pub/sub
- Поддержка распределённых блокировок (distributed lock)
- Интеграция с DI через атрибуты (InjectAsSingleton)
- Не содержит бизнес-логики, только инфраструктурные детали

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructure-core-redisdataaccess.git
   ```
2. Добавьте проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Infrastructure.Redis.Abstractions
   - Moedelo.Infrastructure.Redis
3. Для использования сервисов внедрите зависимости через DI-инфраструктуру (ВНИМАНИЕ: это только пример, скорее всего, вам не надо использовать абстракции из этого репозитория, используйте функционал, предоставляемый md-common-redisdataaccess):
   ```csharp
   public class MyService
   {
       private readonly IRedisDbExecuter redisDbExecuter;
       public MyService(IRedisDbExecuter redisDbExecuter)
       {
           this.redisDbExecuter = redisDbExecuter;
       }

       public async Task<bool> SetValueAsync(string key, string value)
       {
           // connection — объект IRedisConnection, получаемый из настроек
           return await redisDbExecuter.SetValueForKeyAsync(connection, key, value);
       }
   }
   ```

## Зависимости
- md-infrastructure-core-dependencyinjection
- Требуется .NET Standard 2.0+ (реализация и абстракции)

## Совместимость и ограничения
- Совместим с .NET Standard 2.0+, .NET Framework 4.7.2 (реализация)
- Не содержит бизнес-логики, только инфраструктурные компоненты
- Не предназначен для прямого использования в бизнес-коде — используйте через абстракции common-модуля

## Состав и проекты
- Moedelo.Infrastructure.Redis.Abstractions (абстракции, TargetFramework: netstandard2.0)
- Moedelo.Infrastructure.Redis (реализация, TargetFramework: netstandard2.0; net472)

## Документация и примеры
- Примеры использования — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)

## Best practices
- Скорее всего, в ваших приложениях нет необходимости использовать классы из этого репозитория напрямую — используйте функционал, предоставляемый md-common-redisdataaccess
- Используйте только через абстракции (IRedisDbExecuter, IConnectionMultiplexerPool)
- Не размещайте бизнес-логику в инфраструктурном модуле
- Следите за актуальностью зависимостей и настройкой DI
- Для построения клиентов Redis используйте базовые классы из md-common-redisdataaccess

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория