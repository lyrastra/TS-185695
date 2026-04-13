# md-infrastructureV2

#### Владелец репозитория: Команда Архитектуры

## Назначение

Монорепозиторий инфраструктурных библиотек для корпоративных .NET-приложений (.NET Framework 4.7.2). Предоставляет готовые решения для работы с Redis, базами данных, Kafka, audit, dependency injection и других инфраструктурных задач.

## Структура репозитория

### Audit и логирование

- **Moedelo.InfrastructureV2.Audit** — система трассировки и audit trail
- **Moedelo.InfrastructureV2.AuditMvc** — интеграция audit с ASP.NET MVC
- **Moedelo.InfrastructureV2.AuditWebApi** — интеграция audit с Web API
- **Moedelo.InfrastructureV2.AuditOwin** — интеграция audit с OWIN
- **Moedelo.InfrastructureV2.Logging** — абстракции логирования

### Data Access

- **Moedelo.InfrastructureV2.DataAccess** — работа с SQL Server (Dapper)
- **Moedelo.InfrastructureV2.MySqlDataAccess** — работа с MySQL
- **Moedelo.InfrastructureV2.PostgreSqlDataAccess** — работа с PostgreSQL
- **Moedelo.InfrastructureV2.MongoDataAccess** — работа с MongoDB
- **Moedelo.InfrastructureV2.NHibernate** — интеграция NHibernate
- **Moedelo.InfrastructureV2.RedisDataAccess** — работа с Redis ([README](./src/Moedelo.InfrastructureV2.RedisDataAccess/README.md))

### Dependency Injection

- **Moedelo.InfrastructureV2.Injection** — базовые абстракции DI
- **Moedelo.InfrastructureV2.Injection.LightInject** — реализация на LightInject
- **Moedelo.InfrastructureV2.Injection.LightInject.Mvc** — интеграция с MVC
- **Moedelo.InfrastructureV2.Injection.LightInject.Web** — интеграция с Web Forms
- **Moedelo.InfrastructureV2.Injection.LightInject.WebApi** — интеграция с Web API
- **Moedelo.InfrastructureV2.Injection.Web** — общие абстракции для web

### Messaging и события

- **Moedelo.InfrastructureV2.ApacheKafka** — работа с Kafka
- **Moedelo.InfrastructureV2.EventBus** — внутренняя шина событий

### Общие модули

- **Moedelo.InfrastructureV2.Domain** — интерфейсы, модели, extension методы
- **Moedelo.InfrastructureV2.ApiClient** — базовые классы для HTTP клиентов
- **Moedelo.InfrastructureV2.Json** — работа с JSON (Newtonsoft.Json)
- **Moedelo.InfrastructureV2.Setting** — получение настроек из Consul
- **Moedelo.InfrastructureV2.Consul** — интеграция с Consul
- **Moedelo.InfrastructureV2.DataMapping** — AutoMapper integration
- **Moedelo.InfrastructureV2.System.Extensions** — расширения BCL типов

### Web

- **Moedelo.InfrastructureV2.Mvc** — утилиты для MVC
- **Moedelo.InfrastructureV2.WebApi** — утилиты для Web API
- **Moedelo.InfrastructureV2.WebApi.Validation** — валидация в Web API

## Подключение к проекту

1. Добавьте сабмодуль в корень вашего репозитория:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructurev2.git
   ```

2. Добавьте нужные проекты из `md-infrastructurev2/src/` в ваш solution (.sln)

3. Настройка DI происходит автоматически через атрибуты `[InjectAsSingleton]`, `[InjectAsTransient]`

4. Настройте подключение к Consul для получения настроек

## Зависимости

- Многие модули зависят от `md-infrastructure-core-*` (низкоуровневые библиотеки)
- Требуется настроенный Consul для получения конфигурации
- .NET Framework 4.7.2+

## Best Practices

1. **Используйте сабмодули как read-only** — изменения вносите в исходные репозитории
2. **Подключайте только нужные проекты** — не добавляйте весь репозиторий в solution
3. **Следите за версиями сабмодулей** — обновляйте регулярно через `git submodule update`
4. **Читайте README конкретных модулей** — там детальная документация

## Документация

- [Корпоративная Wiki: Архитектура](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura)
- [Корпоративная Wiki: Submodules](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)
- Документация конкретных модулей — см. README в соответствующих директориях

## Поддержка

Architecture Team — см. [CODEOWNERS](./CODEOWNERS)
