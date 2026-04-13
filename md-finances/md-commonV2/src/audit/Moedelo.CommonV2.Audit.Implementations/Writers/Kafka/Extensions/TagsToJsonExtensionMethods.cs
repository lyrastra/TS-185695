using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.CommonV2.Audit.Writers.Kafka.Extensions;

internal static class TagsToJsonExtensionMethods
{
    private struct KeyValueJsonTriplet
    {
        public KeyValueJsonTriplet(KeyValuePair<string, List<object>> keyValue)
        {
            const int jsonDelimiterLength = 1;

            KeyValue = keyValue;
            KeyJsonLength = keyValue.Key.GetJsonLength();
            ValueJsonLength = keyValue.Value.GetJsonLength();
            JsonLength = keyValue.Key.GetJsonLength() + ValueJsonLength + jsonDelimiterLength;
        }

        public KeyValuePair<string, List<object>> KeyValue { get; }

        public long KeyJsonLength { get; }
        public long ValueJsonLength { get; }
        public long JsonLength { get; }
    }

    internal static string TagsToJson(this IReadOnlyDictionary<string, List<object>> spanTags)
    {
        return spanTags.TagsToJson(int.MaxValue);
    }

    internal static string TagsToJson(this IReadOnlyDictionary<string, List<object>> spanTags, int maxSize)
    {
        try
        {
            var json = spanTags
                .ToJsonString(nullHandling: MdSerializerNullHandling.Ignore)
                .MaskSensitiveJsonFields();

            if (json.Length <= maxSize)
            {
                return json;
            }

            const int bracesLength = 2;
            var totalLength = 0L + bracesLength;

            var shrinkedJson = spanTags
                .Select(keyValue => new KeyValueJsonTriplet(keyValue))
                .OrderBy(triplet => triplet.JsonLength)
                .Select(triplet =>
                {
                    const int commaLength = 1;
                    const int jsonDelimiterLength = 1;

                    if (totalLength + triplet.JsonLength + commaLength < maxSize)
                    {
                        totalLength += triplet.JsonLength + commaLength;

                        return triplet.KeyValue;
                    }

                    var shrinkedValue = triplet.ValueJsonLength.FormatTagHasTooLargeValue(maxSize);
                    
                    totalLength += triplet.KeyJsonLength + jsonDelimiterLength + triplet.JsonLength + commaLength;

                    return new KeyValuePair<string, List<object>>(triplet.KeyValue.Key, [shrinkedValue]);
                })
                .ToDictionary(kv => kv.Key, kv => kv.Value)
                .ToJsonString();

            if (shrinkedJson.Length <= maxSize)
            {
                return shrinkedJson;
            }
            
            return ((long)shrinkedJson.Length).FormatTagHasTooLargeValue(maxSize);
        }
        catch(Exception exception)
        {
            try
            {
                return new { ErrorOnJsonSerialization = exception.Message }.ToJsonString();
            }
            catch
            {
                return "{\"Error\":\"Не удалось сериализовать ошибку сериализации\"}";
            }
        }
    }

    internal static string FormatTagHasTooLargeValue(this int size, int limit)
    {
        return ((long)size).FormatTagHasTooLargeValue(limit);
    }

    internal static string FormatTagHasTooLargeValue(this long size, int limit)
    {
        return $"Значение размером {size} отброшено, поскольку общий объём тэгов в json-виде превысил {limit}";
    }
}
