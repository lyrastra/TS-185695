using System.ComponentModel;
using Newtonsoft.Json;

namespace Moedelo.InfrastructureV2.Json
{
    internal static class MdSerializerSettings
    {
        private static JsonSerializerSettings DateTimeZoneHandlingLocal => new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Local
        };

        internal static JsonSerializerSettings GetBy(
            MdSerializerSettingsEnum settingEnum,
            MdSerializerNullHandling nullHandling)
        {
            var settings = GetBy(settingEnum);

            if (settings == null)
            {
                return nullHandling == MdSerializerNullHandling.Ignore
                    ? new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                    : null;
            }

            settings.NullValueHandling = nullHandling == MdSerializerNullHandling.Include
                ? NullValueHandling.Include
                : NullValueHandling.Ignore;

            return settings;
        }

        internal static JsonSerializerSettings GetBy(MdSerializerSettingsEnum settingEnum)
        {
            switch (settingEnum)
            {
                case MdSerializerSettingsEnum.DateTimeZoneHandlingLocal:
                    return DateTimeZoneHandlingLocal;
                case MdSerializerSettingsEnum.None:
                    return null;
                default:
                    throw new InvalidEnumArgumentException(nameof(settingEnum), (int) settingEnum,
                        typeof(MdSerializerSettingsEnum));
            }
        }
    }
}