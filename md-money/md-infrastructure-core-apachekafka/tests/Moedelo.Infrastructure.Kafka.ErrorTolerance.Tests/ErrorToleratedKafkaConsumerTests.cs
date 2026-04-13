using Confluent.Kafka;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Extensions;
using Moq;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
[CancelAfter(2000)]
public class ErrorToleratedKafkaConsumerTests
{
    private IKafkaConsumer consumer = null!;
    private ILogger logger = null!;
    private Mock<IConfluentConsumerFactory> consumerFactoryMoq = null!;
    private Mock<IConsumer<string, string>> confluentConsumerMoq = null!;
    private KafkaConsumerConfig config = null!;
    private Mock<IKafkaConsumerStateMemory> stateMemoryMoq = null!;
    private ConsumeResultWrapImpl consumeResultWrap = null!;
    private ConsumeResult<string, string> consumeResult = null!;
    private Mock<IErrorToleratedKafkaConsumerOptions> optionsMoq = null!;
    private CancellationTokenSource testGuardCancellation = null!;
    private KafkaTopicPartitionOffset kafkaTopicPartitionOffset;

    private KafkaTopicPartition KafkaTopicPartition =>
        new(kafkaTopicPartitionOffset.Topic, kafkaTopicPartitionOffset.Partition);

    private string messageKey = null!;
    private string messageValue = null!;
    private Mock<IPartitionStateEstimator> partitionStateEstimatorMoq = null!;

    [SetUp]
    public void SetUp()
    {
        logger = new Mock<ILogger>().Object;
        config = new KafkaConsumerConfig(
            brokerEndpoints: "connection-to-kafka",
            new KafkaConsumerGroupId("test", "groupId"),
            new KafkaTopicName("Topic.Name", "test.Topic.Name"));
        config.OptionalParams.MaxPollIntervalMs = 1234;

        consumerFactoryMoq = new Mock<IConfluentConsumerFactory>();
        consumerFactoryMoq.Setup(factory => factory.Create(
                It.IsAny<ConsumerConfig>(),
                It.IsAny<ConfluentConsumerEventHandlers>()))
            .Returns<ConsumerConfig, ConfluentConsumerEventHandlers>((_, _) => confluentConsumerMoq.Object);
        messageKey = $"MessageKey{TestContext.CurrentContext.Random.Next(100, 120)}";
        messageValue = $"{{value:{TestContext.CurrentContext.Random.Next(100, 120)}}}";
        kafkaTopicPartitionOffset = new KafkaTopicPartitionOffset(
            config.TopicName.NameInKafka,
            TestContext.CurrentContext.Random.Next(1, 12),
            TestContext.CurrentContext.Random.NextLong(1000, 120000));

        consumeResult = new ConsumeResult<string, string>
        {
            Topic = kafkaTopicPartitionOffset.Topic,
            Partition = kafkaTopicPartitionOffset.Partition,
            Offset = kafkaTopicPartitionOffset.Offset,
            Message = new Message<string, string>
            {
                Key = messageKey,
                Value = messageValue
            }
        };
        consumeResultWrap = new ConsumeResultWrapImpl(consumeResult);

        confluentConsumerMoq = new Mock<IConsumer<string, string>>();
        confluentConsumerMoq
            .Setup(c => c.Consume(It.IsAny<CancellationToken>()))
            .Returns<CancellationToken>(_ => consumeResult);

        optionsMoq = new Mock<IErrorToleratedKafkaConsumerOptions>();
        optionsMoq.SetupGet(opt => opt.MaxOffsetMapDepth).Returns(1024);
        optionsMoq.SetupGet(opt => opt.MaxPausedMessageKeys).Returns(24);
        optionsMoq.SetupGet(opt => opt.ErrorToleranceTimeSpan).Returns(TimeSpan.FromSeconds(60));

        stateMemoryMoq = new Mock<IKafkaConsumerStateMemory>();
        stateMemoryMoq.Setup(mem => mem.GetPartitionStateAsync(KafkaTopicPartition))
            .Returns<KafkaTopicPartition>(_ => ValueTask.FromResult(
                new PartitionMemoryState(
                    kafkaTopicPartitionOffset.Partition,
                    CommittedOffset: default,
                    OffsetMapDepth: 0,
                    UniqueSkippedMessageKeysCount: 0)));
        partitionStateEstimatorMoq = new Mock<IPartitionStateEstimator>();
        partitionStateEstimatorMoq
            .Setup(est => est.EstimateMemoryStatus(
                It.IsAny<IErrorToleratedKafkaConsumerOptions>(),
                It.IsAny<PartitionMemoryState>(),
                It.IsAny<long>()))
            .Returns<IErrorToleratedKafkaConsumerOptions, PartitionMemoryState, long>((_, _, _) =>
                new PartitionMemoryEstimation(false, string.Empty));

        consumer = new ErrorToleratedKafkaConsumer(config,
            optionsMoq.Object,
            stateMemoryMoq.Object,
            partitionStateEstimatorMoq.Object,
            consumerFactoryMoq.Object,
            logger);
        testGuardCancellation = new CancellationTokenSource();
    }

