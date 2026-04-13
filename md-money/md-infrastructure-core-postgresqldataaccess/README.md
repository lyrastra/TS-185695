# md-infrastructure-core-postgresqldataaccess

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Реализация инфраструктурных сервисов для работы с PostgreSQL в корпоративных .NET-приложениях. Предоставляет DI-расширения, настройки и конкретную реализацию IPostgreSqlExecutor для работы с PostgreSQL через Dapper и Npgsql. Используется совместно с md-common-postgresqldataaccess (абстракции и базовые классы).

## Основные возможности
- Реализация IPostgreSqlExecutor (PostgreSqlExecutor)
- DI-расширение для регистрации и настройки (ConfigurePostgreSqlDbExecutor)
- Класс опций MoedeloPostgresqlSqlExecutorOptions для настройки поведения
- Вспомогательные internal-обработчики типов (DateTime, IPAddress, MAC)
- Интеграция с Microsoft.Extensions.DependencyInjection

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructure-core-postgresqldataaccess.git
   ```
2. Добавьте проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions
   - Moedelo.Infrastructure.PostgreSqlDataAccess
3. Зарегистрируйте сервисы в DI:
   ```csharp
   using Moedelo.Infrastructure.PostgreSqlDataAccess;
   // ...
   services.ConfigurePostgreSqlDbExecutor(options =>
   {
       options.EnableLegacyTimestampBehavior = true;
       options.ReplaceUnspecifiedKindOfDateTimeByLocal = false;
   });
   ```
4. Используйте IPostgreSqlExecutor через DI (не надо так делать, используйте абстракции из md-common-postgresqldataaccess):
   ```csharp
   public class MyService
   {
       private readonly IPostgreSqlExecutor _executor;
       public MyService(IPostgreSqlExecutor executor)
       {
           _executor = executor;
       }
       // ...
   }
   ```

## Зависимости
- md-infrastructure-core-dependencyinjection
- Dapper, Npgsql (NuGet)

## Совместимость и ограничения
- TargetFramework: netstandard2.0
- Требует Microsoft.Extensions.DependencyInjection
- Не содержит бизнес-логики — только инфраструктурные сервисы

## Состав и проекты
- Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions (абстракции, TargetFramework: netstandard2.0)
- Moedelo.Infrastructure.PostgreSqlDataAccess (реализация, TargetFramework: netstandard2.0)

## Документация и примеры
- Примеры использования — см. исходный код
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)

## Best practices
- Для большинства сценариев предпочтительно использовать абстракции и базовые классы из md-common-postgresqldataaccess, не обращаясь к инфраструктурным деталям напрямую
- Используйте только через DI и абстракции (IPostgreSqlExecutor)
- Не храните строки подключения в коде — используйте IOptions и настройки
- Не используйте инфраструктурные классы напрямую вне DI
- Следите за актуальностью NuGet-зависимостей

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория
