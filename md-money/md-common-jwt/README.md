# md-common-jwt

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Инфраструктурная библиотека для работы с JWT (JSON Web Token) в .NET-приложениях. Предоставляет абстракции и сервисы для создания, валидации, подписи и разбора JWT-токенов, а также интеграцию с корпоративными настройками и DI.

## Основные возможности
- Абстракции для работы с JWT (IJwtService)
- Генерация, подпись и валидация JWT-токенов
- Поддержка сертификатов и ключей для подписи
- Интеграция с корпоративными настройками (md-common-settings)
- Автоматическая регистрация сервисов через DI-инфраструктуру

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-jwt.git
   ```
2. Добавьте проекты из папки `src/` в ваш solution (.sln).
3. Сервисы регистрируются автоматически через атрибуты `[InjectAsSingleton]` при использовании md-infrastructure-core-dependencyinjection. Дополнительная ручная регистрация не требуется.
4. Используйте IJwtService для создания и валидации токенов:
   ```csharp
   public class AuthService
   {
       private readonly IJwtService jwtService;
       public AuthService(IJwtService jwtService) { this.jwtService = jwtService; }

       public string GenerateToken(UserInfo user)
       {
           var claims = new[] { ... };
           return jwtService.Encode(claims);
       }

       public T DecodeToken<T>(string token) where T : class
       {
           return jwtService.Decode<T>(token);
       }
   }
   ```

## Зависимости
- md-common-settings
- md-infrastructure-core-dependencyinjection
- Требуется .NET Standard 2.0

## Совместимость и ограничения
- Совместим с .NET Standard 2.0+, .NET Core 2.0+, .NET 5/6/7/8+
- Не содержит бизнес-логики, только инфраструктурные компоненты для работы с JWT
- Не рекомендуется использовать для хранения бизнес-логики

## Состав и проекты
- Moedelo.Common.Jwt.Abstractions (абстракции для работы с JWT, TargetFramework: netstandard2.0)
- Moedelo.Common.Jwt (реализация сервисов, TargetFramework: netstandard2.0)

## Документация и примеры
- Примеры использования — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/logirovanie-i-middleware)

## Best practices
- Используйте только абстракции (IJwtService) для внедрения зависимостей и тестирования
- Храните ключи и сертификаты в защищённых настройках (см. md-common-settings)
- Не размещайте бизнес-логику в инфраструктурном модуле JWT
- Следите за актуальностью зависимостей и конфигурации

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория