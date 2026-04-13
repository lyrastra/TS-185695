using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Tests.Models;

/// <summary>
/// Данные о запущенном консьюмере
/// </summary>
/// <param name="Handlers">зарегистрированные обработчики</param>
/// <param name="CancellationToken">токен отмены операции</param>
/// <param name="ConsumerTaskResultSource">источник для отслеживания состояния таски консьюмера</param>
/// <param name="ConsumerTaskForceCompletionSource">источник для форсированного завершения таски косньюмера</param>
public record ConsumerTaskArgs(
    IKafkaConsumerHandlers<TestKafkaMessage> Handlers,
    CancellationToken CancellationToken,
    TaskCompletionSource ConsumerTaskResultSource,
    TaskCompletionSource ConsumerTaskForceCompletionSource);

public static class ConsumerTaskArgsExtensions
{
    public static Task CallOnMessage(this ConsumerTaskArgs args, TestKafkaMessage msg)
    {
        return args.Handlers.HandleMessage(msg, args.CancellationToken);
    }
}

public class FakeTaskFactory : IDisposable
{
    public FakeTaskFactory(CancellationToken testGuardToken)
    {
        TestGuardToken = testGuardToken;
    }

    public void Dispose()
    {
    }

    public CancellationToken TestGuardToken { get; }
    public List<ConsumerTaskArgs> RunningTasksArgs { get; } = new();

    /// <summary>
    /// Информация о последнем запущенном консьюмере
    /// </summary>
    public ConsumerTaskArgs LastRunning =>  RunningTasksArgs.Last();

    public Func<KafkaConsumerSettings<TestKafkaMessage>, CancellationToken, Task> TaskFactory =>
        async (settings, cancellationToken) =>
        {
            var consumerTask = new TaskCompletionSource();
            var taskArgs = new ConsumerTaskArgs(
                settings.Handlers,
                cancellationToken,
                consumerTask,
                new());
            RunningTasksArgs.Add(taskArgs);
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, TestGuardToken);
            try
            {
                // ждём либо когда нас явно завершат (из теста), либо когда отозвут токен
                await taskArgs.ConsumerTaskForceCompletionSource.Task
                    .WaitAsync(cts.Token)
                    .ConfigureAwait(false);
            }
            catch when (cts.IsCancellationRequested)
            {
                // помечаем таску, как прерванную
                consumerTask.TrySetCanceled();
            }
            finally
            {
                RunningTasksArgs.Remove(taskArgs);
                
                // помечаем таску, как законченную
                consumerTask.TrySetResult();
            }
        };
}