    [TearDown]
    public void TearDown()
    {
        testGuardCancellation.Dispose();
    }

    private void SetupCancelAfterOneConsume()
    {
        confluentConsumerMoq
            .Setup(c => c.Consume(It.IsAny<CancellationToken>()))
            .Returns<CancellationToken>(_ =>
            {
                testGuardCancellation.Cancel();
                return consumeResult;
            });
    }

    private CancellationToken GetLinkedToken(CancellationToken testCancellation) =>
        CancellationTokenSource.CreateLinkedTokenSource(testCancellation, testGuardCancellation.Token).Token;

    [Test]
    public void MaxPollIntervalMs_ReturnsValueFromConnectionSettings()
    {
        consumer.MaxPollIntervalMs.Should().Be(config.OptionalParams.MaxPollIntervalMs);
    }

    [Test]
    public void Dispose_CallsDisposeOfConfluentConsumer()
    {
        consumer.Dispose();

        confluentConsumerMoq.Verify(moq => moq.Close(), Times.Once);
        confluentConsumerMoq.Verify(moq => moq.Dispose(), Times.Once);
    }

    [Test]
    public void Subscribe_CallsSubscribeOfConfluentConsumer()
    {
        var topicName = config.TopicName.NameInKafka;
        consumer.Subscribe(topicName);

        confluentConsumerMoq.Verify(
            moq => moq.Subscribe(It.Is<string>(v => v == topicName)),
            Times.Once);
    }

    [Test]
    public async ValueTask CommitAsync_CallsMemoryHasSkippedMessagesBefore()
    {
        var offset = new KafkaTopicPartitionOffset(config.TopicName.NameInKafka, 1, 1234);
        consumeResult.Topic = offset.Topic;
        consumeResult.Partition = offset.Partition;
        consumeResult.Offset = offset.Offset;

        await consumer.CommitAsync(consumeResultWrap);

        stateMemoryMoq.Verify(
            moq => moq.HasSkippedMessagesBeforeAsync(It.Is<KafkaTopicPartitionOffset>(v => v.Equals(offset))),
            Times.Once);
    }

    [Test]
    public async ValueTask CommitAsync_CallsRawCommit_IfThereIsNoSkippedMessage()
    {
        const bool hasSkippedMessages = false;

        stateMemoryMoq
            .Setup(mem => mem.HasSkippedMessagesBeforeAsync(It.IsAny<KafkaTopicPartitionOffset>()))
            .Returns(ValueTask.FromResult(hasSkippedMessages));

        await consumer.CommitAsync(consumeResultWrap);

        confluentConsumerMoq.Verify(moq => moq.Commit(consumeResult), Times.Once);
    }

    [Test]
    public async ValueTask CommitAsync_CallsMemoryCommit_IfThereIsNoSkippedMessage()
    {
        const bool hasSkippedMessages = false;

        stateMemoryMoq
            .Setup(mem => mem.HasSkippedMessagesBeforeAsync(It.IsAny<KafkaTopicPartitionOffset>()))
            .Returns(ValueTask.FromResult(hasSkippedMessages));

        var offset = new KafkaTopicPartitionOffset(consumeResult.Topic, consumeResult.Partition, consumeResult.Offset);

        await consumer.CommitAsync(consumeResultWrap);

        stateMemoryMoq.Verify(
            moq => moq.SetCommittedOffsetAsync(
                It.Is<KafkaTopicPartitionOffset>(v => v.Equals(offset)),
                It.Is<string>(v => v == consumeResult.Message.Key)),
            Times.Once);
    }

    [Test]
    public async ValueTask CommitAsync_DoesNotCallRawCommit_IfThereIsSomeSkippedMessage()
    {
        const bool hasSkippedMessages = true;

        stateMemoryMoq
            .Setup(mem => mem.HasSkippedMessagesBeforeAsync(It.IsAny<KafkaTopicPartitionOffset>()))
            .Returns(ValueTask.FromResult(hasSkippedMessages));

        await consumer.CommitAsync(consumeResultWrap);

        confluentConsumerMoq.Verify(
            moq => moq.Commit(
                It.Is<ConsumeResult<string, string>>(v => v == consumeResult)),
            Times.Never);
    }

    [Test]
    public async ValueTask CommitAsync_DoesNotCallMemoryCommit_IfThereIsSomeSkippedMessage()
    {
        const bool hasSkippedMessages = true;

        stateMemoryMoq
            .Setup(mem => mem.HasSkippedMessagesBeforeAsync(It.IsAny<KafkaTopicPartitionOffset>()))
            .Returns(ValueTask.FromResult(hasSkippedMessages));

        await consumer.CommitAsync(consumeResultWrap);

        stateMemoryMoq.Verify(
            moq => moq.SetCommittedOffsetAsync(
                It.IsAny<KafkaTopicPartitionOffset>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Test]
    public async ValueTask CommitAsync_CallsMemoryMarkMessageAsProcessedAsync_IfThereIsSomeSkippedMessage()
    {
        const bool hasSkippedMessages = true;

        stateMemoryMoq
            .Setup(mem => mem.HasSkippedMessagesBeforeAsync(It.IsAny<KafkaTopicPartitionOffset>()))
            .Returns(ValueTask.FromResult(hasSkippedMessages));

        await consumer.CommitAsync(consumeResultWrap);

        var offset = new KafkaTopicPartitionOffset(consumeResult.Topic, consumeResult.Partition, consumeResult.Offset);
        stateMemoryMoq.Verify(
            moq => moq.MarkMessageAsProcessedAsync(
                It.Is<KafkaTopicPartitionOffset>(v => v.Equals(offset)),
                It.Is<string>(v => v == consumeResult.Message.Key)),
            Times.Once);
    }

    [Test]
    public async ValueTask PauseAsync_CallsMemoryMarkMessageAsSkippedAsync()
    {
        var messageKey = consumeResult.Message.Key;
        var offset = new KafkaTopicPartitionOffset(consumeResult.Topic, consumeResult.Partition, consumeResult.Offset);

        await consumer.OnMessageHandlingFailedAsync(offset, messageKey);

        stateMemoryMoq.Verify(
            moq => moq.MarkMessageAsSkippedAsync(
                It.Is<KafkaTopicPartitionOffset>(v => v.Equals(offset)),
                It.Is<string>(v => v == messageKey)),
            Times.Once);
    }

    #region ConsumeAsync

    [Test]
    public async ValueTask ConsumeAsync_CallsRawConsumerConsumeMethod(CancellationToken cancellationToken)
    {
        await consumer.ConsumeAsync(GetLinkedToken(cancellationToken))
            .IgnoreException<OperationCanceledException, IConsumeResultWrap>();

        confluentConsumerMoq.Verify(
            rawConsumer => rawConsumer.Consume(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async ValueTask ConsumeAsync_CallsMemoryMessageIsConsumed(CancellationToken cancellationToken)
    {
        SetupCancelAfterOneConsume();
        var linkedToken = GetLinkedToken(cancellationToken);

        await consumer.ConsumeAsync(linkedToken);

        // то текущее смещение в состоянии должно быть актуализировано
        stateMemoryMoq.Verify(memory => memory.MessageIsConsumedAsync(
            consumeResultWrap.TopicPartitionOffset, messageKey, linkedToken), Times.Once);
    }

    [Test]
    public async ValueTask ConsumeAsync_DoesNotCallConsumeAfterTokenIsCancelled(CancellationToken cancellationToken)
    {
        SetupCancelAfterOneConsume();

        await consumer.ConsumeAsync(GetLinkedToken(cancellationToken))
            .IgnoreException<OperationCanceledException, IConsumeResultWrap>();

        confluentConsumerMoq.Verify(
            rawConsumer => rawConsumer.Consume(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async ValueTask ConsumeAsync_ReturnsResult_IfThereIsNoReasonToSkipOrMarkAsProcessed(CancellationToken cancellationToken)
    {
        stateMemoryMoq.Setup(mem =>
                mem.HasAnySkippedMessageWithKeyAsync(KafkaTopicPartition, messageKey))
            .Returns(ValueTask.FromResult(false));
        stateMemoryMoq.Setup(mem =>
                mem.IsMessageAlreadyProcessedAsync(kafkaTopicPartitionOffset))
            .Returns(ValueTask.FromResult(false));

        var result = await consumer.ConsumeAsync(GetLinkedToken(cancellationToken));

        result.MessageKey.Should().Be(messageKey);
        result.MessageValue.Should().Be(messageValue);
        result.TopicPartitionOffset.Should().Be(kafkaTopicPartitionOffset);
    }

    [Test]
    public async ValueTask ConsumeAsync_SkipsMessage_IfSomeMessageWithSuchKeyIsAlreadySkipped(CancellationToken cancellationToken)
    {
        SetupCancelAfterOneConsume();

        stateMemoryMoq.Setup(mem =>
                mem.HasAnySkippedMessageWithKeyAsync(KafkaTopicPartition, messageKey))
            .Returns(ValueTask.FromResult(true));

        await consumer.ConsumeAsync(GetLinkedToken(cancellationToken))
            .IgnoreException<OperationCanceledException, IConsumeResultWrap>();

        stateMemoryMoq.Verify(mem =>
                mem.MarkMessageAsSkippedAsync(kafkaTopicPartitionOffset, messageKey),
            Times.Once);
    }

    [Test]
    public async ValueTask ConsumeAsync_CommitsOffset_IfMessageIsAlreadyProcessedAndNoOneMessageIsSkipped(CancellationToken cancellationToken)
    {
        SetupCancelAfterOneConsume();

        stateMemoryMoq.Setup(mem =>
                mem.HasAnySkippedMessageWithKeyAsync(KafkaTopicPartition, messageKey))
            .Returns(ValueTask.FromResult(false));
        stateMemoryMoq.Setup(mem =>
                mem.IsMessageAlreadyProcessedAsync(kafkaTopicPartitionOffset))
            .Returns(ValueTask.FromResult(true));
        stateMemoryMoq.Setup(mem =>
                mem.HasSkippedMessagesBeforeAsync(kafkaTopicPartitionOffset))
            .Returns(ValueTask.FromResult(false));

        await consumer.ConsumeAsync(GetLinkedToken(cancellationToken))
            .IgnoreException<OperationCanceledException, IConsumeResultWrap>();

        confluentConsumerMoq.Verify(moq => moq.Commit(consumeResult), Times.Once);
    }

    [Test]
    public async ValueTask
        ConsumeAsync_SavesNewCommittedOffsetIntoMemory_IfMessageIsAlreadyProcessedAndNoOneMessageIsSkipped(CancellationToken cancellationToken)
    {
        // Настраиваем мок консьюмера так, чтобы после первого вызова Consume() токен отменялся
        // Это позволяет тесту завершиться после обработки одного сообщения
        SetupCancelAfterOneConsume();

        // Настраиваем моки для проверки сценария, когда сообщение уже было обработано ранее:
        
        // 1. Проверяем, что нет пропущенных сообщений с таким же ключом
        //    Это означает, что сообщение можно обработать (не нужно пропускать из-за цепочки пропусков)
        stateMemoryMoq.Setup(mem =>
                mem.HasAnySkippedMessageWithKeyAsync(KafkaTopicPartition, messageKey))
            .Returns(ValueTask.FromResult(false));
        
        // 2. Проверяем, что сообщение уже было обработано ранее
        //    Это основной сценарий теста - сообщение уже обработано, но нужно обновить committed offset
        stateMemoryMoq.Setup(mem =>
                mem.IsMessageAlreadyProcessedAsync(kafkaTopicPartitionOffset))
            .Returns(ValueTask.FromResult(true));
        
        // 3. Проверяем, что нет пропущенных сообщений перед текущим offset
        //    Если есть пропущенные сообщения, то committed offset не обновляется (логика в CommitAsync)
        stateMemoryMoq.Setup(mem =>
                mem.HasSkippedMessagesBeforeAsync(kafkaTopicPartitionOffset))
            .Returns(ValueTask.FromResult(false));

        // Создаем источник для ожидания фактического вызова SetCommittedOffsetAsync
        // Это позволяет дождаться реального выполнения асинхронной операции вместо задержки по времени
        var setCommittedOffsetCalled = new TaskCompletionSource();
        
        // Настраиваем мок SetCommittedOffsetAsync так, чтобы при вызове мы сигнализировали о завершении
        // Используем Callback для установки TaskCompletionSource, что гарантирует ожидание фактического вызова
        stateMemoryMoq.Setup(mem =>
                mem.SetCommittedOffsetAsync(kafkaTopicPartitionOffset, messageKey))
            .Returns(ValueTask.CompletedTask)
            .Callback(() => setCommittedOffsetCalled.TrySetResult());

        // Вызываем ConsumeAsync, который должен:
        // 1. Получить сообщение через rawConsumer.Consume()
        // 2. Вызвать MessageIsConsumedAsync для обновления состояния
        // 3. Обнаружить, что сообщение уже обработано (IsMessageAlreadyProcessedAsync = true)
        // 4. Вызвать DoCommitAsync, который:
        //    - Вызовет rawConsumer.Commit() для коммита в Kafka
        //    - Вызовет SetCommittedOffsetAsync для сохранения offset в памяти (асинхронно через ConfigureAwait(false))
        // 5. Завершиться из-за отмены токена (через SetupCancelAfterOneConsume)
        await consumer.ConsumeAsync(GetLinkedToken(cancellationToken))
            .IgnoreException<OperationCanceledException, IConsumeResultWrap>();

        // Дожидаемся фактического вызова SetCommittedOffsetAsync
        // Это гарантирует, что асинхронная операция завершилась, независимо от производительности CI серверов
        // Используем cancellationToken для контроля времени ожидания (управляется через [CancelAfter] атрибут)
        await setCommittedOffsetCalled.Task.WaitAsync(cancellationToken);

        // Проверяем, что SetCommittedOffsetAsync был вызван ровно один раз
        // Это подтверждает, что committed offset был сохранен в память после обработки уже обработанного сообщения
        stateMemoryMoq.Verify(mem =>
                mem.SetCommittedOffsetAsync(kafkaTopicPartitionOffset, messageKey),
            Times.Once);
    }

    [Test]
    public async ValueTask ConsumeAsync_DoesNotCallRawConsumerPause_IfMemoryIsNotReadOnly(CancellationToken cancellationToken)
    {
        SetupCancelAfterOneConsume();

        // заставляем обработать событие без вызова обработчика (пропуском)
        stateMemoryMoq.Setup(mem =>
                mem.HasAnySkippedMessageWithKeyAsync(KafkaTopicPartition, messageKey))
            .Returns(ValueTask.FromResult(true));

        // память не находится в режиме "только чтение"
        partitionStateEstimatorMoq
            .Setup(est => est.EstimateMemoryStatus(
                It.IsAny<IErrorToleratedKafkaConsumerOptions>(),
                It.IsAny<PartitionMemoryState>(),
                It.IsAny<long>()))
            .Returns<IErrorToleratedKafkaConsumerOptions, PartitionMemoryState, long>((_, _, _) =>
                new PartitionMemoryEstimation(IsReadOnly: false, string.Empty));

        await consumer.ConsumeAsync(GetLinkedToken(cancellationToken))
            .IgnoreException<OperationCanceledException, IConsumeResultWrap>();

        confluentConsumerMoq
            .Verify(cons => cons.Pause(It.IsAny<IEnumerable<TopicPartition>>()),
                Times.Never);
    }

    [Test]
    public async ValueTask ConsumeAsync_CallsRawConsumerPause_IfMemoryIsReadOnly(CancellationToken cancellationToken)
    {
        SetupCancelAfterOneConsume();

        // заставляем обработать событие без вызова обработчика (пропуском)
        stateMemoryMoq.Setup(mem =>
                mem.HasAnySkippedMessageWithKeyAsync(KafkaTopicPartition, messageKey))
            .Returns(ValueTask.FromResult(true));

        // память находится в режиме "только чтение"
        partitionStateEstimatorMoq
            .Setup(est => est.EstimateMemoryStatus(
                It.IsAny<IErrorToleratedKafkaConsumerOptions>(),
                It.IsAny<PartitionMemoryState>(),
                It.IsAny<long>()))
            .Returns<IErrorToleratedKafkaConsumerOptions, PartitionMemoryState, long>((_, _, _) =>
                new PartitionMemoryEstimation(IsReadOnly: true, string.Empty));

        await consumer.ConsumeAsync(GetLinkedToken(cancellationToken))
            .IgnoreException<OperationCanceledException, IConsumeResultWrap>();

        confluentConsumerMoq.Verify(rawConsumer =>
                rawConsumer.Pause(It.Is<IEnumerable<TopicPartition>>(partitions =>
                    // ReSharper disable PossibleMultipleEnumeration
                    partitions.Count() == 1
                    && partitions.First().Partition == kafkaTopicPartitionOffset.Partition
                    && partitions.First().Topic == kafkaTopicPartitionOffset.Topic
                    // ReSharper restore PossibleMultipleEnumeration
                )),
            Times.Once);
    }

    #endregion
}
