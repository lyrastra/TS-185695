# md-common-http

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Библиотека общих абстракций и базовых компонентов для построения HTTP API-клиентов в корпоративных .NET-приложениях. Предоставляет универсальные базовые классы, generic-реализации и вспомогательные типы для работы с HTTP, не завися от конкретной инфраструктуры.

## Основные возможности
- Абстрактные и generic-базовые классы для API-клиентов (BaseApiClientInternal, BaseApiClient, BaseLegacyApiClient)
- Поддержка всех HTTP-методов (GET, POST, PUT, PATCH, DELETE, file upload/download)
- Интеграция с аудитом, логированием, корпоративными настройками
- Расширяемые заголовки и параметры
- Generic-реализации для .NET Core и .NET Framework
- Не содержит прямой работы с HttpClient — делегирует инфраструктуре

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-http.git
   ```
2. Добавьте нужные проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Common.Http.Abstractions
   - Moedelo.Common.Http.Generic.Abstractions
   - Moedelo.Common.Http.Generic.NetCore / Moedelo.Common.Http.Generic.NetFramework (по целевой платформе)
3. Для работы требуется подключение и настройка инфраструктурных зависимостей (см. md-infrastructure-core-http, md-infrastructure-core-dependencyinjection).
4. Реализуйте собственный API-клиент, унаследовавшись от одного из базовых классов:
   ```csharp
   public class MyApiClient : BaseApiClientInternal
   {
       public MyApiClient(
           IHttpRequestExecuter httpRequestExecutor,
           IUriCreator uriCreator,
           IAuditTracer auditTracer,
           IDefaultHeadersGetter[] defaultHeadersGetterCollection,
           SettingValue endpointSetting,
           ILogger logger)
           : base(httpRequestExecutor, uriCreator, auditTracer, defaultHeadersGetterCollection, endpointSetting, logger) { }

       public Task<MyResponse> GetDataAsync() => GetAsync<MyResponse>("/data", null, null, null, CancellationToken.None);
   }
   ```

## Зависимости
- md-common-settings
- md-common-audit
- md-common-executioncontext
- md-infrastructure-core-dependencyinjection
- md-infrastructure-core-http (абстракции)
- md-infrastructure-core-json

## Совместимость и ограничения
- Не содержит реализации низкоуровневых HTTP-запросов — только абстракции и базовые паттерны
- Не предназначен для прямого использования в инфраструктурных сервисах

## Состав и проекты
- Moedelo.Common.Http.Abstractions - базовые абстракции для использования в net8, TargetFramework: netstandard2.1
- Moedelo.Common.Http.Generic.Abstractions - общие абстракции для использования как в net8, так и в net472, TargetFramework: netstandard2.0
- Moedelo.Common.Http.Generic.NetCore - реализации общих абстракций для net8, TargetFramework: net8.0
- Moedelo.Common.Http.Generic.NetFramework - реализации общих абстракций для net472 (.NET Framework 4.7.2), TargetFramework: net472

## Документация и примеры
- Примеры использования — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/http-i-api-klienty)

## Best practices
- Используйте только абстракции и базовые классы для построения собственных API-клиентов
- Не реализуйте низкоуровневую работу с HTTP — используйте инфраструктурные сервисы
- Для DI используйте только абстракции (IHttpRequestExecuter, IUriCreator и др.)
- Следите за актуальностью зависимостей и настройкой инфраструктуры

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория 

## Исторический контекст и область применения базовых классов

В корпоративной экосистеме Moedelo исторически сложились две группы веб-приложений:

- **.NET Framework 4.7.2 (net472)** — старейшие приложения, размещённые на собственной инфраструктуре (md-enums, md-commonv2, md-infrastructurev2, так называемая "v2-инфраструктура"). Новые приложения на этой платформе не создаются, но их по-прежнему несколько десятков.
- **.NET 8 (net8)** — современные приложения, активно развиваемые и создаваемые на базе шаблона md-templates и инфраструктурных сабмодулей, входящих в этот шаблон. Количество таких приложений постоянно растёт.

**BaseApiClient** — базовый класс для построения API-клиентов, предназначенных для взаимодействия между приложениями группы net8.

**BaseLegacyApiClient** — базовый класс для построения API-клиентов, предназначенных для обращения из приложений группы net8 к приложениям группы net472.

> Обратите внимание: оба этих класса предназначены только для использования в net8-приложениях. Для net472-приложений используются собственные абстракции и базовые классы, размещённые в md-commonv2, которые не связаны по коду с md-common-http. 

## Ограничения и важные замечания
- Retry-политика (повторные попытки HTTP-запросов) не реализована на уровне базовых классов и инфраструктуры. Если требуется retry, его реализация должна обсуждаться и согласовываться с архитекторами.
- Все приватные API-клиенты должны регистрироваться как singleton через DI (см. best practices).
- Для новых клиентов всегда используйте унифицированный подход (см. описание ниже), если требуется поддержка как net8, так и net472 — это минимизирует дублирование кода и упрощает поддержку.
- Модуль не предназначен для прямого использования в инфраструктурных сервисах — только для построения API-клиентов.

## Унифицированный подход к реализации клиентов
Унифицированный подход позволяет реализовать бизнес-логику приватного клиента один раз в абстрактном классе, а платформенные детали (net8, net472) изолировать в зависимостях, внедряемых через DI и фабрики. Это минимизирует дублирование кода, упрощает поддержку и обеспечивает совместимость между платформами. Примеры реализации и подробности — см. [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/http-i-api-klienty).

## FAQ и типовые ошибки
См. раздел [FAQ и типовые ошибки](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/http-i-api-klienty#faq-и-типовые-ошибки) в wiki. 