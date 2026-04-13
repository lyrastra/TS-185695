using FluentAssertions;
using Moq;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
public class PartitionStateEstimatorTests
{
    private const int Partition = 7;
    private readonly TimeSpan defaultErrorToleranceTimeSpan = TimeSpan.FromMinutes(1);
    private const int DefaultMaxOffsetMapDepth = 100;
    private const int DefaultMaxPausedMessageKeys = 10;
    
    private PartitionStateEstimator estimator = null!;
    private IErrorToleratedKafkaConsumerOptions options = null!;
    private TimeSpan errorToleranceTimeSpan;
    private int maxOffsetMapDepth;
    private int maxPausedMessageKeys;
    private int currentOffset;

    [SetUp]
    public void SetUp()
    {
        var optionsMoq = new Mock<IErrorToleratedKafkaConsumerOptions>();
        optionsMoq.SetupGet(opt => opt.ErrorToleranceTimeSpan).Returns(() => this.errorToleranceTimeSpan);
        optionsMoq.SetupGet(opt => opt.MaxOffsetMapDepth).Returns(() => this.maxOffsetMapDepth);
        optionsMoq.SetupGet(opt => opt.MaxPausedMessageKeys).Returns(() => this.maxPausedMessageKeys);

        options = optionsMoq.Object;
        currentOffset = TestContext.CurrentContext.Random.Next();
        estimator = new PartitionStateEstimator();
        errorToleranceTimeSpan = defaultErrorToleranceTimeSpan;
        maxOffsetMapDepth = DefaultMaxOffsetMapDepth;
        maxPausedMessageKeys = DefaultMaxPausedMessageKeys;
    }

    [Test]
    public void IfMemoryIsEmpty([Values(false)]bool isReadOnly)
    {
        var state = new PartitionMemoryState(
            Partition,
            CommittedOffset: default,
            OffsetMapDepth: 0,
            UniqueSkippedMessageKeysCount: 0);

        var estimation = estimator.EstimateMemoryStatus(options, state, currentOffset);

        estimation.IsReadOnly.Should().Be(isReadOnly);
    }

    [Test]
    public void IfUniqueKeysCountDoesNotExceedLimit([Values(false)]bool isReadOnly)
    {
        var state = new PartitionMemoryState(
            Partition,
            CommittedOffset: default,
            OffsetMapDepth: 0,
            UniqueSkippedMessageKeysCount: options.MaxPausedMessageKeys);

        var estimation = estimator.EstimateMemoryStatus(options, state, currentOffset);

        estimation.IsReadOnly.Should().Be(isReadOnly);
    }

    [Test]
    public void IfUniqueKeysCountExceedsLimit([Values(true)]bool isReadOnly)
    {
        var state = new PartitionMemoryState(
            Partition,
            CommittedOffset: default,
            OffsetMapDepth: 0,
            UniqueSkippedMessageKeysCount: options.MaxPausedMessageKeys + 1);

        var estimation = estimator.EstimateMemoryStatus(options, state, currentOffset);

        estimation.IsReadOnly.Should().Be(isReadOnly);
    }

    [Test]
    public void ForFirstMessageInMemoryAfterCommit([Values(false)]bool isReadOnly)
    {
        var state = new PartitionMemoryState(
            Partition,
            CommittedOffset: new(99, DateTime.UtcNow),
            OffsetMapDepth: 1,
            UniqueSkippedMessageKeysCount: 1);

        var estimation = estimator.EstimateMemoryStatus(options, state, currentOffset);

        estimation.IsReadOnly.Should().Be(isReadOnly);
    }

    [Test]
    public void ForNotTooOldCommittedDate([Values(false)] bool isReadOnly)
    {
        var committedAtUtc = DateTime.UtcNow - errorToleranceTimeSpan.Add(TimeSpan.FromSeconds(-1));
        var state = new PartitionMemoryState(
            Partition,
            CommittedOffset: new(99, committedAtUtc),
            OffsetMapDepth: 1,
            UniqueSkippedMessageKeysCount: 1);

        var estimation = estimator.EstimateMemoryStatus(options, state, currentOffset);

        estimation.IsReadOnly.Should().Be(isReadOnly);
    }

    [Test]
    public void ForTooOldCommittedDate([Values(true)] bool isReadOnly)
    {
        var committedAtUtc = DateTime.UtcNow - errorToleranceTimeSpan.Add(TimeSpan.FromSeconds(1));
        var state = new PartitionMemoryState(
            Partition,
            CommittedOffset: new(99, committedAtUtc),
            OffsetMapDepth: 1,
            UniqueSkippedMessageKeysCount: 1);

        var estimation = estimator.EstimateMemoryStatus(options, state, currentOffset);

        estimation.IsReadOnly.Should().Be(isReadOnly);
    }
}
