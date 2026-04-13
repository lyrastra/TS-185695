using System.Collections;
using FluentAssertions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
public class PartitionConsumingStateTests
{
    private const string ConsumerGroupId = "ConsumingStateMemory.Test.GroupId";
    private const string Topic = "Kafka.Topic.Name";
    private const int MemoryCapacity = 1024;
    private PartitionConsumingState state = null!;

    private int partition;

     
    private long committedOffset;
    private DateTime committedDate;
    private BitArray processedOffsetMap = null!;
    private string messageKey = null!;

    [SetUp]
    public void SetUp()
    {
        this.partition = TestContext.CurrentContext.Random.Next(12);
        this.state = new PartitionConsumingState(ConsumerGroupId, Topic, partition, MemoryCapacity);
        this.committedOffset = TestContext.CurrentContext.Random.NextLong();
        committedDate = DateTime.Now;
        this.messageKey = TestContext.CurrentContext.Random.Next().ToString();
        this.processedOffsetMap = new BitArray(PartitionConsumingState.MaxOffsetMapSize);
    }

    [Test]
    public void IsAssigned_IsFalse_ByDefault()
    {
        state.IsAssigned.Should().BeFalse();
    }

    [Test]
    public void IsAssigned_IsTrue_AfterAssign()
    {
        state.Assigned();
        state.IsAssigned.Should().BeTrue();
    }

    [Test]
    public void Assigned_ThrowsException_OnSecondCall()
    {
        state.Assigned();
        var act = () => state.Assigned();
        act.Should().Throw<InvalidAssignOfAlreadyAssignedPartitionException>();
    }

    [Test]
    public void Revoked_ThrowsException_IfNotAssigned()
    {
        var act = () => state.Revoked();
        act.Should().Throw<InvalidOperationOnNonAssignedPartitionException>();
    }

    [Test]
    public void Revoked_SetsIsAssignedToFalse()
    {
        state.Assigned();
        state.Revoked();
        state.IsAssigned.Should().BeFalse();
    }

    [Test]
    public void Assigned_RestoreIsAssigned_AfterRevoked()
    {
        state.Assigned();
        state.Revoked();
        state.Assigned();
        state.IsAssigned.Should().BeTrue();
    }

    #region SetCommitmentState

    [Test]
    public void SetCommitmentState_SetsProperCommittedOffsetValue()
    {
        state.Assigned();
        state.SetCommitmentState(committedOffset, committedDate, 0, Array.Empty<byte>());
        state.CommittedOffset.Should().Be(committedOffset);

        state.SetCommitmentState(committedOffset + 1, committedDate, 0, Array.Empty<byte>());
        state.CommittedOffset.Should().Be(committedOffset + 1);
    }

    [Test]
    public void SetCommitmentState_ThrowsException_IfIsNotAssigned()
    {
        var act = () => state.SetCommitmentState(committedOffset, committedDate, 0, Array.Empty<byte>());

        act.Should().Throw<InvalidOperationOnNonAssignedPartitionException>();
    }

    #endregion

    #region IsAlreadyProcessed

    [Test]
    [TestCase(-1, true)]
    [TestCase(0, true)]
    [TestCase(1, false)]
    public void IsAlreadyProcessed_ReturnsProperValue_WithEmptyOffsetMap(int delta, bool expected)
    {
        state.Assigned();
        state.SetCommitmentState(committedOffset, committedDate, 0, Array.Empty<byte>());

        state.IsAlreadyProcessed(committedOffset + delta).Should().Be(expected);
    }

    // ReSharper disable once InconsistentNaming
    private static object[][] IsAlreadyProcessed_ReturnsProperValue_WithOffsetMapTestCaseSource =
    {
        new object[] { new[] { false, false, false }, 1, false },
        new object[] { new[] { true, false, false }, 1, true },
        new object[] { new[] { true, false, false }, 2, false },
        new object[] { new[] { false, true, false }, 1, false }
    };

    [Test]
    [TestCaseSource(nameof(IsAlreadyProcessed_ReturnsProperValue_WithOffsetMapTestCaseSource))]
    public void IsAlreadyProcessed_ReturnsProperValue_WithOffsetMap(bool[] offsetMap, int delta, bool expected)
    {
        state.Assigned();
        processedOffsetMap.CopyFrom(offsetMap);
        state.SetCommitmentState(committedOffset, committedDate, offsetMap.Length, processedOffsetMap.ToByteArray());

        state.IsAlreadyProcessed(committedOffset + delta).Should().Be(expected);
    }

    #endregion

    #region HasAnySkippedMessageBefore

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_IfThereIsNoCommittedOffset()
    {
        state.HasAnySkippedMessageBefore(committedOffset).Should().BeFalse();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_IfOffsetIsLessOrEqualThanCommittedOffset()
    {
        state.Assigned();
        state.SetCommitmentState(committedOffset, committedDate, 0, Array.Empty<byte>());

        state.HasAnySkippedMessageBefore(committedOffset - 1).Should().BeFalse();
        state.HasAnySkippedMessageBefore(committedOffset).Should().BeFalse();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ThrowsException_IfOffsetTooFarFromCommittedOffset()
    {
        state.Assigned();
        state.SetCommitmentState(committedOffset, committedDate, 0, Array.Empty<byte>());

        var newOffset = committedOffset + PartitionConsumingState.MaxOffsetMapSize + 1;

        var act = () => state.HasAnySkippedMessageBefore(newOffset);

        act.Should().Throw<InvalidOffsetException>();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_ForNextAfterCommittedOffset()
    {
        state.Assigned();
        state.SetCommitmentState(committedOffset, committedDate, 0, Array.Empty<byte>());

        state.HasAnySkippedMessageBefore(committedOffset + 1).Should().BeFalse();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsTrue_AfterMessageIsSkipped()
    {
        state.Assigned();
        state.SetCommitmentState(committedOffset, committedDate, 0, Array.Empty<byte>());
        state.MarkAsSkipped(committedOffset + 1, messageKey);
        state.HasAnySkippedMessageBefore(committedOffset + 2).Should().BeTrue();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_IfOffsetIsGreaterThanCommittedOffsetAndOffsetMapContainsOnlyProcessedMessageBefore()
    {
        state.Assigned();
        processedOffsetMap.Set(0, true);
        state.SetCommitmentState(committedOffset, committedDate, offsetMapDepth: 1, processedOffsetMap.ToByteArray());

        state.HasAnySkippedMessageBefore(committedOffset + 2).Should().BeFalse();
    }

    [Test]
    public void
        HasAnySkippedMessageBefore_ReturnsTrue_IfOffsetIsGreaterThanCommittedOffsetAndContainsSkippedMessageBefore()
    {
        state.Assigned();
        processedOffsetMap.Set(0, false);
        state.SetCommitmentState(committedOffset, committedDate, offsetMapDepth: 1, processedOffsetMap.ToByteArray());

        state.HasAnySkippedMessageBefore(committedOffset + 2).Should().BeTrue();
    }

    [Test]
    public void
        HasAnySkippedMessageBefore_ReturnsTrue_IfOffsetIsGreaterThanCommittedOffsetAndContainsSkippedMessageBefore2()
    {
        state.Assigned();
        processedOffsetMap.Set(0, false);
        processedOffsetMap.Set(1, true);
        processedOffsetMap.Set(2, true);
        state.SetCommitmentState(committedOffset, committedDate, offsetMapDepth: 3, processedOffsetMap.ToByteArray());

        state.HasAnySkippedMessageBefore(committedOffset + 3).Should().BeTrue();
    }

    #endregion
}
