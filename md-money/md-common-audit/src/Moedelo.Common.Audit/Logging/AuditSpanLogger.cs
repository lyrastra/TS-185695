using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Logging.Interfaces;
using Moedelo.Common.Audit.Writers.Kafka;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Audit.Logging;

[InjectAsSingleton(typeof(IAuditSpanLogger))]
internal sealed class AuditSpanLogger : IAuditSpanLogger, IDisposable
{
    private const int DefaultFlushDelaySec = 10;
    private const int DefaultFlushBufferLength = 1000;
    private const int BufferInitialCapacity = 1000 * 2;

    private bool disposed;

    private CancellationTokenSource cts = new();
    private readonly object lockObj = new();

    private readonly Task flushTask;
    // двух списков должно хватить: пока в один пишутся события, другой записывается в кафку, потом они меняются местами
    private readonly SimpleListPool<IAuditSpanData> bufferListsPool = new(poolCapacity: 2, listCapacity: BufferInitialCapacity);
    private volatile List<IAuditSpanData> currentBuffer;
    private readonly IAuditKafkaWriter kafkaWriter;

    public AuditSpanLogger(
        ISettingRepository settingRepository,
        IAuditKafkaWriter kafkaWriter)
    {
        this.kafkaWriter = kafkaWriter;
        currentBuffer = bufferListsPool.Capture();

        var flushDelaySec = settingRepository.GetInt("AuditLoggerFlushDelaySec", DefaultFlushDelaySec);
        var flushBufferLength = settingRepository.GetInt("AuditLoggerFlushBufferLength", DefaultFlushBufferLength);
        flushTask = Task.Run(() => FlushingLoopAsync(
            flushDelaySec,
            flushBufferLength,
            cts.Token));
    }

    public void FireAndForgetLog(IAuditSpanData span)
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
            await WaitForBufferFlushingMomentAsync(flushDelaySec, flushBufferLength, cancellationToken)
                .ConfigureAwait(false);
            await FlushBufferAsync().ConfigureAwait(false);
        }
    }

    private async Task WaitForBufferFlushingMomentAsync(
        IntSettingValue flushDelaySec,
        IntSettingValue flushBufferLength,
        CancellationToken cancellationToken)
    {
        var passedSecondsCount = 0;
        var delayTimeSpan = TimeSpan.FromSeconds(1);

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
                await Task.Delay(delayTimeSpan, cancellationToken).ConfigureAwait(false);
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
        List<IAuditSpanData> oldBuffer = null;
        try
        {
            lock (lockObj)
            {
                oldBuffer = currentBuffer;
                if (oldBuffer.Count == 0)
                {
                    // нечего записывать
                    return;
                }

                currentBuffer = bufferListsPool.Capture();
            }

            if (oldBuffer.Count > 0)
            {
                await kafkaWriter.WriteAsync(oldBuffer).ConfigureAwait(false);
            }
        }
        catch
        {
            //ignore
        }
        finally
        {
            if (oldBuffer != null)
            {
                bufferListsPool.Release(oldBuffer);
            }
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