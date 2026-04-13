using System;
using Newtonsoft.Json;

namespace Moedelo.Infrastructure.Json.Convertors;

internal sealed class TimeSpanToGoDurationConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        serializer.Serialize(writer, ((TimeSpan)value).ToGoDuration());
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer)
    {
        throw new InvalidOperationException($"Класс {nameof(TimeSpanToGoDurationConverter)} не предназначен для сериализации");
    }
}
