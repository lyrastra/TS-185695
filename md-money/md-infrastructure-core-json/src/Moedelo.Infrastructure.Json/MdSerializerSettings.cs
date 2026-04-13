using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Moedelo.Infrastructure.Json;

public static class MdSerializerSettings
{
    private static readonly TimeSpanToGoDurationConverter TimeSpanToGoDurationConverter = new ();
    private static readonly CamelCasePropertyNamesContractResolver CamelCasePropertyNamesContractResolver = new ();
    private static JsonSerializerSettings None => null;

    private static JsonSerializerSettings IgnoreNullValuesSettings => new()
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    public static JsonSerializerSettings GetBy(
        MdSerializerSettingsEnum options,
        MdSerializerNullHandling nullHandling)
    {
        var settings = GetBy(options);

        if (settings == null)
        {
            return nullHandling == MdSerializerNullHandling.Ignore
                ? IgnoreNullValuesSettings
                : null;
        }

        settings.NullValueHandling = nullHandling == MdSerializerNullHandling.Include
            ? NullValueHandling.Include
            : NullValueHandling.Ignore;

        return settings;
    }

    public static JsonSerializerSettings GetBy(MdSerializerSettingsEnum options)
    {
        if (options == MdSerializerSettingsEnum.None)
        {
            return None;
        }

        var settings = new JsonSerializerSettings();

        if (options.HasFlag(MdSerializerSettingsEnum.CamelCaseNamingStrategy))
        {
            settings.ContractResolver = CamelCasePropertyNamesContractResolver;
        }

        if (options.HasFlag(MdSerializerSettingsEnum.ToLocalDateTimeConverter))
        {
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        }
            
        if (options.HasFlag(MdSerializerSettingsEnum.ToIsoDateTimeConverter))
        {
            settings.DateFormatString = DateFormats.IsoDate;
        }
        else if(options.HasFlag(MdSerializerSettingsEnum.ToMdDateTimeConverter))
        {
            settings.DateFormatString = DateFormats.MdDate;
        }
            
        if (options.HasFlag(MdSerializerSettingsEnum.TypeNameHandlingAll))
        {
            settings.TypeNameHandling = TypeNameHandling.All;
        }

        if (options.HasFlag(MdSerializerSettingsEnum.ConvertTimeSpanToGoDuration))
        {
            settings.Converters.Add(TimeSpanToGoDurationConverter);
        }

        return settings;

    }
}