using FluentAssertions;
using Moedelo.Common.Kafka.Tests.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moq;

namespace Moedelo.Common.Kafka.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[CancelAfter(2000)]
public class ConsumerObserverTests
{
    private Mock<Func<TestKafkaMessage, Task>> onMessageMoq = null!;
    private Func<TestKafkaMessage, Task> onMessage = null!;
    private Mock<Func<Exception, Task>> onExceptionMoq = null!;
    private Mock<Func<IKafkaConsumerHandlers<TestKafkaMessage>, CancellationToken, Task>> consumerTaskFactoryMoq = null!;
    private Func<IKafkaConsumerHandlers<TestKafkaMessage>, CancellationToken, Task> taskFactory = null!;

    [SetUp]
    public void SetUp()
    {
        taskFactory = (_, __) => Task.CompletedTask;
        onMessage = _ => Task.CompletedTask;
        this.consumerTaskFactoryMoq = new Mock<Func<IKafkaConsumerHandlers<TestKafkaMessage>, CancellationToken, Task>>();
        consumerTaskFactoryMoq
            .Setup(foo =>
                foo(
                    It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                    It.IsAny<CancellationToken>()))
            .Returns<IKafkaConsumerHandlers<TestKafkaMessage>, CancellationToken>(
                (arg1, arg2) => taskFactory(arg1, arg2));

        this.onMessageMoq = new Mock<Func<TestKafkaMessage, Task>>();
        onMessageMoq
            .Setup(foo => foo(It.IsAny<TestKafkaMessage>()))
            .Returns<TestKafkaMessage>((arg) => onMessage(arg));
        this.onExceptionMoq = new Mock<Func<Exception, Task>>();
    }

    private ConsumerObserver<TestKafkaMessage> CreateObserver(CancellationToken cancellationToken)
    {
        return new ConsumerObserver<TestKafkaMessage>(
            consumerTaskFactoryMoq.Object,
            onMessageMoq.Object,
            onExceptionMoq.Object,
            cancellationToken);
    }

    [Test(Description = "Находится в idle состоянии сразу после создания (до начала обработки сообщений)")]
    public void IsIdle_ReturnsTrueAfterCreation(CancellationToken cancellationToken)
    {
        var observer = CreateObserver(cancellationToken);

        observer.IsIdle.Should().BeTrue();
    }

