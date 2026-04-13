using System;
using System.IO;

namespace Moedelo.InfrastructureV2.Logging;

internal class FileLogger : ILogInternal
{
    private const string DefaultLogDirName = "C:\\Logs";

    private static readonly string LogDirName =
        Environment.GetEnvironmentVariable("MD_LOGS_DIRECTORY") ?? DefaultLogDirName;

    private readonly string appName;

    public FileLogger(string appName)
    {
        this.appName = appName;
    }

    public bool IsLevelEnabled(LogLevel level)
    {
        switch (level)
        {
            case LogLevel.Trace:
            case LogLevel.Debug:
#if DEBUG
                return true;
#else
                    return false;
#endif
            default:
                return true;
        }
    }

    public void WriteEvent(LogLevel level, string logEvent)
    {
        try
        {
            if (!Directory.Exists(LogDirName))
            {
                Directory.CreateDirectory(LogDirName);
            }

            var logFilename = $"{LogDirName}\\{appName} {DateTime.Today:yyyy-MM-dd}.log";
            using (var fileWriter = new StreamWriter(logFilename, true))
            {
                fileWriter.WriteLine(logEvent);
            }
        }
        catch
        {
            // ignored
        }
    }
}
