using FluentAssertions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Extensions;
using Moq;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests;

[TestFixture(Description = "Набор тестов на ConsumingStateMemory")]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
public class ConsumingStateMemoryTests
{
    private const string ConsumerGroupId = "ConsumingStateMemory.Test.GroupId";
    private const string Topic = "Kafka.Topic.Name";
    private const int MemoryCapacity = 1024;
    private ConsumingStateMemory memory = null!;
    private int partition;
    private KafkaTopicPartition topicPartition;
    private long baseOffset;
    private string messageKey = null!;
    private KafkaTopicPartitionOffset partitionOffset;

    [SetUp]
    public void SetUp()
    {
        memory = new ConsumingStateMemory(ConsumerGroupId, new PartitionConsumingStateFactory(), MemoryCapacity);
        partition = TestContext.CurrentContext.Random.Next(12);
        topicPartition = new KafkaTopicPartition(Topic, partition);
        baseOffset = TestContext.CurrentContext.Random.NextLong(100, 1000000000L);
        messageKey = TestContext.CurrentContext.Random.Next().ToString();
        partitionOffset = new KafkaTopicPartitionOffset(Topic, partition, baseOffset);
    }

    #region Throws exception to any call to not assigned partition

    [Test]
    public void ProcessMessage_ThrowsException_IfPartitionIsNotAssigned()
    {
        // memory.Assigned() is not called yet;
        Action act = () => memory.ProcessMessage(partitionOffset, messageKey);
        act.Should().Throw<InvalidOperationOnNonAssignedPartitionException>();
    }

    [Test]
    public void SkipMessage_ThrowsException_IfPartitionIsNotAssigned()
    {
        // memory.Assigned() is not called yet;
        Action act = () => memory.SkipMessage(partitionOffset, messageKey);
        act.Should().Throw<InvalidOperationOnNonAssignedPartitionException>();
    }

    [Test]
    public void IsMessageAlreadyProcessed_ThrowsException_IfPartitionIsNotAssigned()
    {
        // memory.Assigned() is not called yet;
        Action act = () => memory.IsMessageAlreadyProcessed(partitionOffset);
        act.Should().Throw<InvalidOperationOnNonAssignedPartitionException>();
    }

    [Test]
    public void HasSomeSkippedMessagesBefore_WithoutMessageKey_ThrowsException_IfPartitionIsNotAssigned()
    {
        // memory.Assigned() is not called yet;
        Action act = () => memory.HasAnySkippedMessageBefore(partitionOffset);
        act.Should().Throw<InvalidOperationOnNonAssignedPartitionException>();
    }

    [Test]
    public void HasSomeSkippedMessagesBefore_WithMessageKey_ThrowsException_IfPartitionIsNotAssigned()
    {
        // memory.Assigned() is not called yet;
        Action act = () => memory.HasAnySkippedMessageWithKey(topicPartition, messageKey);
        act.Should().Throw<InvalidOperationOnNonAssignedPartitionException>();
    }

    #endregion

    [Test]
    public void HasAnySkippedMessage_ReturnsFalse_RightAfterAssignment()
    {
        memory.Assigned(topicPartition);
        memory.HasAnySkippedMessageBefore(partitionOffset).Should().BeFalse();
    }

    [Test]
    public void HasAnySkippedMessage_ReturnsFalse_RightAfterAssignmentFromEmptyState()
    {
        var state = new PartitionConsumingState(ConsumerGroupId, Topic, partition, MemoryCapacity);

        memory.Assigned(state);
        memory.HasAnySkippedMessageBefore(partitionOffset).Should().BeFalse();
    }

