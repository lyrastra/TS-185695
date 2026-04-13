using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Tests.Models;
using Moq;

namespace Moedelo.Infrastructure.Kafka.Tests;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[CancelAfter(2000)]
public class KafkaConsumeExecutorTests
{

    private static KafkaConsumerConfig config;

    private KafkaConsumeExecutor executor;
    private Mock<IKafkaOneTimeConsumeExecutor> oneTimeExecutorMock;
    private Mock<IKafkaConsumerEventSourceImplementation> eventSourceMock;
    private Mock<IKafkaConsumer> kafkaConsumerMock;
    private Mock<IKafkaConsumerHandlers<TestKafkaMessage>> handlersMock;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        config = new KafkaConsumerConfig(
            brokerEndpoints: "some.connection.string.to.kafka",
            new KafkaConsumerGroupId("test", "testGroup"),
            new KafkaTopicName("Test.Topic", "Real.Test.Topic"));
    }

    [SetUp]
    public void Setup()
    {
        oneTimeExecutorMock = new Mock<IKafkaOneTimeConsumeExecutor>();
        eventSourceMock = new Mock<IKafkaConsumerEventSourceImplementation>();
        kafkaConsumerMock = new Mock<IKafkaConsumer>();
        handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        kafkaConsumerMock.SetupGet(c => c.ConsumerUid).Returns("test-consumer-uid");
        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(false); // по умолчанию - сразу выход из цикла

        executor = new KafkaConsumeExecutor(oneTimeExecutorMock.Object, eventSourceMock.Object);
    }

    #region Event Tests

    [Test(Description = "Поднимает событие ConsumerStarted при старте")]
    public async Task RaisesConsumerStartedEvent_WhenStarting(CancellationToken cancellationToken)
    {
        await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken);

        eventSourceMock.Verify(
            es => es.RaiseConsumerStartedEventAsync(
                It.IsAny<string>(),
                It.Is<KafkaConsumerConfig>(c => c == config),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test(Description = "Поднимает событие ConsumerStopped при выходе из цикла (CanConsume = false)")]
    public async Task RaisesConsumerStoppedEvent_WhenCanConsumeIsFalse(CancellationToken cancellationToken)
    {
        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(false);

        await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken);

        eventSourceMock.Verify(
            es => es.RaiseConsumerStoppedEventAsync(
                It.IsAny<string>(),
                It.Is<KafkaConsumerConfig>(c => c == config),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test(Description = "Поднимает событие ConsumerStopped даже если oneTimeExecutor бросил исключение")]
    public async Task RaisesConsumerStoppedEvent_WhenExceptionThrown(CancellationToken cancellationToken)
    {
        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(true);
        oneTimeExecutorMock
            .Setup(e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        try
        {
            await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken);
        }
        catch
        {
            // ожидаем исключение
        }

        eventSourceMock.Verify(
            es => es.RaiseConsumerStoppedEventAsync(
                It.IsAny<string>(),
                It.Is<KafkaConsumerConfig>(c => c == config),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test(Description = "Поднимает PartitionSetOnPause, если результат обработки неуспешный")]
    public async Task RaisesPartitionSetOnPauseEvent_WhenHandleResultIsNotSuccessful(CancellationToken cancellationToken)
    {
        var consumeCount = 0;
        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(() => consumeCount++ < 1);

        var failedResult = CreateHandlingResult(success: false);
        oneTimeExecutorMock
            .Setup(e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(failedResult);

        await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken);

        eventSourceMock.Verify(
            es => es.RaisePartitionSetOnPauseEventAsync(
                It.IsAny<string>(),
                It.Is<MessageHandlingResultBase>(r => r == failedResult),
                It.Is<KafkaConsumerConfig>(c => c == config),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test(Description = "Не поднимает PartitionSetOnPause, если результат обработки успешный")]
    public async Task DoesNotRaisePartitionSetOnPauseEvent_WhenHandleResultIsSuccessful(CancellationToken cancellationToken)
    {
        var consumeCount = 0;
        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(() => consumeCount++ < 1);

        var successResult = CreateHandlingResult(success: true);
        oneTimeExecutorMock
            .Setup(e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(successResult);

        await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken);

        eventSourceMock.Verify(
            es => es.RaisePartitionSetOnPauseEventAsync(
                It.IsAny<string>(),
                It.IsAny<MessageHandlingResultBase>(),
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    #endregion

    #region Loop Behavior Tests

    [Test(Description = "Вызывает ConsumeAndHandle, пока CanConsume = true")]
    public async Task CallsConsumeAndHandle_WhileCanConsumeIsTrue(CancellationToken cancellationToken)
    {
        var consumeCount = 0;
        const int expectedIterations = 3;
        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(() => consumeCount++ < expectedIterations);

        oneTimeExecutorMock
            .Setup(e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateHandlingResult(success: true));

        await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken);

        oneTimeExecutorMock.Verify(
            e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(expectedIterations));
    }

    [Test(Description = "Останавливает цикл, когда CanConsume становится false")]
    public async Task StopsLoop_WhenCanConsumeIsFalse(CancellationToken cancellationToken)
    {
        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(false);

        await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken);

        oneTimeExecutorMock.Verify(
            e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Test(Description = "Продолжает цикл после неуспешной обработки (партиция на паузе, но консьюмер жив)")]
    public async Task ContinuesLoop_AfterFailedMessageHandling(CancellationToken cancellationToken)
    {
        // Сценарий: топик с 2 партициями, одна встала на паузу, но консьюмер должен продолжать
        // читать из второй партиции. Kafka-библиотека сама разберётся откуда читать.
        var consumeCount = 0;
        const int totalIterations = 3;
        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(() => consumeCount < totalIterations);

        oneTimeExecutorMock
            .Setup(e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                consumeCount++;
                // Первое сообщение — ошибка (партиция 0 встанет на паузу)
                // Остальные — успех (читаем из партиции 1)
                return CreateHandlingResult(success: consumeCount > 1);
            });

        await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken);

        // Консьюмер должен продолжить работу после первой ошибки и обработать все 3 итерации
        oneTimeExecutorMock.Verify(
            e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(totalIterations),
            "Консьюмер не должен останавливаться из-за паузы одной партиции");
    }

    [Test(Description = "Останавливает цикл при отмене CancellationToken")]
    public async Task StopsLoop_WhenCancellationRequested()
    {
        using var cts = new CancellationTokenSource();
        var consumeCount = 0;

        kafkaConsumerMock.SetupGet(c => c.CanConsume).Returns(true);
        oneTimeExecutorMock
            .Setup(e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()))
            .Callback(() =>
            {
                if (++consumeCount >= 2) cts.Cancel();
            })
            .ReturnsAsync(CreateHandlingResult(success: true));

        await executor.RunConsumeLoopAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cts.Token);

        oneTimeExecutorMock.Verify(
            e => e.ConsumeAndHandleAsync(
                It.IsAny<KafkaConsumerConfig>(),
                It.IsAny<IKafkaConsumerHandlers<TestKafkaMessage>>(),
                It.IsAny<IKafkaConsumer>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(2));
    }

    #endregion

    #region Property Tests

    [Test(Description = "EventSource возвращает инжектированный eventSource")]
    public void EventSource_ReturnsInjectedConsumerEventSource()
    {
        Assert.That(executor.EventSource, Is.SameAs(eventSourceMock.Object));
    }

    #endregion

    #region Helpers

    private static MessageHandlingResultBase CreateHandlingResult(bool success)
    {
        var consumeResultMock = new Mock<IConsumeResultWrap>();
        consumeResultMock.SetupGet(c => c.TopicPartitionOffset).Returns(new KafkaTopicPartitionOffset("topic", 0, 0));

        return new TestMessageHandlingResult(consumeResultMock.Object, success, null, TimeSpan.Zero);
    }

    private record TestMessageHandlingResult(
        IConsumeResultWrap ConsumeResult,
        bool Success,
        Exception? Exception,
        TimeSpan Duration) : MessageHandlingResultBase(ConsumeResult, Success, Exception, Duration);

    #endregion
}
