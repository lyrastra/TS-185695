# md-accounts-shared

**Владелец/команда:** Команда Архитектуры
Канал для вопросов/обсуждений: канал команды Архитектуры в корпоративном мессенджере

## Назначение
Shared-сабмодуль для бизнес-домена "Account" (репозитории [md-account](https://gitlab.mdtest.org/development/md-account) и [md-accountNetCore](https://gitlab.mdtest.org/development/md-accountNetCore)). Содержит общие контракты, API-клиенты, интеграционные компоненты и абстракции для взаимодействия с сервисами и событиями домена Account.

## Основные возможности
- API-клиенты для сервисов домена Accounts (User, Firm, Address, ProductPartition и др.)
- Контракты и DTO для взаимодействия между сервисами
- Абстракции и реализации для интеграции с Kafka и RabbitMQ (продюсеры/консьюмеры)
- Перечисления и типы, используемые в домене

## Быстрый старт
1. Подключить сабмодуль:
   ```
   git submodule add https://gitlab.mdtest.org/development/md-accounts-shared.git
   ```
2. Добавить нужные проекты из сабмодуля в ваш solution (.sln) через Visual Studio или вручную.
3. Используйте абстрактные типы:

   ```csharp
   [InjectAsSingleton(typeof(IMyService))]
   public class MyService : IMyService
   {
       private readonly IUserClient userClient;

       public MyService(IUserClient userClient)
       {
           this.userClient = userClient;
       }

       public async ValueTask DoSomethingAsync(CancellationToken cancellationToken)
       {
           // Асинхронное использование userClient с поддержкой отмены
           var user = await userClient.GetUserByIdAsync(id: 123, cancellationToken);
           // ...
       }
   }
   ```

   > Удостоверьтесь, что проект веб-приложения ссылается на проекты с реализацией абстрактных типов. Иначе во время работы приложения DI-контейнер не сможет найти реализацию для используемой абстракции.

## Зависимости
- md-common-apachekafka
- md-infrastructure-core-dependencyinjection
- md-common-accessrules
- md-common-http
- Требуется поддержка .NET Standard/.NET Core (см. TargetFramework в .csproj соответствующих проектов)

## Совместимость и ограничения
- Совместим с сервисами и инфраструктурой Moedelo Accounts
- Не содержит бизнес-логики, только контракты и интеграционные компоненты
- Не рекомендуется создавать жёсткие зависимости между shared-модулями разных доменов

## Состав и проекты
- Moedelo.Accounts.Kafka.Abstractions (Moedelo.Accounts.Kafka.Abstractions.csproj): Абстракции для взаимодействия с доменом Account через Apache Kafka (TargetFramework: netstandard2.0)
- Moedelo.Accounts.Kafka.NetCore.Abstractions (Moedelo.Accounts.Kafka.NetCore.Abstractions.csproj): Абстракции для интеграции с Kafka специфичные для использования в net8+ приложениях (TargetFramework: netstandard2.0)
- Moedelo.Accounts.RabbitMq.Abstractions (Moedelo.Accounts.RabbitMq.Abstractions.csproj): Абстракции для взаимодействия с доменом Account через RabbitMQ (TargetFramework: netstandard2.0)
- Moedelo.Accounts.ApiClient.Abstractions (Moedelo.Accounts.ApiClient.Abstractions.csproj): Контракты, интерфейсы, DTO и валидация для взаимодействия с доменом Account через Rest API  (User, Firm, Address и др.) (TargetFrameworks: net472, netstandard2.0)
- Moedelo.Accounts.ApiClient.Enums (Moedelo.Accounts.ApiClient.Enums.csproj): Общедоступные перечисления домена Account (TargetFramework: netstandard2.0)
- Moedelo.Accounts.ApiClient (Moedelo.Accounts.ApiClient.csproj): реализация Moedelo.Accounts.ApiClient.Abstractions для использования в net8+ приложениях (TargetFramework: netstandard2.1)
- Moedelo.Accounts.ApiClient.NetFramework (Moedelo.Accounts.ApiClient.NetFramework.csproj): реализация Moedelo.Accounts.ApiClient.Abstractions для использования в NET Framework 4.7.2 приложениях. (TargetFramework: net472)
- Moedelo.Accounts.Kafka.NetCore (Moedelo.Accounts.Kafka.NetCore.csproj): реализация Moedelo.Accounts.Kafka.Abstractions для использования в net8+ приложениях. (TargetFramework: netstandard2.0)
- Moedelo.Accounts.Kafka.NetFramework (Moedelo.Accounts.Kafka.NetFramework.csproj): реализация Moedelo.Accounts.Kafka.Abstractions для использования в NET Framework 4.7.2 приложениях. (TargetFramework: net472)

## Документация и примеры
Читай код

## Поддержка и контакты
- Владелец/команда: Команда Архитектуры
- Ответственный: (указать при необходимости)
- Канал для вопросов/обсуждений: (указать при необходимости)

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория.