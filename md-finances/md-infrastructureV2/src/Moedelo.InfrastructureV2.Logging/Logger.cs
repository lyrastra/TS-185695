using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Logging;
using Common.Logging.NLog;
using Common.Logging.Simple;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Logging.Helper;

namespace Moedelo.InfrastructureV2.Logging;

public sealed class Logger : ILogger, IDisposable
{
    private readonly string appName;
    private readonly ILogInternal innerLog;
    private readonly AsyncLocalLazy<ConcurrentBag<KeyValuePair<LogLevel, string>>> localCache;
    private readonly List<ILogEventExtender> extenders = [];

    private static Common.Logging.Configuration.NameValueCollection Options => new()
    {
        { "configType", "FILE" },
        { "configFile", "C:/Uploads/Settings/NLog.config" }
    }; 

    public Logger()
    {
        const string AppNameSettingsKey = "appName";

        appName = ConfigurationManager.AppSettings[AppNameSettingsKey];
        LogManager.Adapter = new NLogLoggerFactoryAdapter(Options);
        ILog log = GetLogInstance(appName);
        var fileLogger = new FileLogger(appName);

        innerLog = log != null
            ? new NLogger(log, fileLogger)
            : fileLogger;

        localCache = new AsyncLocalLazy<ConcurrentBag<KeyValuePair<LogLevel, string>>>(static () => []);
    }

    public void Dispose()
    {
        FlushLog();
    }

    private static ILog GetLogInstance(string appName)
    {
        try
        {
            var log = LogManager.GetLogger(appName);

            if (log is NoOpLogger)
            {
                throw new Exception("Common.Logging is not configured");
            }

            return log;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void Trace(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Log(LogLevel.Trace, tag, message, null, context, extraData, environment, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
    }

    public void Debug(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Log(LogLevel.Debug, tag, message, null, context, extraData, environment, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
    }

    public void Info(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Log(LogLevel.Info, tag, message, null, context, extraData, environment, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
    }

    public void Warning(
        string tag, 
        string message, 
        Exception exception = null, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Log(LogLevel.Warn, tag, message, exception, context, extraData, environment, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
    }

    public void Error(
        string tag, 
        string message, 
        Exception exception = null, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Log(LogLevel.Error, tag, message, exception, context, extraData, environment, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
    }

    public void Fatal(
        string tag, 
        string message, 
        Exception exception = null, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Log(LogLevel.Fatal, tag, message, exception, context, extraData, environment, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
    }

    public void TraceIfError(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Log(LogLevel.Trace, tag, message, null, context, extraData, environment, ifError: true, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
    }

    public void DebugIfError(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Log(LogLevel.Debug, tag, message, null, context, extraData, environment, ifError: true, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
    }

    public void AddLogEventExtender(ILogEventExtender extender)
    {
        if (extenders.Contains(extender) == false)
        {
            extenders.Add(extender);
        }
    }

    private void Log(
        LogLevel logLevel, 
        string tag, 
        string message, 
        Exception exception = null, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment httpEnvironment = null, 
        bool ifError = false, 
        string callerMemberName = "",
        string callerFilePath = "",
        int callerLineNumber = 0)
    {
        if (!innerLog.IsLevelEnabled(logLevel) && !ifError)
        {
            return;
        }

        var eventObject = LoggerHelper.GetEventObject(
            appName, 
            tag, 
            logLevel, 
            message, 
            context, 
            exception,
            extraData, 
            httpEnvironment, 
            callerMemberName, 
            callerFilePath, 
            callerLineNumber);

        foreach (var nameValue in extenders.SelectMany(extender => extender.EnumerateLogExtraEventFields()))
        {
            eventObject[nameValue.Key] = nameValue.Value;
        }

        try
        {
            var serializedEvent = LoggerHelper.ConvertToJson(eventObject);
            var truncatedEvent = LoggerHelper.TruncateEvent(serializedEvent, eventObject, exception);
            if (ifError)
            {
                CacheLog(logLevel, truncatedEvent);
            }
            else
            {
                if (logLevel < LogLevel.Warn)
                {
                    FlushLog();
                }
                innerLog.WriteEvent(logLevel, truncatedEvent);
            }
        }
        catch (Exception e)
        {
            var serializedEvent = LoggerHelper.SerializeToPlain(eventObject);
            var truncatedEvent = LoggerHelper.TruncateEvent(serializedEvent, eventObject);
            innerLog.WriteEvent(logLevel, truncatedEvent);

            var errorEvent = LoggerHelper.GetEventObject(appName, tag, LogLevel.Error, "Serialization error", exception: e, extraData: new {LoggerError = true});

            var serializedErrorEvent = LoggerHelper.SerializeToPlain(errorEvent);
            innerLog.WriteEvent(logLevel, serializedErrorEvent);
        }
    }

    private void CacheLog(LogLevel level, string logEvent)
    {
        var list = localCache.GetOrCreateValue();
        list.Add(new KeyValuePair<LogLevel, string>(level, logEvent));
    }

    private void FlushLog()
    {
        var list = localCache.GetValue();

        if (list != null)
        {
            KeyValuePair<LogLevel, string> kv;
            while (!list.TryTake(out kv))
            {
                innerLog.WriteEvent(kv.Key, kv.Value);
            }
        }
    }
}