    [Test]
    public void Assigned_CallsStateFactory()
    {
        var stateFactoryMoq = new Mock<IPartitionConsumingStateFactory>();
        stateFactoryMoq
            .Setup(factory => factory
                .Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns<string, string, int, int>((group, topic, partitionNum, capacity) =>
                new PartitionConsumingState(group, topic, partitionNum, capacity));
        memory = new ConsumingStateMemory(ConsumerGroupId, stateFactoryMoq.Object, MemoryCapacity);

        memory.Assigned(topicPartition);
        stateFactoryMoq.Verify(factory =>
                factory.Create(ConsumerGroupId, Topic, partition, MemoryCapacity),
            Times.Once);
    }

    [Test]
    public void Assigned_CallsAssignedForPartitionState()
    {
        var stateMoq = new Mock<IPartitionConsumingState>();
        var stateFactoryMoq = new Mock<IPartitionConsumingStateFactory>();
        stateFactoryMoq
            .Setup(factory => factory
                .Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(stateMoq.Object);
        memory = new ConsumingStateMemory(ConsumerGroupId, stateFactoryMoq.Object, MemoryCapacity);

        memory.Assigned(topicPartition);

        stateMoq.Verify(factory => factory.Assigned(), Times.Once);
    }

    [Test]
    public void Assigned_CallsAssignedForPartitionState_IfCalledAfterRevoked()
    {
        var stateMoq = new Mock<IPartitionConsumingState>();
        var stateFactoryMoq = new Mock<IPartitionConsumingStateFactory>();
        stateFactoryMoq
            .Setup(factory => factory
                .Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(stateMoq.Object);
        memory = new ConsumingStateMemory(ConsumerGroupId, stateFactoryMoq.Object, MemoryCapacity);

        memory.Assigned(topicPartition);
        memory.Revoked(partitionOffset);
        stateMoq.Invocations.Clear();
        memory.Assigned(topicPartition);
        stateMoq.Verify(factory => factory.Assigned(), Times.Once);
    }

    [Test]
    public void Assigned_CallsStateFactory_IfAssignedFromState()
    {
        var stateFactoryMoq = new Mock<IPartitionConsumingStateFactory>();
        stateFactoryMoq
            .Setup(factory => factory
                .Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns<string, string, int, int>((group, topic, partitionNum, capacity) =>
                new PartitionConsumingState(group, topic, partitionNum, capacity));
        memory = new ConsumingStateMemory(ConsumerGroupId, stateFactoryMoq.Object, MemoryCapacity);

        var storedState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition, null, DateTime.Now, 0, Array.Empty<byte>());
        memory.Assigned(storedState);

        stateFactoryMoq.Verify(factory => factory.Create(ConsumerGroupId, Topic, partition, MemoryCapacity), Times.Once);
    }

    [Test]
    public void Assigned_CallsAssignsForPartitionState_IfAssignedFromState()
    {
        var stateMoq = new Mock<IPartitionConsumingState>();
        var stateFactoryMoq = new Mock<IPartitionConsumingStateFactory>();
        stateFactoryMoq
            .Setup(factory => factory
                .Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(stateMoq.Object);
        memory = new ConsumingStateMemory(ConsumerGroupId, stateFactoryMoq.Object, MemoryCapacity);

        var storedState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition, null, DateTime.Now, 0, Array.Empty<byte>());
        memory.Assigned(storedState);

        stateMoq.Verify(factory => factory.Assigned(), Times.Once);
    }

    [Test]
    public void Assigned_CallsAssignedForPartitionState_IfCalledAfterRevokedAndAssignedFromState()
    {
        var stateMoq = new Mock<IPartitionConsumingState>();
        stateMoq.SetupGet(s => s.Topic).Returns(Topic);
        stateMoq.SetupGet(s => s.ConsumerGroupId).Returns(ConsumerGroupId);
        stateMoq.SetupGet(s => s.Partition).Returns(partition);
        stateMoq.SetupGet(s => s.CommittedOffset).Returns((long?)default);
        stateMoq.SetupGet(s => s.OffsetMapDepth).Returns(0);
        stateMoq.SetupGet(s => s.OffsetMapData).Returns(Array.Empty<byte>());

        var stateFactoryMoq = new Mock<IPartitionConsumingStateFactory>();
        stateFactoryMoq
            .Setup(factory => factory
                .Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(stateMoq.Object);
        memory = new ConsumingStateMemory(ConsumerGroupId, stateFactoryMoq.Object, MemoryCapacity);
        var storedState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition, null, DateTime.Now, 0, Array.Empty<byte>());

        memory.Assigned(storedState);
        memory.Revoked(partitionOffset);
        stateMoq.Invocations.Clear();
        memory.Assigned(storedState);
        stateMoq.Verify(factory => factory.Assigned(), Times.Once);
    }

    [Test]
    public void Assigned_SetsCommitmentState_IfAssignedFromStateWithNotNullCommittedOffset()
    {
        var stateMoq = new Mock<IPartitionConsumingState>();
        var stateFactoryMoq = new Mock<IPartitionConsumingStateFactory>();
        stateFactoryMoq
            .Setup(factory => factory
                .Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(stateMoq.Object);
        memory = new ConsumingStateMemory(ConsumerGroupId, stateFactoryMoq.Object, MemoryCapacity);

        var committedOffset = TestContext.CurrentContext.Random.NextLong();
        var committedDate = DateTime.Now;
        var offsetMapDepth = TestContext.CurrentContext.Random.Next(10, 20);
        var offsetMapData = new byte[] { 4, 5, 8 };
        var storedState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition, committedOffset, committedDate, offsetMapDepth, offsetMapData);
        memory.Assigned(storedState);

        stateMoq.Verify(factory =>
                factory.SetCommitmentState(committedOffset, committedDate, offsetMapDepth, offsetMapData),
            Times.Once);
    }

    [Test]
    public void Assigned_DoesNotSetCommitmentState_IfAssignedFromStateWithNullCommittedOffset()
    {
        var stateMoq = new Mock<IPartitionConsumingState>();
        var stateFactoryMoq = new Mock<IPartitionConsumingStateFactory>();
        stateFactoryMoq
            .Setup(factory => factory
                .Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(stateMoq.Object);
        memory = new ConsumingStateMemory(ConsumerGroupId, stateFactoryMoq.Object, MemoryCapacity);

        var storedState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition,
            CommittedOffset: null,
            CommittedDateTimeUtc: null,
            OffsetMapDepth: 0,
            OffsetMapData: Array.Empty<byte>());
        memory.Assigned(storedState);

        stateMoq.Verify(factory =>
                factory.SetCommitmentState(
                    It.IsAny<long>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>(),
                    It.IsAny<byte[]>()),
            Times.Never);
    }

    [Test]
    public void Assigned_DoesNotThrowsException_IfAssignedFromStateWithSameNotNullCommittedOffset()
    {
        var committedOffset = TestContext.CurrentContext.Random.NextLong();
        var utcNow = DateTime.UtcNow;

        var storedState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition,
            CommittedOffset: committedOffset,
            CommittedDateTimeUtc: utcNow, 
            OffsetMapDepth: 0,
            OffsetMapData: Array.Empty<byte>());

        memory.Assigned(storedState);
        memory.Revoked(new KafkaTopicPartitionOffset(Topic, partition, committedOffset));
        
        var diffState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition,
            CommittedOffset: committedOffset,
            CommittedDateTimeUtc: utcNow, 
            OffsetMapDepth: 0,
            OffsetMapData: Array.Empty<byte>());
        
        var act = () => memory.Assigned(diffState);
        act.Should().NotThrow();
    }

    [Test]
    public void Assigned_DoesNotThrowsException_IfAssignedFromStateWithNullCommittedOffsetWhenSomeOffsetHasAlreadyCommitted()
    {
        var committedOffset = TestContext.CurrentContext.Random.NextLong();
        var utcNow = DateTime.UtcNow;

        var storedState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition,
            CommittedOffset: committedOffset,
            CommittedDateTimeUtc: utcNow, 
            OffsetMapDepth: 0,
            OffsetMapData: Array.Empty<byte>());

        memory.Assigned(storedState);
        memory.Revoked(new KafkaTopicPartitionOffset(Topic, partition, committedOffset));
        
        var diffState = new PartitionConsumingReadOnlyState(
            ConsumerGroupId, Topic, partition,
            CommittedOffset: null,
            CommittedDateTimeUtc: null, 
            OffsetMapDepth: 0,
            OffsetMapData: Array.Empty<byte>());
        
        var act = () => memory.Assigned(diffState);
        act.Should().NotThrow();
    }

