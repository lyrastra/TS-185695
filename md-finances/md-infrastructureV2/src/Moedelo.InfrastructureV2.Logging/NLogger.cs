using System;

using Common.Logging;

namespace Moedelo.InfrastructureV2.Logging
{
    internal sealed class NLogger : ILogInternal
    {
        private readonly ILog log;
        private readonly FileLogger fileLog;

        public NLogger(ILog log, FileLogger fileLog)
        {
            this.log = log;
            this.fileLog = fileLog;
        }

        public bool IsLevelEnabled(LogLevel level)
        {
#if DEBUG
            {
                return true;
            }
#endif
#pragma warning disable 162
// ReSharper disable once HeuristicUnreachableCode
            switch (level)
            {       
                case LogLevel.Trace:
                    return log.IsTraceEnabled;
                case LogLevel.Debug:
                    return log.IsDebugEnabled;
                case LogLevel.Info:
                    return log.IsInfoEnabled;
                case LogLevel.Warn:
                    return log.IsWarnEnabled;
                case LogLevel.Error:
                    return log.IsErrorEnabled;
                case LogLevel.Fatal:
                    return log.IsFatalEnabled;
                default:
                    return false;
            };
#pragma warning restore 162
        }

        public void WriteEvent(LogLevel level, string logEvent)
        {
            try
            {
                switch (level)
                {
                    case LogLevel.Trace:
                        log.Trace(logEvent);
                        break;
                    case LogLevel.Debug:
                        log.Debug(logEvent);
                        break;
                    case LogLevel.Info:
                        log.Info(logEvent);
                        break;
                    case LogLevel.Warn:
                        log.Warn(logEvent);
                        break;
                    case LogLevel.Error:
                        log.Error(logEvent);
                        break;
                    case LogLevel.Fatal:
                        log.Fatal(logEvent);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(level), level, null);
                }
            }
            catch (Exception e)
            {
                fileLog.WriteEvent(LogLevel.Fatal, $"Common.Logging write error. Exception: {e.Message}. Trace: {e.StackTrace}");
            }
        }
    }
}
