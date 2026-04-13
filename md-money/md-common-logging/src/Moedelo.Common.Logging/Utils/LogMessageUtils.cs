#nullable enable
using System;
using System.Text;
using Moedelo.Common.Logging.Extensions;
using Moedelo.Common.Logging.Logger;

namespace Moedelo.Common.Logging.Utils;

internal static class LogMessageUtils
{
    private const int MaxMessageLength = 1024 * 4;

    public static string GetLoggingMessage(ref LogMessage logMessage)
    {
        var stringValue = logMessage.Message;

        if (stringValue.Length <= MaxMessageLength && logMessage.Exception == null)
        {
            return stringValue;
        }

        return new StringBuilder()
            .AppendShrinking(stringValue, MaxMessageLength)
            .AppendExceptionLoggingMessage(logMessage.Exception)
            .ToString();
    }

    public static string? ShrinkForLogging(this string? value)
    {
        if (value is not { Length: > MaxMessageLength })
        {
            return value;
        }

        return new StringBuilder()
            .AppendShrinking(value, MaxMessageLength)
            .ToString();
    }

    private static StringBuilder AppendShrinking(this StringBuilder valueBuilder, string stringValue, int maxLength)
    {
        if (stringValue.Length > maxLength)
        {
            valueBuilder.Append(stringValue.AsSpan(0, maxLength));
            valueBuilder.Append("...[");
            valueBuilder.Append(maxLength);
            valueBuilder.Append('/');
            valueBuilder.Append(stringValue.Length);
            valueBuilder.Append(" first chars]");
        }
        else
        {
            valueBuilder.Append(stringValue);
        }

        return valueBuilder;
    }

    private static StringBuilder AppendExceptionLoggingMessage(this StringBuilder builder, Exception? exception)
    {
        if (exception == null)
        {
            return builder;
        }
        builder.Append(" (");

        for (var current = exception; current != null; current = current.InnerException)
        {
            builder.Append($"{current.GetType()}: {current.GetInvariantCultureMessage()}");

            if (current.InnerException != null)
            {
                builder.Append(" --> ");
            }
        }

        builder.Append(')');

        return builder;
    }
}
