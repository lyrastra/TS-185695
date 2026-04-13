using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moedelo.Common.Logging.ExtraLog.Abstractions;
using Moedelo.Common.Logging.Logger;
using Moedelo.Common.Logging.Options;
using Moedelo.Common.Settings.Abstractions;

namespace Moedelo.Common.Logging.LoggerProviders
{
    internal sealed class FileLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private static readonly TimeSpan FlushTimeout = TimeSpan.FromSeconds(1);
        private const int FlushBufferLength = 1000;
        
        private readonly List<string> currentBuffer = new (FlushBufferLength);

        private bool disposed;

        private readonly string appName;
        private readonly IExtraLogFieldsProvider[] extraLogFieldsProviders;

        private readonly CancellationTokenSource cts = new ();
        private readonly ConcurrentDictionary<string, ILogger> loggers = new ();
        private readonly BlockingCollection<string> messageQueue = new (new ConcurrentQueue<string>());
        private readonly Task flushTask;
        private readonly ILoggingSettings loggingSettings; 
        private readonly bool needCopyLogsToStdout;
        private IExternalScopeProvider scopeProvider;

        public FileLoggerProvider(
            IOptionsMonitor<FileLoggerOptions> options,
            ILoggingSettings loggingSettings,
            IEnumerable<IExtraLogFieldsProvider> extraLogFieldsProviders)
        {
            var loggerOptions = options.CurrentValue;
            this.needCopyLogsToStdout = Environment.GetEnvironmentVariable("MD_COPY_LOGS_TO_STDOUT") == "1";
            this.appName = loggerOptions.FileName;
            this.loggingSettings = loggingSettings;
            this.extraLogFieldsProviders = extraLogFieldsProviders?.ToArray() ?? Array.Empty<IExtraLogFieldsProvider>();
            flushTask = Task.Factory.StartNew(ProcessLogQueueAsync, TaskCreationOptions.LongRunning).Unwrap();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(
                categoryName, 
                static (tag, self) => new FileLogger(
                    self,
                    self.appName,
                    tag,
                    self.loggingSettings,
                    self.extraLogFieldsProviders,
                    self.scopeProvider),
                this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void WriteLogMessage(string logMessage)
        {
            if (messageQueue.IsAddingCompleted == false)
            {
                messageQueue.Add(logMessage);
            }
        }

        private async Task ProcessLogQueueAsync()
        {
            while (cts.IsCancellationRequested == false)
            {
                await TryTakeMessagesAndWriteToFileAsync().ConfigureAwait(false);
                await Task.Delay(FlushTimeout).ConfigureAwait(false);
            }

            await TryTakeMessagesAndWriteToFileAsync().ConfigureAwait(false);
        }

        private async Task TryTakeMessagesAndWriteToFileAsync()
        {
            while (messageQueue.TryTake(out var message))
            {
                currentBuffer.Add(message);
            }

            await WriteToFileAsync(currentBuffer).ConfigureAwait(false);
            currentBuffer.Clear();
        }

        private async Task WriteToFileAsync(IReadOnlyCollection<string> messages)
        {
            try
            {
                if (messages.Count == 0)
                {
                    return;
                }

                var targetDirectory = loggingSettings.LogDirectoryPath;
                
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory!);
                }

                var fileName = $"{appName} {DateTime.Today:yyyy-MM-dd}.log";
                var logFilePath = Path.Combine(targetDirectory, fileName);

                await using var fileWriter = File.AppendText(logFilePath);

                foreach (var message in messages)
                {
                    await fileWriter.WriteLineAsync(message).ConfigureAwait(false);

                    if (needCopyLogsToStdout)
                    {
                        Console.WriteLine(message);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                messageQueue.CompleteAdding();
                cts.Cancel();
                flushTask.Wait();
                cts.Dispose();
                messageQueue.Dispose();
            }

            disposed = true;
        }

        ~FileLoggerProvider()
        {
            Dispose(false);
        }

        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            this.scopeProvider = scopeProvider;
        }
    }
}