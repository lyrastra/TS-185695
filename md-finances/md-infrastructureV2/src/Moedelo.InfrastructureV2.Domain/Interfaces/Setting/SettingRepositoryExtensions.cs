using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

public static class SettingRepositoryExtensions
{
    public static SettingValue GetRequired(
        this ISettingRepository settingRepository,
        string settingName)
    {
        return settingRepository.Get(settingName).Required();
    }

    public static IntSettingValue GetInt(
        this ISettingRepository settingRepository,
        string settingName)
    {
        return new IntSettingValue(settingRepository.Get(settingName));
    }

    public static IntSettingValue GetRequiredInt(
        this ISettingRepository settingRepository,
        string settingName)
    {
        return new IntSettingValue(settingRepository.Get(settingName))
            .Required();
    }

    public static IntSettingValue GetInt(
        this ISettingRepository settingRepository,
        string settingName,
        int defaultValue)
    {
        return new IntSettingValue(settingRepository.Get(settingName), defaultValue);
    }

    /// <summary>
    /// Получить значение настройки указанного типа из json-строки.
    /// При наличии настройки, она будет преобразована в тип TValue с помощью json-десериализации.
    /// </summary>
    /// <param name="settingRepository">репозиторий настроек</param>
    /// <param name="settingName">название настройки</param>
    /// <param name="defaultValue">значение по умолчанию (если настройка не задана)</param>
    /// <typeparam name="TValue">тип значения</typeparam>
    /// <returns></returns>
    public static TSettingValue<TValue> GetFromJson<TValue>(
        this ISettingRepository settingRepository,
        string settingName,
        TValue defaultValue)
    {
        return new TSettingValue<TValue>(settingRepository.Get(settingName), defaultValue);
    }

    /// <summary>
    /// Получить значение настройки указанного типа из json-строки.
    /// При наличии настройки, она будет преобразована в тип TValue с помощью json-десериализации.
    /// При отсутствии настройки будет выброшено исключение.
    /// </summary>
    /// <param name="settingRepository">репозиторий настроек</param>
    /// <param name="settingName">название настройки</param>
    /// <typeparam name="TValue">тип значения</typeparam>
    /// <returns></returns>
    public static TSettingValue<TValue> GetRequiredFromJson<TValue>(
        this ISettingRepository settingRepository,
        string settingName)
    {
        return new TSettingValue<TValue>(settingRepository.Get(settingName))
            .Required();
    }
}