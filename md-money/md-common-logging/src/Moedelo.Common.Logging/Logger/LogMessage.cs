using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.ExtraLog.Abstractions;
using Moedelo.Common.Logging.Json;

namespace Moedelo.Common.Logging.Logger
{
    [JsonConverter(typeof(LogMessageJsonConverter))]
    internal readonly record struct LogMessage(
        LogLevel Level,
        string Host,
        int Pid,
        string AppName,
        string Logger,
        string Message,
        DateTime? Date = null,
        Exception Exception = null,
        IReadOnlyCollection<ExtraLogField> ExtraLogFields = null);
}