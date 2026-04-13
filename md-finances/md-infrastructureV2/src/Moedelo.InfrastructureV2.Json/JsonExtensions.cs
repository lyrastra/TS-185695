using System;
using System.IO;
using System.Linq;
using System.Text;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Moedelo.InfrastructureV2.Json;

public static class JsonExtensions
{
    private static readonly string[] GenericSensitiveProperties = [
        "Password", "Phone", "PublicToken", "apikey",
        "access_token", "refresh_token",
        "executioncontexttoken"]; 
        
    private static readonly IContractResolver MaskingPropertiesOnlyByAttributeContractResolver =
        new MaskingPropertiesResolver(Array.Empty<string>(), true);
        
    private static readonly IContractResolver MaskingGenericSensitivePropertiesContractResolver =
        new MaskingPropertiesResolver(GenericSensitiveProperties, false);

    private static readonly IContractResolver MaskingGenericSensitivePropertiesAndByAttributeContractResolver =
        new MaskingPropertiesResolver(GenericSensitiveProperties, true);
        
    public static string ToJsonString(
        this object obj,
        MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None,
        MdSerializerNullHandling nullHandling = MdSerializerNullHandling.Include)
    {
        var settings = MdSerializerSettings.GetBy(settingEnum, nullHandling); 
            
        return JsonConvert.SerializeObject(obj, settings);
    }
        
    public static string ToJsonString(
        this object obj,
        MdSerializationSettings serializationSettings)
    {
        var settings = MdSerializerSettings.GetBy(
            serializationSettings.GetMdSerializerSettingsEnum(),
            serializationSettings.NullHandling);

        settings.ContractResolver = ChooseContractResolver(serializationSettings) ?? settings.ContractResolver;

        return JsonConvert.SerializeObject(obj, settings);
    }

    private static IContractResolver ChooseContractResolver(MdSerializationSettings serializationSettings)
    {
        var maskByAttribute = serializationSettings.MaskPropertiesByAttribute; 
        var maskGenericProps = serializationSettings.MaskGenericSensitiveProperties;
        var maskCustomProps = serializationSettings.MaskProperties?.Count > 0;

        if (maskByAttribute)
        {
            if (maskGenericProps)
            {
                return maskCustomProps
                    ? new MaskingPropertiesResolver(serializationSettings.MaskProperties.Concat(GenericSensitiveProperties), true)
                    : MaskingGenericSensitivePropertiesAndByAttributeContractResolver;
            }

            return maskCustomProps
                ? new MaskingPropertiesResolver(serializationSettings.MaskProperties, true)
                : MaskingPropertiesOnlyByAttributeContractResolver;
        }

        if (maskGenericProps)
        {
            return maskCustomProps
                ? new MaskingPropertiesResolver(
                    serializationSettings.MaskProperties.Concat(GenericSensitiveProperties), false)
                : MaskingGenericSensitivePropertiesContractResolver;
        }

        return maskCustomProps
            ? new MaskingPropertiesResolver(serializationSettings.MaskProperties, false)
            : null;
    }

    public static T FromJsonString<T>(this string json, MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None)
    {
        return settingEnum == MdSerializerSettingsEnum.None
            ? JsonConvert.DeserializeObject<T>(json)
            : JsonConvert.DeserializeObject<T>(json, MdSerializerSettings.GetBy(settingEnum));
    }
        
    public static TData FromJsonStream<TData>(this Stream jsonStream,
        MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None,
        bool leaveStreamOpen = true)
    {
        using var streamReader = new StreamReader(jsonStream,
            Encoding.UTF8,
            detectEncodingFromByteOrderMarks: true,
            bufferSize: 1024,
            leaveOpen: leaveStreamOpen);

        using var jsonTextReader = new JsonTextReader(streamReader);
        var serializer = JsonSerializer.CreateDefault(MdSerializerSettings.GetBy(settingEnum));

        return serializer.Deserialize<TData>(jsonTextReader);
    }

    public static T FromJsonStringOrDefault<T>(this string json, MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None)
    {
        try
        {
            return settingEnum == MdSerializerSettingsEnum.None
                ? JsonConvert.DeserializeObject<T>(json)
                : JsonConvert.DeserializeObject<T>(json, MdSerializerSettings.GetBy(settingEnum));
        }
        catch
        {
            return default(T);
        }
    }

    public static long GetJsonLength<TData>(this TData data)
    {
        if (data == null)
        {
            return 0L;
        }

        using var jsonStream = data.ToJsonStream(new StreamLengthCalculator());

        return jsonStream.Length;
    }

    public static TStream ToJsonStream<TData, TStream>(
        this TData value,
        TStream stream,
        MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None) where TStream : Stream
    {
        var serializer = JsonSerializer.CreateDefault(MdSerializerSettings.GetBy(settingEnum));

        using var textWriter = new StreamWriter(stream);
        using var jsonWriter = new JsonTextWriter(textWriter);

        serializer.Serialize(textWriter, value, typeof(TData));

        return stream;
    }
    public static T FromJsonStringOrDefault<T>(this string json, T defaultValue, MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None)
    {
        try
        {
            return settingEnum == MdSerializerSettingsEnum.None
                ? JsonConvert.DeserializeObject<T>(json)
                : JsonConvert.DeserializeObject<T>(json, MdSerializerSettings.GetBy(settingEnum));
        }
        catch
        {
            return defaultValue;
        }
    }

    public static DateTime ToIsoDateTime(this string json)
    {
        return JsonConvert.DeserializeObject<DateTime>(json, new IsoDateConverter());
    }
}