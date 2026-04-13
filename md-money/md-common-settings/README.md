# md-common-settings

**Владелец/команда:** Команда Архитектуры

## Назначение

Инфраструктурная библиотека для централизованного доступа, хранения и типобезопасной работы с настройками (application settings) в .NET и .NET Framework. Обеспечивает абстракции, репозитории, расширения и интеграцию с конфигурацией и логированием.

## Основные возможности
- Интерфейсы и модели для работы с настройками (ISettingRepository, ILoggingSettings и др.)
- Реализация репозитория настроек с поддержкой расширений
- Интеграция с Microsoft.Extensions.Configuration и логированием
- Вспомогательные классы и расширения для работы с настройками

## Быстрый старт
1. Подключите сабмодуль:
   ```sh
   git submodule add https://gitlab.mdtest.org/development/infra/md-common-settings.git
   ```
2. Добавьте нужные проекты из папки `src/` в ваш solution (.sln) через Visual Studio или вручную.
3. Используйте абстракции и репозиторий в своём коде:
   ```csharp
   using Moedelo.Common.Settings;
   ISettingRepository repo = ...;
   var value = repo.GetValue("MySetting");
   ```

## Зависимости
- md-infrastructure-core-consul
- md-infrastructure-core-dependencyinjection
- md-infrastructure-core-json
- Требуется .NET Standard 2.0+ или .NET Framework 4.7.2

## Совместимость и ограничения
- Совместим с .NET Standard 2.0+, .NET Core 2.0+, .NET 5/6/7/8+, .NET Framework 4.7.2
- Не содержит бизнес-логики, только инфраструктурные компоненты для работы с настройками
- Не рекомендуется использовать для хранения бизнес-логики

## Состав и проекты
- Moedelo.Common.Settings.Abstractions (Moedelo.Common.Settings.Abstractions.csproj): интерфейсы и модели для работы с настройками, TargetFramework: netstandard2.0
- Moedelo.Common.Settings (Moedelo.Common.Settings.csproj): реализация репозитория, расширения, интеграция с конфигами и логированием, TargetFramework: netstandard2.0
- Moedelo.Common.Settings.NetFramework (Moedelo.Common.Settings.NetFramework.csproj): расширения для интеграции с .NET Framework, TargetFramework: net472

## Документация и примеры

### Ключевые абстракции
- **ISettingRepository** — контракт для получения значения настройки по имени: `SettingValue Get(string settingName);`
- **ILoggingSettings** — контракт для доступа к настройкам логирования:
  - `LogLevel MinLogLevel { get; }` — минимальный уровень логирования
  - `EnumSettingValue<LogLevel> MinLogLevelSetting { set; }` — настройка для получения текущего уровня логирования
  - `string LogDirectoryPath { get; }` — путь к каталогу логов
- **ISettingsConfigurations** — контракт для доступа к конфигурации приложения: `SettingsConfig Config { get; }`
- **SettingValue** — тип-обёртка для значения настройки, поддерживает ленивое получение, проверку на null, создание константных значений
- **EnumSettingValue<TEnum>** — тип-обёртка для enum-настроек, безопасно преобразует строковое значение в enum
- **SettingsConfig** — модель конфигурации приложения (окружение, шардинг, имя приложения, домен и др.)

Для получения дополнительных сведений и уточнения деталей реализации обращайтесь к кодовой базе ("читай код!").

## Best practices
- Используйте абстракции для DI и тестирования
- Для получения целочисленных настроек используйте extension-метод `GetInt` (возвращает IntSettingValue с безопасным преобразованием и поддержкой значения по умолчанию)
- Для получения enum-настроек используйте extension-метод `GetEnum<TEnum>` (возвращает EnumSettingValue<TEnum> с безопасным преобразованием и поддержкой значения по умолчанию)
- Для контроля обязательности настройки используйте метод `ThrowExceptionIfNull` у SettingValue — он выбросит исключение, если значение отсутствует

## Поддержка и контакты
- Владелец/команда: Команда Архитектуры

## История изменений (Changelog)
- Основные изменения фиксируются в истории коммитов репозитория.
