using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Consul.Abstractions;
using Moq;
using NUnit.Framework;

namespace Moedelo.Common.AspNet.HostedServices.Tests;

[TestFixture]
public class MoedeloExclusivePeriodicHostedServiceTests
{
    private Mock<IMoedeloServiceLeadershipService> leadershipServiceMock;
    private Mock<IAuditTracer> auditTracerMock;
    private Mock<ILogger> loggerMock;
    private TestExclusiveHostedService hostedService;
    private CancellationToken cancellationToken;

    [SetUp]
    public void Setup()
    {
        leadershipServiceMock = new Mock<IMoedeloServiceLeadershipService>();
        auditTracerMock = new Mock<IAuditTracer>();
        loggerMock = new Mock<ILogger>();
        cancellationToken = CancellationToken.None;

        leadershipServiceMock.Setup(x => x.ConsulSessionId).Returns("test-session-id");

        hostedService = new TestExclusiveHostedService(
            leadershipServiceMock.Object,
            auditTracerMock.Object,
            loggerMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        hostedService?.Dispose();
    }

    [Test]
    public async Task DoExecuteTaskAsync_WhenLeadershipAcquired_ShouldCallExecuteTaskExclusivelyAsync()
    {
        // Arrange
        leadershipServiceMock
            .Setup(x => x.AcquireLeadershipAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        await hostedService.DoExecuteTaskAsync(cancellationToken);

        // Assert
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCalled, Is.True);
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCallCount, Is.EqualTo(1));
    }

    [Test]
    public async Task DoExecuteTaskAsync_WhenLeadershipNotAcquired_ShouldNotCallExecuteTaskExclusivelyAsync()
    {
        // Arrange
        leadershipServiceMock
            .Setup(x => x.AcquireLeadershipAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        await hostedService.DoExecuteTaskAsync(cancellationToken);

        // Assert
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCalled, Is.False);
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCallCount, Is.EqualTo(0));
    }

    [Test]
    public async Task DoExecuteTaskAsync_WhenLeadershipAcquired_ShouldCallAcquireLeadershipAsyncWithCorrectParameters()
    {
        // Arrange
        leadershipServiceMock
            .Setup(x => x.AcquireLeadershipAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        await hostedService.DoExecuteTaskAsync(cancellationToken);

        // Assert
        leadershipServiceMock.Verify(
            x => x.AcquireLeadershipAsync("test-leadership-id", cancellationToken),
            Times.Once);
    }

    [Test]
    public async Task DoExecuteTaskAsync_WhenLeadershipStatusChanges_ShouldCallExecuteTaskExclusivelyAsyncOnlyWhenMaster()
    {
        // Arrange
        leadershipServiceMock
            .SetupSequence(x => x.AcquireLeadershipAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false)  // First call: not master
            .ReturnsAsync(true);  // Second call: becomes master

        // Act
        await hostedService.DoExecuteTaskAsync(cancellationToken); // First call
        await hostedService.DoExecuteTaskAsync(cancellationToken); // Second call

        // Assert
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCallCount, Is.EqualTo(1));
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCalled, Is.True);
    }

    [Test]
    public async Task DoExecuteTaskAsync_WhenLeadershipStatusDoesNotChange_ShouldCallExecuteTaskExclusivelyAsyncEachTime()
    {
        // Arrange
        leadershipServiceMock
            .Setup(x => x.AcquireLeadershipAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        await hostedService.DoExecuteTaskAsync(cancellationToken); // First call
        await hostedService.DoExecuteTaskAsync(cancellationToken); // Second call

        // Assert
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCallCount, Is.EqualTo(2));
    }

    [Test]
    public void DoExecuteTaskAsync_WhenAcquireLeadershipThrowsException_ShouldRethrowException()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Test exception");
        leadershipServiceMock
            .Setup(x => x.AcquireLeadershipAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(expectedException);

        // Act & Assert
        var actualException = Assert.ThrowsAsync<InvalidOperationException>(
            () => hostedService.DoExecuteTaskAsync(cancellationToken));

        Assert.That(actualException, Is.SameAs(expectedException));
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCalled, Is.False);
    }

    [Test]
    public void DoExecuteTaskAsync_WhenExecuteTaskExclusivelyAsyncThrowsException_ShouldRethrowException()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Test exception");
        leadershipServiceMock
            .Setup(x => x.AcquireLeadershipAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        hostedService.ShouldThrowException = expectedException;

        // Act & Assert
        var actualException = Assert.ThrowsAsync<InvalidOperationException>(
            () => hostedService.DoExecuteTaskAsync(cancellationToken));

        Assert.That(actualException, Is.SameAs(expectedException));
        Assert.That(hostedService.ExecuteTaskExclusivelyAsyncCalled, Is.True);
    }

    [Test]
    public void OnBeforeStart_WithInvalidLeadershipLockId_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidHostedService = new InvalidTestExclusiveHostedService(
            leadershipServiceMock.Object,
            auditTracerMock.Object,
            loggerMock.Object);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => invalidHostedService.OnBeforeStart());
    }

    /// <summary>
    /// Тестовый класс для тестирования MoedeloExclusivePeriodicHostedService
    /// </summary>
    private class TestExclusiveHostedService : MoedeloExclusivePeriodicHostedService
    {
        public bool ExecuteTaskExclusivelyAsyncCalled { get; private set; }
        public int ExecuteTaskExclusivelyAsyncCallCount { get; private set; }
        public Exception? ShouldThrowException { get; set; }

        public TestExclusiveHostedService(
            IMoedeloServiceLeadershipService leadershipService,
            IAuditTracer auditTracer,
            ILogger logger) : base(leadershipService, auditTracer, logger)
        {
        }

        protected override string LeadershipLockId => "test-leadership-id";

        protected override Task ExecuteTaskExclusivelyAsync(CancellationToken cancellationToken)
        {
            ExecuteTaskExclusivelyAsyncCalled = true;
            ExecuteTaskExclusivelyAsyncCallCount++;

            if (ShouldThrowException != null)
            {
                throw ShouldThrowException;
            }

            return Task.CompletedTask;
        }

        public new Task DoExecuteTaskAsync(CancellationToken cancellationToken)
        {
            return base.DoExecuteTaskAsync(cancellationToken);
        }

        public new void OnBeforeStart()
        {
            base.OnBeforeStart();
        }
    }

    /// <summary>
    /// Тестовый класс с невалидным LeadershipLockId для тестирования валидации
    /// </summary>
    private class InvalidTestExclusiveHostedService : MoedeloExclusivePeriodicHostedService
    {
        public InvalidTestExclusiveHostedService(
            IMoedeloServiceLeadershipService leadershipService,
            IAuditTracer auditTracer,
            ILogger logger) : base(leadershipService, auditTracer, logger)
        {
        }

        protected override string LeadershipLockId => ""; // Невалидный пустой идентификатор

        protected override Task ExecuteTaskExclusivelyAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public new void OnBeforeStart()
        {
            base.OnBeforeStart();
        }
    }
} 