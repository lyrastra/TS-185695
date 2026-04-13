using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Tests.Models;
using Moq;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Moedelo.Infrastructure.Kafka.Tests;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[CancelAfter(2000)]
public class KafkaConsumerStarterTests
{
    private static ILogger<KafkaConsumerStarter> logger;
    private static IKafkaConsumerFactorySettings settings;
    private static KafkaConsumerConfig config;

    private KafkaConsumerStarter starter;
    private Mock<IDefaultKafkaConsumerFactory> rawConsumerFactoryMock;
    private Mock<IKafkaConsumerHandlers<TestKafkaMessage>> handlersMock;
    private Mock<IKafkaConsumeExecutor> consumeExecutorMock;
    private KafkaConsumerSettings<TestKafkaMessage> consumerSettings;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        var loggerMock = new Mock<ILogger<KafkaConsumerStarter>>();
        loggerMock
            .Setup(obj => obj.IsEnabled(It.IsAny<LogLevel>()))
            .Returns(false);

        var settingsMock = new Mock<IKafkaConsumerFactorySettings>();
        settingsMock
            .SetupGet(s => s.PauseBeforeFirstConsumerStart)
            .Returns(TimeSpan.FromMilliseconds(1));

        logger = loggerMock.Object;
        settings = settingsMock.Object;
        config = new KafkaConsumerConfig(
            brokerEndpoints: "some.connection.string.to.kafka",
            new KafkaConsumerGroupId("test", "testGroup"),
            new KafkaTopicName("Test.Topic", "Real.Test.Topic"));
    }

    [SetUp]
    public void Setup()
    {
        handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();
        rawConsumerFactoryMock = new Mock<IDefaultKafkaConsumerFactory>();
        rawConsumerFactoryMock
            .Setup(f => f.CreateAsync(
                It.IsAny<KafkaConsumerConfig>(), It.IsAny<IKafkaConsumerFactorySettings>(),
                It.IsAny<ILogger>()))
            .ReturnsAsync(() => new Mock<IKafkaConsumer>().Object);
        consumeExecutorMock = new Mock<IKafkaConsumeExecutor>();
        consumeExecutorMock
            .Setup(executor => executor.RunConsumeLoopAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        starter = new KafkaConsumerStarter(settings,
            kafkaConsumerFactories: [],
            logger,
            rawConsumerFactoryMock.Object,
            consumeExecutorMock.Object);
        
        consumerSettings = new KafkaConsumerSettings<TestKafkaMessage>(config, handlersMock.Object);
    }

    [TearDown]
    public async Task TearDown()
    {
        await starter.DisposeAsync();
    }

    [Test(Description = "Использует переданную фабрику для создания консьюмеров")]
    public async Task UsesFactoryToCreateRawConsumer(CancellationToken cancellationToken)
    {
        var factoryCalled = new TaskCompletionSource();
        rawConsumerFactoryMock
            .Setup(f => f.CreateAsync(
                It.IsAny<KafkaConsumerConfig>(), It.IsAny<IKafkaConsumerFactorySettings>(),
                It.IsAny<ILogger>()))
            .Callback(() => factoryCalled.TrySetResult())
            .ReturnsAsync(() => new Mock<IKafkaConsumer>().Object);

        _ = starter.ListenAsync(consumerSettings, cancellationToken);
        await factoryCalled.Task.WaitAsync(cancellationToken);

        rawConsumerFactoryMock
            .Verify(foo =>
                    foo.CreateAsync(
                        It.Is<KafkaConsumerConfig>(c => c == config),
                        It.Is<IKafkaConsumerFactorySettings>(s => s == settings),
                        It.IsAny<ILogger>()),
                Times.Once);
    }

    [Test(Description = "Использует executor для запуска consume loop")]
    public async Task UsesConsumerExecutor(CancellationToken cancellationToken)
    {
        var executorCalled = new TaskCompletionSource();
        consumeExecutorMock
            .Setup(executor => executor.RunConsumeLoopAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(), It.IsAny<CancellationToken>()))
            .Callback(() => executorCalled.TrySetResult())
            .Returns(Task.CompletedTask);

        _ = starter.ListenAsync(consumerSettings, cancellationToken);
        await executorCalled.Task.WaitAsync(cancellationToken);

        consumeExecutorMock
            .Verify(foo =>
                    foo.RunConsumeLoopAsync(
                        It.Is<KafkaConsumerConfig>(c => c == config),
                        It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                        It.IsAny<IKafkaConsumer>(),
                        It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Test(Description = "Вызывает OnFatalException, если фабрика бросает исключение")]
    public async Task CallsOnFatalException_IfFactoryThrows(CancellationToken cancellationToken)
    {
        var onFatalExceptionCalled = new TaskCompletionSource();
        rawConsumerFactoryMock
            .Setup(f => f.CreateAsync(
                It.IsAny<KafkaConsumerConfig>(), It.IsAny<IKafkaConsumerFactorySettings>(),
                It.IsAny<ILogger>()))
            .ThrowsAsync(new Exception("Фабрика бросила исключение"));
        handlersMock
            .Setup(h => h.OnFatalException(It.IsAny<Exception>()))
            .Callback(() => onFatalExceptionCalled.TrySetResult())
            .Returns(Task.CompletedTask);

        _ = starter.ListenAsync(consumerSettings, cancellationToken);
        await onFatalExceptionCalled.Task.WaitAsync(cancellationToken);

        handlersMock.Verify(h => h.OnFatalException(It.IsAny<Exception>()), Times.Once);
    }

    [Test(Description = "Вызывает OnFatalException, если фабрика вернула null")]
    public async Task CallsOnFatalException_IfFactoryReturnsNull(CancellationToken cancellationToken)
    {
        var onFatalExceptionCalled = new TaskCompletionSource();
        rawConsumerFactoryMock
            .Setup(f => f.CreateAsync(
                It.IsAny<KafkaConsumerConfig>(), It.IsAny<IKafkaConsumerFactorySettings>(),
                It.IsAny<ILogger>()))
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type
            .Returns(Task.FromResult<IKafkaConsumer>(null!));
#pragma warning restore CS8619
        handlersMock
            .Setup(h => h.OnFatalException(It.IsAny<Exception>()))
            .Callback(() => onFatalExceptionCalled.TrySetResult())
            .Returns(Task.CompletedTask);

        _ = starter.ListenAsync(consumerSettings, cancellationToken);
        await onFatalExceptionCalled.Task.WaitAsync(cancellationToken);

        handlersMock.Verify(h => h.OnFatalException(It.IsAny<Exception>()), Times.Once);
    }

    [Test(Description = "Вызывает OnFatalException, если executor бросает исключение")]
    public async Task CallsOnFatalException_IfExecutorThrows(CancellationToken cancellationToken)
    {
        var onFatalExceptionCalled = new TaskCompletionSource();
        consumeExecutorMock
            .Setup(executor => executor.RunConsumeLoopAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Executor бросил исключение"));
        handlersMock
            .Setup(h => h.OnFatalException(It.IsAny<Exception>()))
            .Callback(() => onFatalExceptionCalled.TrySetResult())
            .Returns(Task.CompletedTask);

        _ = starter.ListenAsync(consumerSettings, cancellationToken);
        await onFatalExceptionCalled.Task.WaitAsync(cancellationToken);

        handlersMock.Verify(h => h.OnFatalException(It.IsAny<Exception>()), Times.Once);
    }

    [Test(Description = "Выбрасывает ObjectDisposedException при вызове ListenAsync после Dispose")]
    public async Task ThrowsObjectDisposedException_WhenCalledAfterDispose(CancellationToken cancellationToken)
    {
        await starter.DisposeAsync();

        Assert.ThrowsAsync<ObjectDisposedException>(
            () => starter.ListenAsync(consumerSettings, cancellationToken));
    }

    [Test(Description = "DisposeAsync останавливает запущенные консьюмеры через linked CancellationToken")]
    public async Task DisposeAsync_StopsRunningConsumers(CancellationToken cancellationToken)
    {
        var consumerLoopStarted = new TaskCompletionSource();
        CancellationToken capturedToken = default;

        consumeExecutorMock
            .Setup(executor => executor.RunConsumeLoopAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()))
            .Callback<KafkaConsumerConfig, IKafkaConsumerHandlers<TestKafkaMessage>, IKafkaConsumer, CancellationToken>(
                (_, _, _, token) =>
                {
                    capturedToken = token;
                    consumerLoopStarted.SetResult();
                })
            .Returns(async () =>
            {
                // Имитируем долгоживущий consume loop
                try
                {
                    await Task.Delay(Timeout.Infinite, capturedToken);
                }
                catch (OperationCanceledException)
                {
                    // Ожидаемая отмена
                }
            });

        _ = starter.ListenAsync(consumerSettings, cancellationToken);
        
        // Ждём, пока consume loop стартанёт
        await consumerLoopStarted.Task.WaitAsync(cancellationToken);
        
        Assert.That(capturedToken.IsCancellationRequested, Is.False, 
            "Token не должен быть отменён до вызова DisposeAsync");

        // Act
        await starter.DisposeAsync();

        // Assert
        Assert.That(capturedToken.IsCancellationRequested, Is.True, 
            "Token должен быть отменён после DisposeAsync");
    }
}
