using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.Extensions;
using Moedelo.Common.Logging.Logger;
using Moedelo.Common.Logging.Utils;

namespace Moedelo.Common.Logging.Json;

internal sealed class LogMessageJsonConverter : JsonConverter<LogMessage>
{
    private static readonly IReadOnlyDictionary<LogLevel, string> LogLevels = Enum.GetValues<LogLevel>()
        .ToDictionary(value => value, value => value.ToString());

    public override LogMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new Exception("Этот класс не поддерживает десериализацию");
    }

    public override void Write(Utf8JsonWriter writer, LogMessage logMessage, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString(LogFields.Timestamp, (logMessage.Date ?? DateTime.Now).ToString("O"));
        writer.WriteString(LogFields.Level, LogLevels.GetValueOrDefault(logMessage.Level, "Unknown"));
        writer.WriteString(LogFields.AppName, logMessage.AppName);
        writer.WriteString(LogFields.Logger, logMessage.Logger);
        writer.WriteString(LogFields.Host, logMessage.Host);
        writer.WriteNumber(LogFields.Pid, logMessage.Pid);
        writer.WriteString(LogFields.Message, LogMessageUtils.GetLoggingMessage(ref logMessage));
        
        if (logMessage.Exception != null)
        {
            writer.WriteString(LogFields.StackTrace, logMessage.Exception.GetExceptionStackTrace());
        }

        if (logMessage.ExtraLogFields != null)
        {
            foreach (var field in logMessage.ExtraLogFields)
            {
                if (field.IsInt)
                {
                    writer.WriteNumber(field.Name, field.IntValue);
                }
                else if (field.IsString)
                {
                    writer.WriteString(field.Name, field.StringValue.ShrinkForLogging());
                }
                else
                {
                    throw new Exception("Неизвестный тип данных");
                }
            }
        }

        writer.WriteEndObject();
    }
}