    #region IsMessageAlreadyProcessed tests

    [Test]
    [TestCase(-2, false)]
    [TestCase(-1, false)]
    [TestCase(0, false)]
    [TestCase(1, false)]
    public void IsMessageAlreadyProcessed_ReturnsFalse_IfOffsetIsUnknown(int delta, bool expected)
    {
        memory.Assigned(topicPartition);
        var newOffset = partitionOffset with { Offset = partitionOffset.Offset + delta };

        memory.IsMessageAlreadyProcessed(newOffset).Should().Be(expected);
    }

    [Test]
    public void IsMessageAlreadyProcessed_ReturnsTrue_ForAlreadyProcessedMessage()
    {
        memory.Assigned(topicPartition);
        // commit offset
        memory.CommitOffset(partitionOffset, messageKey);
        // set on pause by skipping message
        memory.SkipMessage(partitionOffset with { Offset = partitionOffset.Offset + 1 }, messageKey);
        var processedMessage = partitionOffset with { Offset = partitionOffset.Offset + 2 };
        memory.ProcessMessage(processedMessage, messageKey + "1");

        memory.IsMessageAlreadyProcessed(processedMessage).Should().BeTrue();
    }

    #endregion

    #region ProcessMessage tests

    [Test]
    public void ProcessMessage_ThrowsException_IfCommittedOffsetIsNotSet()
    {
        memory.Assigned(topicPartition);
        var newOffset = partitionOffset with { Offset = partitionOffset.Offset + 1 };
        var act = () => memory.ProcessMessage(newOffset, messageKey);

        act.Should().Throw<InvalidOffsetException>();
    }

    [Test]
    public void ProcessMessage_ThrowsException_IfOffsetIsLessThanOrEqualCommittedOffset()
    {
        memory.Assigned(topicPartition);

        var newOffset = partitionOffset with { Offset = partitionOffset.Offset - 1 };
        var act = () => memory.ProcessMessage(newOffset, messageKey);

        act.Should().Throw<InvalidOffsetException>();
    }

    [Test]
    public void ProcessMessage_ReturnsProperState()
    {
        memory.Assigned(topicPartition);
        memory.CommitOffset(partitionOffset, messageKey);

        var newOffset = partitionOffset with { Offset = partitionOffset.Offset + 1 };
        var state = memory.ProcessMessage(newOffset, messageKey);

        state.ConsumerGroupId.Should().Be(ConsumerGroupId);
        state.Topic.Should().Be(Topic);
        state.Partition.Should().Be(partition);
        state.CommittedOffset.Should().Be(partitionOffset.Offset);
    }

    #endregion
}
