# md-infrastructure-core-consul

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Библиотека предоставляет абстракции и реализации для интеграции корпоративных .NET-приложений с Consul Service Discovery и KV-хранилищем. Позволяет регистрировать сервисы, отслеживать изменения в KV, реализовывать health-check, а также работать с Consul API кросс-платформенно (поддержка .NET Standard 2.0, .NET 8, .NET Framework 4.7.2).

## Основные возможности

- Абстракции для работы с Consul Agent API, Catalog API, KV API
- Регистрация и отмена регистрации сервисов в Consul
- TTL health-check и отправка keep-alive
- Слежение за изменениями ключей и каталогов (IConsulCatalogWatcher)
- Получение и изменение значений KV через HTTP API
- Кросс-платформенные реализации (netstandard2.0, net8.0, net472)
- DI-совместимость, интеграция с корпоративным DI-контейнером

## Быстрый старт

1. Добавьте зависимость на нужный проект (см. "Состав и проекты").
2. Зарегистрируйте реализации через DI-контейнер.
3. Используйте абстракции для регистрации сервисов, работы с KV и слежения за изменениями.

**Пример ProjectReference для инфраструктурного сабмодуля:**
```
git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructure-core-consul.git
```

## Зависимости

- md-infrastructure-core-dependencyinjection
- md-infrastructure-core-json

## Совместимость и ограничения

- .NET Standard 2.0 (базовые абстракции и реализации)
- .NET 8 (реализация для современных сервисов)
- .NET Framework 4.7.2 (реализация для легаси-приложений)
- Для корректной работы требуется Consul >= 1.9

## Состав и проекты

- **Moedelo.Infrastructure.Consul.Abstraction** — контракты и модели (netstandard2.0)
  - IConsulAgentApiClient, IConsulCatalogApiClient, IConsulHttpApiClient, IConsulCatalogWatcher, AgentServiceRegistration и др.
- **Moedelo.Infrastructure.Consul** — основная реализация (netstandard2.0)
  - ConsulCatalogWatcherBase, ConsulAgentApiClientProxy, ConsulCatalogApiClientProxy и др.
- **Moedelo.Infrastructure.Consul.Net5** — реализация для .NET 8 (net8.0)
- **Moedelo.Infrastructure.Consul.NetFramework** — реализация для .NET Framework 4.7.2
- **Тесты:** Moedelo.Infrastructure.Consul.Tests - unit-тесты (net8.0, NUnit, Moq, FluentAssertions)

## Документация и примеры

- Встроенные XML-комментарии к публичным интерфейсам и классам
- [Consul Agent API](https://developer.hashicorp.com/consul/api-docs/agent)
- [Consul Catalog API](https://developer.hashicorp.com/consul/api-docs/catalog)
- Примеры использования: см. тесты и XML-комментарии



### Ключевые абстракции
- `IConsulAgentApiClient` — регистрация/отмена регистрации сервисов, TTL health-check
- `IConsulCatalogApiClient` — получение списка сервисов
- `IConsulHttpApiClient` — работа с KV через HTTP
- `IConsulCatalogWatcher` — слежение за изменениями ключей и каталогов

## Best practices

- Используйте только через DI, не создавайте реализации напрямую
- Для health-check используйте TTL и регулярные keep-alive
- Для кросс-платформенных приложений выбирайте нужную реализацию (Net5/Net8/NetFramework)
- Для production-окружений настраивайте Consul с учётом отказоустойчивости

## Поддержка и обратная связь

- Владелец: Architecture Team
- Вопросы и предложения — через внутренний трекер или команду архитектуры

## Changelog

- История изменений ведётся в git-репозитории

### Последние изменения

- **Добавлена реализация ConsulSessionalKeyValueApiClientProxy** - HTTP API для работы с KV через сессии без nuget-зависимостей

---
