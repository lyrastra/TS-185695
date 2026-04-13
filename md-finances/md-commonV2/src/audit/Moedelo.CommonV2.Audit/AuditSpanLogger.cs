using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.Audit.Writers;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.Audit
{
    [InjectAsSingleton(typeof(IAuditSpanLogger))]
    public sealed class AuditSpanLogger : IAuditSpanLogger, IDisposable
    {
        private const string Tag = nameof(AuditSpanLogger);

        private const int DefaultFlushDelaySec = 10;
        private const int DefaultFlushBufferLength = 1000;
        
        private bool disposed;

        private CancellationTokenSource cts = new CancellationTokenSource();

        private readonly object lockObj = new object();

        private readonly Task flushTask;
        private volatile List<IAuditSpan> currentBuffer = new List<IAuditSpan>();

        private readonly IAuditKafkaWriter kafkaWriter;
        private readonly ILogger logger;
        private volatile int flushCount = 0; 

        public AuditSpanLogger(
            ISettingRepository settingRepository,
            IAuditKafkaWriter kafkaWriter,
            ILogger logger)
        {
            this.kafkaWriter = kafkaWriter;
            this.logger = logger;

            var flushDelaySec = settingRepository.GetInt("AuditLoggerFlushDelaySec", DefaultFlushDelaySec);
            var flushBufferLength = settingRepository.GetInt("AuditLoggerFlushBufferLength", DefaultFlushBufferLength);
            flushTask = Task.Run(() => FlushingLoopAsync(
                flushDelaySec,
                flushBufferLength,
                cts.Token),
                cts.Token);
        }
        
        public void FireAndForgetLog(IAuditSpan span)
        {
            try
            {
                lock (lockObj)
                {
                    currentBuffer.Add(span);
                }
            }
            catch
            {
                //ignore
            }
        }

        private async Task FlushingLoopAsync(
            IntSettingValue flushDelaySec, 
            IntSettingValue flushBufferLength,
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await WaitForBufferFlushingMomentAsync(flushDelaySec, flushBufferLength, cancellationToken).ConfigureAwait(false);
                await FlushBufferAsync().ConfigureAwait(false);
            }
        }

        private async Task WaitForBufferFlushingMomentAsync(
            IntSettingValue flushDelaySec, 
            IntSettingValue flushBufferLength,
            CancellationToken cancellationToken)
        {
            var passedSecondsCount = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                int bufferCount;

                lock (lockObj)
                {
                    bufferCount = currentBuffer.Count;
                }

                var maxBufferLength = flushBufferLength.Value;
                var autoFlushEverySeconds = flushDelaySec.Value;

                if (bufferCount >= maxBufferLength)
                {
                    // буфер уже слишком большой - пора сбрасывать
                    return;
                }
                if (bufferCount > 0 && passedSecondsCount >= autoFlushEverySeconds)
                {
                    // в буфере что-то есть и прошёл таймаут на сброс данных
                    return;
                }

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken).ConfigureAwait(false);
                    passedSecondsCount += 1;
                }
                catch (TaskCanceledException)
                {
                    // сработал cancellationToken - проглатываем исключение и заканчиваем ожидание
                    return;
                }
            }
        }

        private async Task FlushBufferAsync()
        {
            int count = 0;
            
            try
            {
                List<IAuditSpan> oldBuffer;

                lock (lockObj)
                {
                    oldBuffer = currentBuffer;
                    if (oldBuffer?.Count == 0)
                    {
                        // нечего записывать
                        return;
                    }

                    currentBuffer = new List<IAuditSpan>(oldBuffer?.Count ?? 0);
                }

                if (oldBuffer == null || oldBuffer.Count == 0)
                {
                    return;
                }
                
                count = Interlocked.Increment(ref flushCount);

                await kafkaWriter.WriteAsync(oldBuffer).ConfigureAwait(false);

                if (count == 1)
                {
                    logger.Info(Tag, "AuditSpanLogger first flush done");
                }
            }
            catch(Exception exception)
            {
                if (count is 0 or 1)
                {
                    logger.Error(Tag, "AuditSpanLogger flush error", exception);
                }
                // otherwise ignore
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (cts != null)
                {
                    cts.Cancel();
                    try
                    {
                        // дожидаемся когда закончится сбрасывание буферов
                        flushTask.Wait(TimeSpan.FromSeconds(2));
                    }
                    catch
                    {
                        // игнорируем все исключения
                    }

                    cts.Dispose();
                    cts = null;
                }
            }

            disposed = true;
        }

        ~AuditSpanLogger()
        {
            Dispose(false);
        }
    }
}