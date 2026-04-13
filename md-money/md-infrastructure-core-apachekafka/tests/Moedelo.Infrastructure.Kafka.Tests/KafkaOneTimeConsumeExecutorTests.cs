using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Tests.Models;
using Moq;

namespace Moedelo.Infrastructure.Kafka.Tests;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[CancelAfter(2000)]
public class KafkaOneTimeConsumeExecutorTests
{
    private const string TestTopic = "Test.Topic";

    private static ILogger<KafkaOneTimeConsumeExecutor> logger;
    private static IKafkaConsumerFactorySettings settings;
    private static KafkaConsumerConfig config;

    private KafkaOneTimeConsumeExecutor executor;

    private static ConsumeResultWrapTestingImpl GoodConsumeResult => new ConsumeResultWrapTestingImpl
    {
        TopicPartitionOffset = new(TestTopic, 22, 77),
        MessageKey = "111",
        MessageValue = new TestKafkaMessage().ToJsonString()
    };

    private Mock<IKafkaConsumer> CreateKafkaConsumerMock(
        IConsumeResultWrap? consumeResult,
        bool consumeOnce = false)
    {
        var consumeCount = 0;
        var kafkaConsumerMock = new Mock<IKafkaConsumer>();
        kafkaConsumerMock
            .Setup(c => c.ConsumeAsync(It.IsAny<CancellationToken>()))
            .Callback(() => Interlocked.Increment(ref consumeCount) )
#pragma warning disable CS8604
            .Returns(ValueTask.FromResult(consumeResult!));
#pragma warning restore CS8604
        kafkaConsumerMock
            .SetupGet(c => c.CanConsume)
            .Returns(() => !consumeOnce || consumeCount == 0);
        
        kafkaConsumerMock
            .SetupGet(c => c.ConsumerGroupId)
            .Returns(() => config.GroupId);

        return kafkaConsumerMock;
    }

    [OneTimeSetUp]
    public static void OneTimeSetup()
    {
        var loggerMock = new Mock<ILogger<KafkaOneTimeConsumeExecutor>>();
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
            new KafkaTopicName(TestTopic, $"Real.{TestTopic}"));
    }

    [SetUp]
    public void SetUp()
    {
        executor = new KafkaOneTimeConsumeExecutor(settings, logger);
    }

    [Test(Description = "Вызывает Consume переданного консьюмера")]
    public async Task CallsConsumerConsume(CancellationToken cancellationToken)
    {
        // заглушка для консьюмера - всегда возвращает заданный результат
        var kafkaConsumerMock = CreateKafkaConsumerMock(GoodConsumeResult);
        var handlers = new KafkaConsumerHandlers<TestKafkaMessage>((_,_) => Task.CompletedTask);

        await executor
            .ConsumeAndHandleAsync(config, handlers, kafkaConsumerMock.Object, cancellationToken)
            .ConfigureAwait(false);

        kafkaConsumerMock
            .Verify(foo => foo.ConsumeAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [TestCaseSource(nameof(BadConsumeResultsWithPartitionInfo))]
    [Test(Description = "Не вызывает MessageHandler, если Consume вернул 'плохое' значение или null")]
    public async Task DoesNotCallMessageHandler_IfConsumeReturnsNullOrBrokenResult(IConsumeResultWrap? consumeResult)
    {
        // заглушка для консьюмера - всегда возвращает заданный результат
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        await executor.ConsumeAndHandleAsync(config,
            handlersMock.Object,
            kafkaConsumerMock.Object,
            TestContext.CurrentContext.CancellationToken);

        handlersMock.Verify(foo =>
                foo.HandleMessage(It.IsAny<TestKafkaMessage>(), It.IsAny<CancellationToken>()),
            Times.Never, "Не должен вызываться, поскольку сообщение 'плохое'");
    }

    [TestCaseSource(nameof(BadConsumeResultsWithPartitionInfo))]
    [Test(Description = "Ставит партицию на паузу (вызывает OnMessageHandlingFailed), если Consume вернул 'плохое' значение с данными о секции")]
    public async Task CallsConsumerPause_IfConsumeReturnsBrokenResult(IConsumeResultWrap consumeResult)
    {
        // заглушка для консьюмера - всегда возвращает заданный результат
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult);
        var handlers = new KafkaConsumerHandlers<TestKafkaMessage>((_,_) => Task.CompletedTask);

        await executor.ConsumeAndHandleAsync(config,
            handlers,
            kafkaConsumerMock.Object,
            TestContext.CurrentContext.CancellationToken);

        kafkaConsumerMock.Verify(consumer => consumer
                .OnMessageHandlingFailedAsync(consumeResult.TopicPartitionOffset, consumeResult.MessageValue != null ? consumeResult.MessageKey : null),
            Times.Once);
    }

    [TestCaseSource(nameof(BadConsumeResultsWithPartitionInfo))]
    [Test(Description = "Вызывает обработчик OnMessageHandlingFailed, если Consume вернул 'плохое' значение с данными о секции")]
    public async Task CallsOnMessageHandlingFailed_IfConsumeReturnsBrokenResult(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        await executor.ConsumeAndHandleAsync(config,
            handlersMock.Object,
            kafkaConsumerMock.Object,
            TestContext.CurrentContext.CancellationToken);

        handlersMock.Verify(foo =>
                foo.OnMessageHandlingFailed(It.IsAny<TestKafkaMessage>(), It.IsAny<Exception>()),
            Times.Once);
    }

    [Test(Description = "Выбрасывает исключение, если Consume вернул null")]
    public void ThrowsException_IfConsumeReturnsNull(CancellationToken cancellationToken)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(null);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        Assert.ThrowsAsync<UnexpectedNullConsumeResultException>(async () => await executor
            .ConsumeAndHandleAsync(config, handlersMock.Object, kafkaConsumerMock.Object, cancellationToken));
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Вызывает MessageHandler, если сообщение успешно десериализовано")]
    public async Task CallsMessageHandler_IfMessageIsDeserialized(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        await executor.ConsumeAndHandleAsync(config,
            handlersMock.Object,
            kafkaConsumerMock.Object,
            TestContext.CurrentContext.CancellationToken);

        handlersMock.Verify(foo =>
                foo.HandleMessage(It.IsAny<TestKafkaMessage>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Вызывает OnMessageHandlingFailed, если обработчик сообщения выбросил исключение")]
    public async Task CallsOnMessageHandlingFailed_IfMessageHandlerThrowsException(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();
        handlersMock
            .Setup(h => h.HandleMessage(It.IsAny<TestKafkaMessage>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception("Не удалось обработать сообщение"));

        await executor.ConsumeAndHandleAsync(config,
            handlersMock.Object,
            kafkaConsumerMock.Object,
            TestContext.CurrentContext.CancellationToken);

        handlersMock.Verify(h =>
                h.OnMessageHandlingFailed(It.IsAny<TestKafkaMessage>(), It.IsAny<Exception>()),
            Times.Once);
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Вызывает consumer.Commit, если обработчик сообщения завершился успешно")]
    public async Task CallsConsumerCommit_IfMessageHandlingSuccessfullyDone(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult, consumeOnce: true);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        await executor.ConsumeAndHandleAsync(config,
            handlersMock.Object,
            kafkaConsumerMock.Object,
            TestContext.CurrentContext.CancellationToken);

        kafkaConsumerMock.Verify(consumer => consumer.CommitAsync(consumeResult), Times.Once);
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Вызывает OnMessageCommitted, если обработчик сообщения и consumer.Commit завершились успешно")]
    public async Task CallsOnMessageCommitted_IfMessageHandlingSuccessfullyDone(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult, consumeOnce: true);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        await executor.ConsumeAndHandleAsync(config,
            handlersMock.Object,
            kafkaConsumerMock.Object,
            TestContext.CurrentContext.CancellationToken);

        kafkaConsumerMock.Verify(consumer => consumer.CommitAsync(consumeResult), Times.Once);
        handlersMock.Verify(
            h => h.OnMessageCommitted(It.IsAny<TestKafkaMessage>()),
            Times.Once);
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Не вызывает OnMessageCommitted, если HandleMessage бросает исключение")]
    public async Task DoesNotCallOnMessageCommitted_IfHandleMessageThrowsException(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult, consumeOnce: true);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();
        handlersMock
            .Setup(h => h.HandleMessage(It.IsAny<TestKafkaMessage>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception("Не удалось обработать сообщение"));

        await executor
            .ConsumeAndHandleAsync(config, handlersMock.Object, kafkaConsumerMock.Object, TestContext.CurrentContext.CancellationToken);

        handlersMock.Verify(
            h => h.OnMessageCommitted(It.IsAny<TestKafkaMessage>()),
            Times.Never);
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Вызывает OnMessageHandlingFailed, если HandleMessage бросает исключение")]
    public async Task CallsOnMessageHandlingFailed_IfHandleMessageThrowsException(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult, consumeOnce: true);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();
        handlersMock
            .Setup(h => h.HandleMessage(It.IsAny<TestKafkaMessage>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception("Не удалось обработать сообщение"));

        await executor
            .ConsumeAndHandleAsync(config, handlersMock.Object, kafkaConsumerMock.Object, TestContext.CurrentContext.CancellationToken);

        handlersMock.Verify(
            h => h.OnMessageHandlingFailed(It.IsAny<TestKafkaMessage>(), It.IsAny<Exception>()),
            Times.Once);
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Не вызывает OnMessageCommitted, если consumer.Commit бросает исключение")]
    public async Task DoesNotCallOnMessageCommitted_IfConsumerCommitThrowsException(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult, consumeOnce: true);
        kafkaConsumerMock
            .Setup(c => c.CommitAsync(It.IsAny<IConsumeResultWrap>()))
            .ThrowsAsync(new Exception("Commit failed"));
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        Assert.ThrowsAsync<Exception>(async () => await executor
            .ConsumeAndHandleAsync(config, handlersMock.Object, kafkaConsumerMock.Object, TestContext.CurrentContext.CancellationToken));

        handlersMock.Verify(
            h => h.OnMessageCommitted(It.IsAny<TestKafkaMessage>()),
            Times.Never);
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Вызывает OnMessageHandlingFailed, если consumer.Commit бросает исключение")]
    public async Task CallsOnMessageHandlingFailed_IfConsumerCommitThrowsException(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult, consumeOnce: true);
        kafkaConsumerMock
            .Setup(c => c.CommitAsync(It.IsAny<IConsumeResultWrap>()))
            .ThrowsAsync(new Exception("Commit failed"));
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();

        Assert.ThrowsAsync<Exception>(async () => await executor
            .ConsumeAndHandleAsync(config, handlersMock.Object, kafkaConsumerMock.Object, TestContext.CurrentContext.CancellationToken));

        handlersMock.Verify(
            h => h.OnMessageHandlingFailed(It.IsAny<TestKafkaMessage>(), It.IsAny<Exception>()),
            Times.Once);
    }

    [TestCaseSource(nameof(GoodConsumeResults))]
    [Test(Description = "Ставит партицию на паузу, если обработчик сообщения выбросил исключение")]
    public async Task CallsConsumerPause_IfMessageHandlerThrowsException(IConsumeResultWrap consumeResult)
    {
        var kafkaConsumerMock = CreateKafkaConsumerMock(consumeResult, consumeOnce: true);
        var handlersMock = new Mock<IKafkaConsumerHandlers<TestKafkaMessage>>();
        handlersMock
            .Setup(h => h.HandleMessage(It.IsAny<TestKafkaMessage>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception("Не удалось обработать сообщение"));

        await executor
            .ConsumeAndHandleAsync(config, handlersMock.Object, kafkaConsumerMock.Object, TestContext.CurrentContext.CancellationToken);

        kafkaConsumerMock.Verify(consumer => consumer
            .OnMessageHandlingFailedAsync(consumeResult.TopicPartitionOffset, consumeResult.MessageValue != null ? consumeResult.MessageKey : null),
            Times.Once);
    }

    #region Test Data

    private static readonly object[] BadConsumeResultsWithPartitionInfo =
    [
        // что-то битое, но есть информация о топике
        new object?[]
        {
            new ConsumeResultWrapTestingImpl
            {
                TopicPartitionOffset = new ("topic", 2, 3),
                MessageKey = null,
                MessageValue = null
            }
        },
        // что-то битое, но есть информация о топике
        new object?[]
        {
            new ConsumeResultWrapTestingImpl
            {
                TopicPartitionOffset = new ("topic", 2, 32),
                MessageKey = "<<<",
                MessageValue = ">>>"
            }
        }
    ];

    private static readonly object[] GoodConsumeResults =
    [
        new object?[]
        {
            new ConsumeResultWrapTestingImpl
            {
                TopicPartitionOffset = new ("topic1", 22, 77),
                MessageKey = "111",
                MessageValue = new TestKafkaMessage().ToJsonString()
            }
        }
    ];
    #endregion
}
