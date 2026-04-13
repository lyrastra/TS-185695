# md-common-executioncontext

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Библиотека общих абстракций и компонентов для управления execution context (контекстом выполнения) в корпоративных .NET-приложениях. Предоставляет универсальные интерфейсы, middleware, клиентские и вспомогательные классы для передачи, хранения и обработки контекста выполнения между сервисами и слоями приложения.

## Основные возможности
- Абстракции для работы с execution context (IExecutionInfoContextAccessor, IExecutionInfoContextInitializer и др.)
- Middleware для автоматического управления и передачи execution context
- Клиентские компоненты для интеграции с API и внешними сервисами
- Поддержка аутентификации и авторизации в execution context
- Вспомогательные компоненты для работы с неидентифицированным контекстом (Unidentified)

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-executioncontext.git
   ```
2. Добавьте нужные проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Common.ExecutionContext.Abstractions
   - Moedelo.Common.ExecutionContext
   - Moedelo.Common.ExecutionContext.Middleware (по необходимости)
   - Moedelo.Common.ExecutionContext.Client (по необходимости)
   - Moedelo.Common.ExecutionContext.Auth (по необходимости)
   - Moedelo.Common.ExecutionContext.Unidentified (по необходимости)
3. Используйте предоставленные абстракции и middleware для передачи и обработки execution context в приложении.

## Зависимости
- md-common-accessrules
- md-common-types
- md-common-jwt
- md-common-audit
- md-common-settings
- md-infrastructure-core-dependencyinjection
- md-infrastructure-core-http
- md-infrastructure-core-json
- Microsoft.AspNetCore.App (для middleware, auth)

## Совместимость и ограничения
- TargetFramework: netstandard2.0 (основные проекты), net8.0 (middleware, auth)
- Не содержит бизнес-логики — только абстракции и инфраструктурные компоненты
- Используется совместно с другими корпоративными модулями

## Состав и проекты
- Moedelo.Common.ExecutionContext.Abstractions (абстракции, TargetFramework: netstandard2.0)
- Moedelo.Common.ExecutionContext (реализация, TargetFramework: netstandard2.0)
- Moedelo.Common.ExecutionContext.Middleware (middleware, TargetFramework: net8.0)
- Moedelo.Common.ExecutionContext.Client (клиент, TargetFramework: netstandard2.0)
- Moedelo.Common.ExecutionContext.Auth (auth, TargetFramework: net8.0)
- Moedelo.Common.ExecutionContext.Unidentified (unidentified, TargetFramework: netstandard2.0)

## Ключевые абстракции и компоненты
- IExecutionInfoContextAccessor, IExecutionInfoContextInitializer (доступ и инициализация execution context)
- ExecutionInfoContextMiddleware, UnidentifiedExecutionInfoContextMiddleware (middleware для передачи контекста)
- IExecutionContextApiClient, IExecutionContextApiCaller (клиентские интерфейсы)
- IUnidentifiedScopeManager (работа с неидентифицированным контекстом)
- Поддержка интеграции с аутентификацией и авторизацией

## Архитектурное описание: что такое Execution Context

Execution Context — это инфраструктурный механизм для централизованной передачи и хранения информации о текущем контексте выполнения запроса или операции между слоями и сервисами распределённого приложения.

В типовом корпоративном .NET-приложении Execution Context включает:
- идентификаторы пользователя, организации, сессии;
- технические и бизнесовые метаданные (correlationId, traceId, tenantId, source, scope, права доступа);
- информацию для аудита, авторизации, трассировки;
- дополнительные параметры для сквозной передачи между микросервисами.

Execution Context обеспечивает:
- корректную аутентификацию, авторизацию, аудит, трассировку и multi-tenancy;
- прозрачную передачу контекста между слоями (API → сервисы → DAL → внешние вызовы);
- упрощение тестирования, внедрения middleware, интеграции с внешними API.

В модуле реализованы:
- абстракции для доступа и инициализации контекста (IExecutionInfoContextAccessor, IExecutionInfoContextInitializer);
- middleware для автоматической передачи и обновления контекста;
- клиентские компоненты для передачи контекста при вызове внешних сервисов;
- расширения для работы с анонимным или частично определённым контекстом (Unidentified, Auth).

Best practice: используйте только предоставленные абстракции и middleware для работы с execution context, не реализуйте собственные механизмы передачи или хранения контекста.

## Best practices
- Используйте только абстракции и middleware этого модуля для работы с execution context
- Не реализуйте собственные механизмы передачи контекста — используйте предоставленные компоненты
- Для DI используйте только абстракции (IExecutionInfoContextAccessor и др.)
- Следите за актуальностью зависимостей и настройкой инфраструктуры

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория

## Состав Execution Context

Execution Context включает в себя следующие основные поля:
- **UserId** — идентификатор пользователя
- **FirmId** — идентификатор организации (фирмы)
- **RoleId** — идентификатор роли
- **Scopes** — список областей доступа (scopes)
- **UserRules** — набор access rule для пользователя
- **Config** — строка конфигурации (по умолчанию "default")
- **CreateDate** — дата создания контекста
- **ExpirationDate** — дата истечения действия контекста
- **Token** — строка-токен (для неидентифицированного/внешнего контекста)
- **ValidUntil** — дата истечения действия токена

Этот состав определяет минимальный гарантированный набор данных, который доступен в execution context во всех сервисах, использующих данный модуль.

## Примеры использования

**Подключение middleware в Startup.cs:**
```csharp
// Startup.cs или Program.cs
app.UseExecutionInfoContext();
```

**Получение ExecutionInfoContext в сервисе:**
```csharp
public class MyService
{
    private readonly IExecutionInfoContextAccessor contextAccessor;

    public MyService(IExecutionInfoContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    public void DoSomething()
    {
        var context = contextAccessor.ExecutionInfoContext;
        // Доступ к UserId, FirmId, RoleId, Scopes, UserRules и др.
    }
}
```