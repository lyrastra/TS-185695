using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

public static class SettingsExtensions
{
    public static bool IsProductionEnvironment(this ISettingRepository settings) => CheckEnvironmentIs(settings, "prod");
    public static bool IsBoxEnvironment(this ISettingRepository settings)  => CheckEnvironmentIs(settings, "box");
    public static bool IsDevEnvironment(this ISettingRepository settings) => CheckEnvironmentIs(settings, "dev") || CheckEnvironmentIs(settings, "local");
    public static bool IsStageEnvironment(this ISettingRepository settings) => CheckEnvironmentIs(settings, "stage");


    private static bool CheckEnvironmentIs(this ISettingRepository settings, string env)
    {
        var environmentKey = settings.Get("Environment").Value;
        return environmentKey == env;
    }
}