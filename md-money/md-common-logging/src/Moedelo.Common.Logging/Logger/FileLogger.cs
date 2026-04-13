using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.Extensions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Common.Logging.ExtraLog.Abstractions;
using Moedelo.Common.Logging.LoggerProviders;
using Moedelo.Common.Logging.Utils;

namespace Moedelo.Common.Logging.Logger
{
    internal sealed class FileLogger  : ILogger
    {
        private static readonly string HostName = DnsHelper.GetHostName();
        private static readonly int Pid = Environment.ProcessId;

        private readonly FileLoggerProvider provider;
        private readonly string appName;
        private readonly string logger;
        private readonly IReadOnlyCollection<IExtraLogFieldsProvider> extraLogFieldsProviders;
        private readonly ILoggingSettings loggingSettings;
        private readonly IExternalScopeProvider scopeProvider;

        public FileLogger(
            FileLoggerProvider provider,
            string appName, 
            string logger, 
            ILoggingSettings loggingSettings,
            IReadOnlyCollection<IExtraLogFieldsProvider> extraLogFieldsProviders,
            IExternalScopeProvider scopeProvider)
        {
            this.provider = provider;
            this.logger = logger;
            this.appName = appName;
            this.scopeProvider = scopeProvider;
            this.extraLogFieldsProviders = extraLogFieldsProviders ?? Array.Empty<IExtraLogFieldsProvider>();
            this.loggingSettings = loggingSettings;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var extraLogFields = new List<ExtraLogField>();
            extraLogFields.AddRange(extraLogFieldsProviders.SelectMany(p => p.Get()));

            scopeProvider?.ForEachScope((scope, _) =>
            {
                switch (scope)
                {
                    case KeyValuePair<string, string> keyValue:
                        extraLogFields.Add(new ExtraLogField(
                            keyValue.Key,
                            keyValue.Value?.MaskSensitiveJsonFields()));
                        break;
                    case KeyValuePair<string, object> keyValue:
                        extraLogFields.Add(new ExtraLogField(
                            keyValue.Key,
                            keyValue.Value?.ToJsonString().MaskSensitiveJsonFields()));
                        break;
                    case IEnumerable<KeyValuePair<string, string>> map:
                        extraLogFields.AddRange(map
                            .Select(item => new ExtraLogField(
                                item.Key,
                                item.Value.MaskSensitiveJsonFields())));
                        break;
                    case IEnumerable<KeyValuePair<string, object>> map:
                        extraLogFields.AddRange(map
                            .Select(item => new ExtraLogField(
                                item.Key,
                                item.Value?.ToJsonString().MaskSensitiveJsonFields())));
                        break;
                }
            }, state);

            var message = formatter(state, exception);

            Log(new LogMessage(
                logLevel,
                HostName,
                Pid,
                appName,
                logger, 
                message,
                DateTime.Now,
                exception,
                extraLogFields));
        }

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = false
        };

        private void Log(LogMessage logMessage)
        {
            var jsonSerialized = JsonSerializer.Serialize(logMessage, JsonSerializerOptions);

            provider.WriteLogMessage(jsonSerialized);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= loggingSettings.MinLogLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return scopeProvider?.Push(state) ?? NullScope.Instance;
        }
    }
}