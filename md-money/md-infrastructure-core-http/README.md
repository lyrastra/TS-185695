# md-infrastructure-core-http

## Владелец

Architecture Team ([CODEOWNERS](./CODEOWNERS))

## Назначение

Инфраструктурная библиотека для низкоуровневой работы с HTTP в корпоративных .NET-приложениях. Предоставляет сервисы и абстракции для выполнения HTTP-запросов, управления HttpClient, поддержки DI и расширения корпоративной инфраструктуры.

## Основные возможности
- Реализация IHttpRequestExecuter и IDisposableHttpRequestExecutor
- Поддержка всех HTTP-методов (GET, POST, PUT, PATCH, DELETE, file upload/download)
- Управление жизненным циклом HttpClient
- Интеграция с DI через атрибуты (InjectAsTransient)
- Обработка ошибок HTTP (статусы, таймауты, валидация)
- Не содержит бизнес-логики, только инфраструктурные детали

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-infrastructure-core-http.git
   ```
2. Добавьте проекты из папки `src/` в ваш solution (.sln):
   - Moedelo.Infrastructure.Http.Abstractions
   - Moedelo.Infrastructure.Http
3. Для использования сервисов внедрите зависимости через DI-инфраструктуру:
   ```csharp
   public class MyService
   {
       private readonly IHttpRequestExecuter httpRequestExecuter;
       public MyService(IHttpRequestExecuter httpRequestExecuter)
       {
           this.httpRequestExecuter = httpRequestExecuter;
       }

       public async Task<string> GetDataAsync(string url)
       {
           return await httpRequestExecuter.GetAsync(url);
       }
   }
   ```

## Зависимости
- md-infrastructure-core-dependencyinjection
- Требуется .NET Standard 2.1 (реализация), .NET Standard 2.0 (абстракции)

## Совместимость и ограничения
- Совместим с .NET Standard 2.1+ (реализация), .NET Standard 2.0+ (абстракции)
- Не содержит бизнес-логики, только инфраструктурные компоненты
- Не предназначен для прямого использования в бизнес-коде — используйте через абстракции common-модуля

## Состав и проекты
- Moedelo.Infrastructure.Http.Abstractions (абстракции, TargetFramework: netstandard2.0)
- Moedelo.Infrastructure.Http (реализация, TargetFramework: netstandard2.1)

## Документация и примеры
- Примеры использования — см. исходный код и тесты
- [Wiki: Архитектура и best practices](https://gitlab.mdtest.org/development/md-templates.wiki/-/wikis/arhitektura/http-i-api-klienty)

## Best practices
- Скорее всего, в ваших приложениях нет необходимости использовать классы из этого репозитория напрямую - используйте функционал, предоставляемый md-common-http
- Используйте только через абстракции (IHttpRequestExecuter, IDisposableHttpRequestExecutor)
- Не размещайте бизнес-логику в инфраструктурном модуле
- Следите за актуальностью зависимостей и настройкой DI
- Для построения API-клиентов используйте базовые классы из md-common-http

## Поддержка и контакты
- Владелец: Architecture Team

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория
