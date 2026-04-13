# md-infrastructure-core-apachekafka

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Реализация инфраструктурных сервисов для работы с Apache Kafka в корпоративных .NET-приложениях. Предоставляет DI-расширения, настройки и конкретные реализации для работы с Kafka через Confluent.Kafka. Используется совместно с md-common-apachekafka (абстракции и паттерны).

## Основные возможности
- Реализация продюсеров и консьюмеров Kafka
- DI-расширения для регистрации и настройки
- Классы для управления пулами продюсеров, балансировкой, error tolerance
- Интеграция с Microsoft.Extensions.DependencyInjection

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructure-core-apachekafka.git
   ```
2. Добавьте проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Infrastructure.Kafka.Abstractions
   - Moedelo.Infrastructure.Kafka
3. Зависимости внедряются автоматически для классов, отмеченных соответствующими атрибутами (см. корпоративные правила DI). DI не является темой этого репозитория.

## Зависимости
- md-infrastructure-core-dependencyinjection
- md-infrastructure-core-json
- Confluent.Kafka (NuGet)

## Совместимость и ограничения
- TargetFramework: netstandard2.1
- Требует Microsoft.Extensions.DependencyInjection
- Не содержит бизнес-логики — только инфраструктурные сервисы
- Используется только совместно с md-common-apachekafka

## Состав и проекты
- Moedelo.Infrastructure.Kafka.Abstractions (абстракции, TargetFramework: netstandard2.1)
- Moedelo.Infrastructure.Kafka (реализация, TargetFramework: netstandard2.1)
- Moedelo.Infrastructure.Kafka.ErrorTolerance (доп. сервисы)

## Документация и примеры
- Примеры использования — см. исходный код
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)

## Best practices
- Для большинства сценариев предпочтительно использовать абстракции и базовые классы из md-common-apachekafka, не обращаясь к инфраструктурным деталям напрямую
- Используйте только через DI и абстракции
- Не храните строки подключения в коде — используйте IOptions и настройки
- Не используйте инфраструктурные классы напрямую вне DI
- Следите за актуальностью NuGet-зависимостей

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория