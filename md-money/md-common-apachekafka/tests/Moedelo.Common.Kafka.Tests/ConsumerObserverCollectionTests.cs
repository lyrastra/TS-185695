using FluentAssertions;
using Moedelo.Common.Kafka.Tests.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moq;

namespace Moedelo.Common.Kafka.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[CancelAfter(2000)]
public class ConsumerObserverCollectionTests
{
    #region SetUp

    private Mock<Func<TestKafkaMessage, Task>> onMessageMoq = null!;
    private Func<TestKafkaMessage, Task> onMessage = null!;
    private Mock<Func<Exception, Task>> onException = null!;
    private Mock<Func<KafkaConsumerSettings<TestKafkaMessage>, CancellationToken, Task>> consumerTaskFactoryMoq = null!;
    private Func<KafkaConsumerSettings<TestKafkaMessage>, CancellationToken, Task> taskFactory = null!;
    private KafkaConsumerConfig config = null!;

    private readonly TimeSpan freezeTimeout = TimeSpan.FromMilliseconds(50);

    [SetUp]
    public void Setup()
    {
        config = new KafkaConsumerConfig("brokerEndpoints",
            new KafkaConsumerGroupId("prefix", "groupId"),
            new KafkaTopicName("Topic.Name", "Kafka.Topic.Name"));

        taskFactory = (_0, _1) => Task.CompletedTask;
        onMessage = _ => Task.CompletedTask;
        this.consumerTaskFactoryMoq = new();
        consumerTaskFactoryMoq
            .Setup(foo =>
                foo(
                    It.IsAny<KafkaConsumerSettings<TestKafkaMessage>>(),
                    It.IsAny<CancellationToken>()))
            .Returns<KafkaConsumerSettings<TestKafkaMessage>, CancellationToken>(
                (arg0, arg1) =>
                    taskFactory(arg0, arg1));

        this.onMessageMoq = new Mock<Func<TestKafkaMessage, Task>>();
        onMessageMoq
            .Setup(foo => foo(It.IsAny<TestKafkaMessage>()))
            .Returns<TestKafkaMessage>((arg) => onMessage(arg));
        this.onException = new Mock<Func<Exception, Task>>();
    }

    private ConsumerObserverCollection<TestKafkaMessage> CreateCollection(CancellationToken cancellationToken)
    {
        var consumerSettings = new KafkaConsumerSettings<TestKafkaMessage>(
            config, new KafkaConsumerHandlers<TestKafkaMessage>(
                    (m, _) => onMessageMoq.Object(m))
                .WithFatalExceptionHandler(onException.Object), null);

        return new ConsumerObserverCollection<TestKafkaMessage>(
            consumerSettings,
            consumerTaskFactoryMoq.Object,
            cancellationToken);
    }

    private static async Task WaitForConditionAsync(
        Func<bool> condition,
        TimeSpan timeout,
        CancellationToken cancellationToken,
        TimeSpan? pollInterval = null)
    {
        pollInterval ??= TimeSpan.FromMilliseconds(10);
        var deadline = DateTime.UtcNow + timeout;

        while (!condition() && DateTime.UtcNow < deadline && !cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(pollInterval.Value, cancellationToken);
        }

        condition().Should().BeTrue($"Condition was not met within {timeout.TotalMilliseconds}ms");
    }

    #endregion

    [Test(Description = "Count возвращает 0, если ничего добавлено не было")]
    public void Count_Returns0_AfterCreation(CancellationToken cancellationToken)
    {
        var collection = CreateCollection(cancellationToken);

        collection.Count.Should().Be(0);
    }

    [Test(Description = "Count возвращает +1, после добавления нового консьюмера")]
    public void Count_Returns1_AfterStartNewCalled(CancellationToken cancellationToken)
    {
        var collection = CreateCollection(cancellationToken);

        collection.StartNew();

        collection.Count.Should().Be(1);
    }

    [Test(Description = "StartNew вызывает consumerTaskFactory при добавлении нового консьюмера")]
    public void StartNew_CallsFactory(CancellationToken cancellationToken)
    {
        var collection = CreateCollection(cancellationToken);

        collection.StartNew();

        consumerTaskFactoryMoq
            .Verify(foo =>
                    foo(
                        It.IsAny<KafkaConsumerSettings<TestKafkaMessage>>(),
                        It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Test(Description = "Stop возвращает false для пустой коллекции")]
    public async Task Stop_ReturnsFalse_IfCollectionIsEmpty(CancellationToken cancellationToken)
    {
        var collection = CreateCollection(cancellationToken);

        var removed = await collection.StopAndRemoveConsumerAsync(TimeSpan.FromMilliseconds(10));

        removed.Should().BeFalse();
    }

    [Test(Description = "Stop возвращает true для коллекции с элементом (без обработки сообщений)")]
    public async Task Stop_ReturnsTrue_IfCollectionHasElement(CancellationToken cancellationToken)
    {
        var collection = CreateCollection(cancellationToken);

        collection.StartNew();
        var removed = await collection.StopAndRemoveConsumerAsync(TimeSpan.FromMilliseconds(10));

        removed.Should().BeTrue();
    }

    [Test(Description = "Stop возвращает false, если в коллекции больше нет элементов")]
    public async Task Stop_ReturnsFalse_IfCalledAfterCollectionIsEmpty(CancellationToken cancellationToken)
    {
        var collection = CreateCollection(cancellationToken);

        collection.StartNew();
        var removeResults = await Task.WhenAll(
            collection.StopAndRemoveConsumerAsync(TimeSpan.FromMilliseconds(10)),
            collection.StopAndRemoveConsumerAsync(TimeSpan.FromMilliseconds(10)),
            collection.StopAndRemoveConsumerAsync(TimeSpan.FromMilliseconds(10)));

        removeResults.Should().BeEquivalentTo(new[] { true, false, false });
    }

    [Test(Description = "Count возвращает N-1 после удаления элемента")]
    public async Task Count_Returns0_AfterRemove(CancellationToken cancellationToken)
    {
        var collection = CreateCollection(cancellationToken);

        collection.StartNew();
        await collection.StopAndRemoveConsumerAsync(TimeSpan.FromMilliseconds(10));

        collection.Count.Should().Be(0);
    }

    [Test(Description = "Stop отзывает токен, переданный в taskFactory")]
    public async Task Stop_CancelsTokenPassedToTaskFactory(CancellationToken cancellationToken)
    {
        using var fakeTaskFactory = new FakeTaskFactory(cancellationToken);
        this.taskFactory = fakeTaskFactory.TaskFactory;
        var collection = CreateCollection(cancellationToken);

        collection.StartNew();
        var registration = fakeTaskFactory.LastRunning;

        registration.CancellationToken.IsCancellationRequested.Should().BeFalse();

        await collection.StopAndRemoveConsumerAsync(freezeTimeout * 2);
        registration.CancellationToken.IsCancellationRequested.Should().BeTrue();
    }

    [Test(Description = "StartNew передаёт onMessage в taskFactory")]
    public async Task StartNew_PassesProperMessageHandler(CancellationToken cancellationToken)
    {
        using var fakeTaskFactory = new FakeTaskFactory(cancellationToken);
        this.taskFactory = fakeTaskFactory.TaskFactory;
        var collection = CreateCollection(cancellationToken);

        collection.StartNew();
        var registration = fakeTaskFactory.LastRunning;

        var message = new TestKafkaMessage();
        await registration.CallOnMessage(message);

        onMessageMoq
            .Verify(
                foo => foo(It.Is<TestKafkaMessage>(m => m == message)),
                Times.Once);
    }

    [Test(Description = "Stop останавливает таску консьюмера, если обработка события не уложилась в maxTimeout")]
    public async Task Stop_CancelsConsumerTaskEvenMessageIsNotProcessedDuringMaxTimeout(CancellationToken cancellationToken)
    {
        using var fakeTaskFactory = new FakeTaskFactory(cancellationToken);
        this.taskFactory = fakeTaskFactory.TaskFactory;
        var collection = CreateCollection(cancellationToken);

        var messageProcessing = new TaskCompletionSource();
        // стартует таску, которая сама не закончится (требует явного завершения)
        this.onMessage = async _ => await messageProcessing.Task.WaitAsync(cancellationToken);

        collection.StartNew();

        var registration = fakeTaskFactory.LastRunning;
        var msg = new TestKafkaMessage();

        // стартуем обработку сообщений (она никогда не завершится, поэтому не ждём результата)
        var _ = registration.CallOnMessage(msg);

        var stopWaitTimeout = TimeSpan.FromMilliseconds(200);
        await collection.StopAndRemoveConsumerAsync(stopWaitTimeout);

        registration.ConsumerTaskResultSource.Task.IsCanceled
            .Should().BeTrue("Таска консьюмера должна быть отменена через отзыв токена");
    }

    [Test(Description = "Stop дожидается окончания обработки текущего сообщения, если оно уже началось")]
    public async Task Stop_WaitsCurrentMessageProcessing(CancellationToken testCancellationToken)
    {
        // Источник для отслеживания завершения консьюмера (используется для синхронизации)
        var consumerCompletionSource = new TaskCompletionSource();
        await using var _ = testCancellationToken.Register(() => consumerCompletionSource.TrySetCanceled());

        // Список для хранения переданных обработчиков и токенов (для доступа к ним из теста)
        var passedArgs = new List<(IKafkaConsumerHandlers<TestKafkaMessage> handlers, CancellationToken token)>();
        var passedArgsLock = new object();

        // Настраиваем taskFactory: имитируем работу консьюмера (бесконечный цикл с задержками)
        // Консьюмер работает до тех пор, пока не будет отменен токен
        this.taskFactory = async (settings, ct) =>
        {
            var tuple = (settings.Handlers, ct);
            lock (passedArgsLock)
            {
                passedArgs.Add(tuple);
            }
            try
            {
                // Создаем связанный токен из токена консьюмера и токена теста
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct, testCancellationToken);

                // Бесконечный цикл работы консьюмера (будет прерван при отмене токена)
                while (!cts.IsCancellationRequested)
                    await Task.Delay(freezeTimeout, cts.Token);
            }
            finally
            {
                // После завершения консьюмера удаляем его из списка и сигнализируем о завершении
                lock (passedArgsLock)
                {
                    passedArgs.Remove(tuple);
                }
                consumerCompletionSource.SetResult();
            }
        };

        // Источник для блокировки обработки сообщения (позволяет контролировать, когда сообщение "обработается")
        var completeMessageProcessing = new TaskCompletionSource();
        var messageHandlingStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        // Настраиваем обработчик сообщений: он блокируется на completeMessageProcessing.Task
        // Это позволяет нам контролировать момент завершения обработки сообщения
        this.onMessage = async _ =>
        {
            messageHandlingStarted.TrySetResult();
            // Блокируемся здесь до тех пор, пока не будет вызван completeMessageProcessing.SetResult()
            await completeMessageProcessing.Task.WaitAsync(testCancellationToken);
        };

        // Создаем коллекцию консьюмеров и запускаем один консьюмер
        var collection = CreateCollection(testCancellationToken);
        collection.StartNew();
        
        // Ждем, пока консьюмер запустится и taskFactory будет вызван
        await WaitForConditionAsync(
            () =>
            {
                lock (passedArgsLock)
                {
                    return passedArgs.Count == 1;
                }
            },
            TimeSpan.FromSeconds(1),
            testCancellationToken);
        (IKafkaConsumerHandlers<TestKafkaMessage> handlers, CancellationToken token) args;
        lock (passedArgsLock)
        {
            args = passedArgs.First();
        }

        // ===== ОСНОВНАЯ ЧАСТЬ ТЕСТА =====
        // Запускаем обработку сообщения в фоне (она заблокируется на completeMessageProcessing.Task)
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        args.handlers.HandleMessage(new TestKafkaMessage(), args.token);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        // Гарантируем, что обработка сообщения реально стартовала (isIdle уже сброшен),
        // иначе Stop может "успеть" выполниться раньше начала обработки на быстрых агентах CI.
        await messageHandlingStarted.Task.WaitAsync(TimeSpan.FromSeconds(1), testCancellationToken);

        // Настраиваем таймауты:
        // - stopMaxTimeout: максимальное время ожидания остановки консьюмера (500ms)
        // - msgProcDoneTimeout: время, через которое мы "завершим" обработку сообщения (166ms)
        //   Это меньше stopMaxTimeout, чтобы проверить, что Stop дождется завершения обработки
        var stopMaxTimeout = freezeTimeout * 14; // 700ms
        
        // Запускаем Stop в фоне (он должен дождаться завершения обработки сообщения)
        var stoppingTask = collection.StopAndRemoveConsumerAsync(stopMaxTimeout);

        // Убеждаемся, что Stop реально начал выполняться (консьюмер уже извлечён из очереди),
        // иначе проверка IsCompleted может быть "пустой" на перегруженном CI.
        await WaitForConditionAsync(
            () => collection.Count == 0,
            TimeSpan.FromSeconds(1),
            testCancellationToken);

        // Даём Stop реально "встать" на ожидание, и проверяем, что он НЕ завершился,
        // пока обработка сообщения заблокирована.
        await Task.Delay(freezeTimeout, testCancellationToken);
        stoppingTask.IsCompleted.Should().BeFalse("Stop должен дождаться завершения обработки сообщения");

        // Разблокируем обработку сообщения, после чего Stop должен завершиться.
        completeMessageProcessing.TrySetResult();
        await stoppingTask;
    }
}
