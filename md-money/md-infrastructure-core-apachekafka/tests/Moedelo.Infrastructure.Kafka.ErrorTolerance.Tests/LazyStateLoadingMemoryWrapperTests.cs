using FluentAssertions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Models;
using Moq;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
public class LazyStateLoadingMemoryWrapperTests
{
    private LazyStateLoadingMemoryWrapper memoryWrapper;
    private Mock<IKafkaConsumerMessageMemoryRepository> repositoryMoq;
    private Mock<IConsumingStateMemory> memoryMoq;
    private int randomPartition;
    private const string DefaultTopic = "Topic456";
    private const string ConsumerGroupId = "ConsumerGroupId123";

    [SetUp]
    public void SetUp()
    {
        this.randomPartition = TestContext.CurrentContext.Random.Next(1, 12);
        this.repositoryMoq = new Mock<IKafkaConsumerMessageMemoryRepository>();
        repositoryMoq.Setup(r => r.GetOrCreateAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .Returns<string, string, int, CancellationToken>((consumerGroupId, topic, partition, cancellationToken) =>
                Task.FromResult<IPartitionConsumingReadOnlyState>(
                    new PartitionConsumingReadOnlyState(consumerGroupId, topic, partition, default, default, 0,
                        Array.Empty<byte>())));
        this.memoryMoq = new Mock<IConsumingStateMemory>();
        memoryMoq.SetupGet(m => m.ConsumerGroupId)
            .Returns(ConsumerGroupId);
        memoryWrapper = new LazyStateLoadingMemoryWrapper(
            repositoryMoq.Object, memoryMoq.Object);
    }

    [Test]
    public void AssignPartition_DoesNotCallRepository()
    {
        memoryWrapper.AssignPartition(new KafkaTopicPartition(DefaultTopic, randomPartition));

        repositoryMoq.Verify(repo => repo.GetOrCreateAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public void AssignPartition_DoesNotCallMemory()
    {
        memoryWrapper.AssignPartition(new KafkaTopicPartition(DefaultTopic, randomPartition));

        memoryMoq.Verify(m => m.Assigned(
            It.IsAny<IPartitionConsumingReadOnlyState>()), Times.Never);
    }

    [Test]
    public async ValueTask GetMemoryAsync_ReturnsMemory()
    {
        memoryWrapper.AssignPartition(new KafkaTopicPartition(DefaultTopic, randomPartition));
        var actual = await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        actual.Should().Be(memoryMoq.Object);
    }

    [Test]
    public async ValueTask GetMemoryAsync_LoadsState()
    {
        memoryWrapper.AssignPartition(new KafkaTopicPartition(DefaultTopic, randomPartition));
        await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        repositoryMoq.Verify(repo => repo.GetOrCreateAsync(
            ConsumerGroupId, DefaultTopic, randomPartition,
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async ValueTask GetMemoryAsync_LoadsStateOnce()
    {
        memoryWrapper.AssignPartition(new KafkaTopicPartition(DefaultTopic, randomPartition));
        await memoryWrapper.GetMemoryAsync(CancellationToken.None);
        repositoryMoq.Invocations.Clear();
        await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        repositoryMoq.Verify(repo => repo.GetOrCreateAsync(
            ConsumerGroupId, DefaultTopic, randomPartition,
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async ValueTask GetMemoryAsync_SetsMemoryState()
    {
        memoryWrapper.AssignPartition(new KafkaTopicPartition(DefaultTopic, randomPartition));
        var _ = await memoryWrapper.GetMemoryAsync(CancellationToken.None);
        
        memoryMoq.Verify(m => m.Assigned(
            It.IsAny<IPartitionConsumingReadOnlyState>()), Times.Once);
    }

    [Test]
    public async ValueTask GetMemoryAsync_SetsMemoryStateOnce()
    {
        memoryWrapper.AssignPartition(new KafkaTopicPartition(DefaultTopic, randomPartition));
        await memoryWrapper.GetMemoryAsync(CancellationToken.None);
        repositoryMoq.Invocations.Clear();
        await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        repositoryMoq.Verify(repo => repo.GetOrCreateAsync(
            ConsumerGroupId, DefaultTopic, randomPartition,
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async ValueTask GetMemoryAsync_DoesNotAssignedIfAlreadyRevoked()
    {
        var offset = TestContext.CurrentContext.Random.Next();
        var kafkaTopicPartition = new KafkaTopicPartition(DefaultTopic, randomPartition);
        var kafkaTopicPartitionOffset = new KafkaTopicPartitionOffset(DefaultTopic, randomPartition, offset);

        memoryWrapper.AssignPartition(kafkaTopicPartition);
        memoryWrapper.RevokePartition(kafkaTopicPartitionOffset);

        var _ = await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        memoryMoq.Verify(m => m.Assigned(
            It.IsAny<IPartitionConsumingReadOnlyState>()), Times.Never);
    }
}