    [Test(Description = "Вызывает фабрику при создании")]
    public void CallsFactoryInConstructor(CancellationToken cancellationToken)
    {
        CreateObserver(cancellationToken);

        consumerTaskFactoryMoq
            .Verify(func => func(It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(), It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Test(Description = "Передаёт вызов в обработчик сообщения")]
    public async Task PassesThroughMessageHandler(CancellationToken cancellationToken)
    {
        var handlers = default(IKafkaConsumerHandlers<TestKafkaMessage>);

        this.taskFactory = (handlers1, _) => { handlers = handlers1; return Task.CompletedTask; };

        CreateObserver(cancellationToken);

        handlers.Should().NotBeNull();
        var message = new TestKafkaMessage();
        await handlers!.HandleMessage(message, CancellationToken.None);
        onMessageMoq.Verify(func => func(It.Is<TestKafkaMessage>(m => m == message)), Times.Once);
    }

    [Test(Description = "Передаёт вызов в обработчик ошибок")]
    public async Task PassesThroughOnException(CancellationToken cancellationToken)
    {
        var handlers = default(IKafkaConsumerHandlers<TestKafkaMessage>);

        this.taskFactory = (handlers1, _) => { handlers = handlers1; return Task.CompletedTask; };

        CreateObserver(cancellationToken);

        var exception = new Exception("test");
        await handlers!.OnFatalException(exception);
        onExceptionMoq.Verify(func => func(It.Is<Exception>(m => m == exception)), Times.Once);
    }

    [Test(Description = "Находится в idle состоянии до начала обработки сообщений")]
    public async Task IsIdle_ReturnsFalseRightAfterMessageHandlingStarted(CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource();
        var handlers = default(IKafkaConsumerHandlers<TestKafkaMessage>);

        this.taskFactory = (handlers1, _) => { handlers = handlers1; return tcs.Task; };

        var observer = CreateObserver(cancellationToken);
        handlers.Should().NotBeNull();
        await handlers!.HandleMessage(new TestKafkaMessage(), CancellationToken.None);

        observer.IsIdle.Should().BeFalse();
    }

    [Test(Description = "Возвращается в idle состоянии после вызова OnException")]
    public async Task IsIdle_ReturnsTrueRightAfterOnExceptionCalled(CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource();
        var handlers = default(IKafkaConsumerHandlers<TestKafkaMessage>);

        this.taskFactory = (handlers1, token) => { handlers = handlers1; return tcs.Task; };

        var observer = CreateObserver(cancellationToken);
        handlers.Should().NotBeNull();
        await handlers!.HandleMessage(new TestKafkaMessage(), CancellationToken.None);

        await handlers!.OnFatalException(new Exception());
        observer.IsIdle.Should().BeTrue();
    }

    [Test(Description = "Возвращается в idle состоянии после вызова OnMessageCommitted")]
    public async Task IsIdle_ReturnsTrueRightAfterMessageCommitted(CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource();
        var handlers = default(IKafkaConsumerHandlers<TestKafkaMessage>);

        this.taskFactory = (handlers1, token) => { handlers = handlers1; return tcs.Task; };

        var observer = CreateObserver(cancellationToken);
        handlers.Should().NotBeNull();
        var message = new TestKafkaMessage();
        await handlers!.HandleMessage(message, CancellationToken.None);

        handlers.OnMessageCommitted(message);
        observer.IsIdle.Should().BeTrue();
    }

    [Test(Description = "Возвращается в idle состоянии после вызова OnMessageHandlingFailed")]
    public async Task IsIdle_ReturnsTrueRightAfterOnMessageHandlingFailed(CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource();
        var handlers = default(IKafkaConsumerHandlers<TestKafkaMessage>);

        this.taskFactory = (handlers1, _) => { handlers = handlers1; return tcs.Task; };

        var observer = CreateObserver(cancellationToken);
        handlers.Should().NotBeNull();
        var message = new TestKafkaMessage();
        await handlers!.HandleMessage(message, CancellationToken.None);

        handlers.OnMessageHandlingFailed(message, new Exception());
        observer.IsIdle.Should().BeTrue();
    }

    [Test(Description = "Завершает таску консьюмера по таймауту, даже если текущее событие не обработалось")]
    public async Task Wait_CancelConsumerTaskAfterTimeoutEventMessageIsNotProcessedYet(CancellationToken cancellationToken)
    {
        var settings = new KafkaConsumerConfig(
            "endpoints",
            new KafkaConsumerGroupId("p", "gId"),
            new KafkaTopicName("topic", "kafka.topic"));
        using var fakeTaskFactory = new FakeTaskFactory(cancellationToken);
        this.taskFactory = (f, c) => fakeTaskFactory.TaskFactory(new(settings, f), c);

        var messageProcessing = new TaskCompletionSource();
        // стартует таску, которая сама не закончится (требует явного завершения)
        this.onMessage = async _ => await messageProcessing.Task.WaitAsync(cancellationToken);

        var observer = CreateObserver(cancellationToken);

        var registration = fakeTaskFactory.LastRunning;
        var msg = new TestKafkaMessage();

        // стартуем обработку сообщений (она никогда не завершится, поэтому не ждём результата)
        var _ = registration.CallOnMessage(msg);

        var stopWaitTimeout = TimeSpan.FromMilliseconds(200);
        var smoothStopped = await observer.WaitForMessageHandlingCompleteAndStopAsync(stopWaitTimeout);

        registration.ConsumerTaskResultSource.Task.IsCanceled
            .Should().BeTrue("Таска консьюмера должна быть отменена через отзыв токена");
        smoothStopped.Should().BeFalse("Потому что дождаться завершения обработки сообщения не удалось");
    }

    [Test(Description = "Дожидается окончания обработки текущего сообщения в течение таймаута")]
    public async Task Wait_WaitsForMessageProcessingDoneDuringMaxSmoothStopTimeout(CancellationToken cancellationToken)
    {
        var settings = new KafkaConsumerConfig(
            "endpoints",
            new KafkaConsumerGroupId("p", "gId"),
            new KafkaTopicName("topic", "kafka.topic"));
        using var fakeTaskFactory = new FakeTaskFactory(cancellationToken);
        this.taskFactory = (f, c) => fakeTaskFactory
            .TaskFactory(new(settings, f), c);

        var msgProcessingDuration = TimeSpan.FromMilliseconds(100);
        var messageProcessing = new TaskCompletionSource();
        // стартует таску, которая сама не закончится (требует явного завершения)
        this.onMessage = async _ => await messageProcessing.Task.WaitAsync(msgProcessingDuration, cancellationToken);

        var observer = CreateObserver(cancellationToken);

        var registration = fakeTaskFactory.LastRunning;
        var msg = new TestKafkaMessage();

        // стартуем обработку сообщений (она никогда не завершится, поэтому не ждём результата)
        var _ = registration.CallOnMessage(msg);

        // запускаем долгий процесс остановки консьюмера
        var stopWaitTimeout = msgProcessingDuration * 5;
        var stoppingTask = observer.WaitForMessageHandlingCompleteAndStopAsync(stopWaitTimeout);

        // заканчиваем "обработку сообщения"
        messageProcessing.SetResult();
        // вызываем обработчик того, что сообщение было закоммичено - это должно закрыть цикл обработки сообщения
        registration.Handlers.OnMessageCommitted(msg);

        // дожидаемся завершения запроса на остановку конcьюмера
        var smoothStopped = await stoppingTask;

        smoothStopped.Should().BeTrue("Потому что мы должны были дождаться завершения обарботки сообщения");
    }
}

