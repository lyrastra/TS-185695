# md-common-accessrules

**Владелец/команда:** Команда Архитектуры

## Назначение
Инфраструктурная библиотека для централизованной проверки, кэширования и управления access rules (прав доступа) в корпоративных .NET-приложениях. Обеспечивает абстракции, реализацию и интеграцию с ASP.NET Core Authorization.

## Основные возможности
- Абстракции для проверки и кэширования access rules (ISettingRepository, IAccessRulesCache, IUserInFirmAccessRulesVerifier и др.)
- Реализация проверки, кэширования и инфраструктуры access rules
- Интеграция с audit, http, redis, settings, dependency injection
- Атрибуты, фильтры и требования для интеграции с ASP.NET Core Authorization
- Поддержка работы с ролями, тарифами, контекстом пользователя

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-accessrules.git
   ```
2. Добавьте нужные проекты из папки `src/` в ваш solution (.sln) через Visual Studio или вручную.
3. Используйте абстракции и реализацию в своём коде:
   ```csharp
   using Moedelo.Common.AccessRules;
   IUserInFirmAccessRulesVerifier verifier = ...;
   bool hasAccess = verifier.HasAccess(userId, rule);
   ```

## Зависимости
- md-common-types
- md-common-audit
- md-common-http
- md-common-redisdataaccess
- md-common-settings
- md-common-executioncontext
- md-infrastructure-core-dependencyinjection
- md-infrastructure-core-http
- md-infrastructure-core-json
- Требуется .NET Standard 2.0/2.1 или .NET 8.0 (для Authorization)

## Совместимость и ограничения
- Совместим с .NET Standard 2.0/2.1, .NET Core 2.0+, .NET 5/6/7/8+, .NET 8.0 (Authorization)
- Не содержит бизнес-логики, только инфраструктурные компоненты для работы с access rules
- Не рекомендуется использовать для хранения бизнес-логики

## Состав и проекты
- Moedelo.Common.AccessRules.Abstractions (Moedelo.Common.AccessRules.Abstractions.csproj): абстракции для проверки и кэширования access rules; TargetFramework: netstandard2.0
- Moedelo.Common.AccessRules (Moedelo.Common.AccessRules.csproj): реализация абстракций из Moedelo.Common.AccessRules.Abstractions для использвоания в net8+ приложениях; TargetFramework: netstandard2.1
- Moedelo.Common.AccessRules.Authorization (Moedelo.Common.AccessRules.Authorization.csproj): методы расширения, облегчающие проверку наличия тех или иных прав в коллекции прав, в том числе для использования в ASP NET Core приложениях, TargetFramework: net8.0

## Документация и примеры

### Ключевые enum и контракты
- **AccessRule** — основной enum со списком всех прав доступа. Определён в этом репозитории: `src/Moedelo.Common.AccessRules.Abstractions/AccessRule.cs`.
- Важно: существует дубликат этого enum в репозитории [md-enums](https://gitlab.mdtest.org/development/infra/md-enums/-/blob/master/src/common/Moedelo.Common.Enums/Enums/Access/AccessRule.cs?ref_type=heads). 
  - Версия из md-enums используется в .NET Framework 4.7.2 приложениях.
  - Версия из текущего репозитория используется в net8+ приложениях.

Для получения дополнительных сведений и уточнения деталей реализации обращайтесь к кодовой базе ("читай код!").

## Best practices
- Используйте абстракции для DI и тестирования
- Для интеграции с ASP.NET Core Authorization используйте предоставленные фильтры и атрибуты
- Не размещайте бизнес-логику в инфраструктурном модуле
- Следите за актуальностью зависимостей

## Декларативная проверка прав через атрибуты

Для удобства и унификации проверки access rules в ASP.NET Core-приложениях реализованы специальные атрибуты:

- `HasAllAccessRules` — требует наличие у пользователя всех указанных прав.
- `HasAnyAccessRules` — требует наличие хотя бы одного из указанных прав.

Атрибуты можно применять к контроллерам и отдельным методам:

```csharp
using Moedelo.Common.AccessRules.Abstractions;

[HasAllAccessRules(AccessRule.ViewFirmDetails, AccessRule.EditFirmDetails)]
public IActionResult ManageFirmDetails() { ... }

[HasAnyAccessRules(AccessRule.ViewSalary, AccessRule.EditSalary)]
public IActionResult ViewOrEditSalary() { ... }
```

Как это работает:
- Атрибуты используют фильтры, которые получают ExecutionInfoContext через DI.
- Проверка выполняется по коллекции `UserRules`.
- При отсутствии нужных прав возвращается 403 Forbidden.

**Рекомендуется использовать эти атрибуты для всех методов, где требуется контроль доступа по правам.**

## Поддержка и контакты
- Владелец/команда: Команда Архитектуры

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория.
