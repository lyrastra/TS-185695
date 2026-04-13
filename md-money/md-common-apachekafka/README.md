# md-common-apachekafka

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Библиотека общих абстракций и базовых компонентов для построения высокоуровневых клиентов Apache Kafka в корпоративных .NET-приложениях. Предоставляет универсальные интерфейсы, базовые классы и паттерны для работы с Kafka, не завися от конкретной инфраструктурной реализации.

## Основные возможности
- Абстракции для работы с Kafka (IKafkaConsumerBalancer, IKafkaTopicNameResolver и др.)
- Контракты для команд, событий, саг, мониторинга, error tolerance
- Универсальные паттерны для построения продюсеров и консьюмеров
- Не содержит реализаций — только контракты и базовые классы

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-apachekafka.git
   ```
2. Добавьте нужные проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Common.Kafka.Abstractions
   - Moedelo.Common.Kafka.Commands (по необходимости)
   - Moedelo.Common.Kafka.Saga.Abstractions и др.
3. Реализуйте собственные клиенты, наследуясь от предоставленных абстракций и интерфейсов.

## Зависимости
- md-common-audit
- md-common-executioncontext
- md-common-settings
- md-infrastructure-core-apachekafka (абстракции)
- md-infrastructure-core-json

## Совместимость и ограничения
- TargetFramework: netstandard2.0
- Не содержит инфраструктурных реализаций — только абстракции
- Используется совместно с md-infrastructure-core-apachekafka

## Состав и проекты
- Moedelo.Common.Kafka.Abstractions (абстракции, TargetFramework: netstandard2.0)
- Moedelo.Common.Kafka.Commands, Moedelo.Common.Kafka.Saga.Abstractions, Moedelo.Common.Kafka.Monitoring и др.

## Документация и примеры
- Примеры использования — см. исходный код
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/submodules)

## Best practices
- Для большинства сценариев предпочтительно использовать только абстракции и базовые классы этого модуля, не обращаясь к инфраструктурным деталям напрямую
- Не реализуйте низкоуровневую работу с Kafka — используйте инфраструктурные сервисы
- Для DI используйте только абстракции (IKafkaConsumerBalancer и др.)
- Следите за актуальностью зависимостей и настройкой инфраструктуры

## Архитектурные паттерны: Command и Event

Модуль чётко разделяет паттерны работы с Kafka на две категории:
- **Command** — асинхронные команды для управления состоянием доменных сущностей. Для них предусмотрены отдельные абстракции, интерфейсы и билдеры:
  - IEntityCommandData
  - IMoedeloEntityCommandKafkaTopicWriter
  - IMoedeloEntityCommandKafkaTopicReader
  - IMoedeloEntityCommandKafkaTopicReaderBuilder
  - MoedeloEntityCommandKafkaTopicReaderBuilder (базовый класс)
- **Event** — события, отражающие произошедшие изменения в домене. Для них реализованы собственные контракты и паттерны:
  - IEntityEventData
  - IMoedeloEntityEventKafkaTopicWriter
  - IMoedeloEntityEventKafkaTopicReader
  - IMoedeloEntityEventKafkaTopicReaderBuilder
  - MoedeloEntityEventKafkaTopicReaderBuilder (базовый класс)

Рекомендуется строго разделять обработку команд и событий, использовать соответствующие абстракции и не смешивать логику в одном обработчике.

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория
