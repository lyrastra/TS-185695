using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Models;
using Moq;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
public class KafkaConsumerStateMemoryTests
{
    private const string ConsumerGroupId = "ConsumerGroupId1";
    private const string Topic = "topic1";
    private const string MessageKey = "messageKey2";
    private const int Partition = 1;

    private KafkaConsumerStateMemory memory = null!;
    private Mock<IKafkaConsumerMessageMemoryRepository> repositoryMoq = null!;
    private Mock<IConsumingStateMemory> stateMemoryMoq = null!;
    private int offset;

    private readonly PartitionConsumingReadOnlyState emptyState = new (
        ConsumerGroupId, Topic, Partition, null, null, 0, Array.Empty<byte>());
    private readonly KafkaTopicPartition topicPartition = new (Topic, Partition);
    private KafkaTopicPartitionOffset topicPartitionOffset;

    [SetUp]
    public void SetUp()
    {
        offset = TestContext.CurrentContext.Random.Next(100, 10000);
        this.topicPartitionOffset = new KafkaTopicPartitionOffset(Topic, Partition, offset);
        repositoryMoq = new Mock<IKafkaConsumerMessageMemoryRepository>();
        stateMemoryMoq = new Mock<IConsumingStateMemory>();
        
        stateMemoryMoq.Setup(mem =>
                mem.CommitOffset(It.IsAny<KafkaTopicPartitionOffset>(), It.IsAny<string>()))
            .Returns(emptyState);

        memory = new KafkaConsumerStateMemory(repositoryMoq.Object, stateMemoryMoq.Object);
    }

    [Test]
    public async ValueTask SetCommittedOffsetAsync_CallsMemoryCommitOffset()
    {
        await memory.SetCommittedOffsetAsync(topicPartitionOffset, MessageKey);

        stateMemoryMoq.Verify(mem =>
            mem.CommitOffset(topicPartitionOffset, MessageKey), Times.Once);
    }

    [Test]
    public async ValueTask SetCommittedOffsetAsync_DoesNotCallRepositorySave_IfThereWereNotNonEmptyOffsetMap()
    {
        stateMemoryMoq.Setup(mem =>
                mem.HasNonEmptyOffsetMapMessage(topicPartition))
            .Returns(false);

        await memory.SetCommittedOffsetAsync(topicPartitionOffset, MessageKey);

        repositoryMoq.Verify(repository =>
            repository.SaveAsync(emptyState, It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async ValueTask SetCommittedOffsetAsync_CallsRepositorySave_IfThereWereNonEmptyOffsetMap()
    {
        stateMemoryMoq.Setup(mem =>
                mem.HasNonEmptyOffsetMapMessage(topicPartition))
            .Returns(true);

        await memory.SetCommittedOffsetAsync(topicPartitionOffset, MessageKey);

        repositoryMoq.Verify(repository =>
            repository.SaveAsync(emptyState, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async ValueTask MessageIsConsumedAsync_DoesNotCallsRepositorySave_IfThereIsNoCommittedOffset(
        [Values(1,2)]int consumedMessagesCount)
    {
        stateMemoryMoq.Setup(mem =>
                mem.MessageIsConsumed(topicPartitionOffset, MessageKey))
            .Returns(new PartitionConsumingSessionState(consumedMessagesCount, null));

        await memory.MessageIsConsumedAsync(topicPartitionOffset, MessageKey, CancellationToken.None);

        repositoryMoq.Verify(repository =>
            repository.SaveAsync(It.IsAny<IPartitionConsumingReadOnlyState>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    
    [Test]
    public async ValueTask MessageIsConsumedAsync_DoesNotMoveCommittedOffset_IfThereIsNoCommittedOffset(
        [Values(1,2)]int consumedMessagesCount)
    {
        stateMemoryMoq.Setup(mem =>
                mem.MessageIsConsumed(topicPartitionOffset, MessageKey))
            .Returns(new PartitionConsumingSessionState(consumedMessagesCount, null));

        await memory.MessageIsConsumedAsync(topicPartitionOffset, MessageKey, CancellationToken.None);

        stateMemoryMoq.Verify(mem =>
            mem.MoveCommittedOffsetTo(It.IsAny<KafkaTopicPartitionOffset>()),
            Times.Never);
    }

    [Test]
    public async ValueTask MessageIsConsumedAsync_DoesNotCallsRepositorySave_IfCommittedOffsetEqualsToPrevious()
    {
        stateMemoryMoq.Setup(mem =>
                mem.MessageIsConsumed(topicPartitionOffset, MessageKey))
            .Returns(new PartitionConsumingSessionState(1, offset - 1));

        await memory.MessageIsConsumedAsync(topicPartitionOffset, MessageKey, CancellationToken.None);

        repositoryMoq.Verify(repository =>
            repository.SaveAsync(It.IsAny<IPartitionConsumingReadOnlyState>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Test]
    public async ValueTask MessageIsConsumedAsync_DoesNotMoveCommittedOffset_IfCommittedOffsetEqualsToPrevious()
    {
        stateMemoryMoq.Setup(mem =>
                mem.MessageIsConsumed(topicPartitionOffset, MessageKey))
            .Returns(new PartitionConsumingSessionState(1, offset - 1));

        await memory.MessageIsConsumedAsync(topicPartitionOffset, MessageKey, CancellationToken.None);

        stateMemoryMoq.Verify(mem =>
                mem.MoveCommittedOffsetTo(It.IsAny<KafkaTopicPartitionOffset>()),
            Times.Never);
    }

    [Test]
    public async ValueTask MessageIsConsumedAsync_MovesCommittedOffset_IfItIsTheFirstConsumedMessageAndCommittedOffsetIsTooLow(
        [Values(2, 3, 4, 5)] int committedOffsetLag)
    {
        var committedOffset = offset - committedOffsetLag; 

        stateMemoryMoq.Setup(mem =>
                mem.MessageIsConsumed(topicPartitionOffset, MessageKey))
            .Returns(new PartitionConsumingSessionState(1, committedOffset));

        await memory.MessageIsConsumedAsync(topicPartitionOffset, MessageKey, CancellationToken.None);

        var expectedNewCommittedOffset = topicPartitionOffset with { Offset = topicPartitionOffset.Offset - 1 };
        stateMemoryMoq.Verify(mem =>
                mem.MoveCommittedOffsetTo(expectedNewCommittedOffset),
            Times.Once);
    }

    [Test]
    public async ValueTask MessageIsConsumedAsync_CallsRepositorySave_IfItIsTheFirstConsumedMessageAndCommittedOffsetIsTooLow(
        [Values(2, 3, 4, 5)] int committedOffsetLag)
    {
        var state = new PartitionConsumingReadOnlyState(
            ConsumerGroupId,
            Topic,
            Partition,
            1234, DateTime.UtcNow, 0,
            Array.Empty<byte>());
        
        var committedOffset = offset - committedOffsetLag; 

        stateMemoryMoq.Setup(mem =>
                mem.MessageIsConsumed(topicPartitionOffset, MessageKey))
            .Returns(new PartitionConsumingSessionState(1, committedOffset));

        stateMemoryMoq.Setup(mem =>
                mem.MoveCommittedOffsetTo(It.IsAny<KafkaTopicPartitionOffset>()))
            .Returns(state);

        await memory.MessageIsConsumedAsync(topicPartitionOffset, MessageKey, CancellationToken.None);

        repositoryMoq.Verify(repository =>
            repository.SaveAsync(state, It.IsAny<CancellationToken>()), Times.Once);
    }
}
