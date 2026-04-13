using FluentAssertions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moq;
using NUnit.Framework.Internal;

namespace Moedelo.Common.ExecutionContext.Client.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
public class ExecutionContextApiClientTests
{
    private IExecutionContextApiClient apiClient = null!;
    private Mock<IExecutionContextApiCaller> apiCaller = null!;
    private Mock<IApiKeyTokenDigestCalculator> digestCalculator = null!;

    private Randomizer TestRandom => TestContext.CurrentContext.Random;

    [SetUp]
    public void SetUp()
    {
        apiCaller = new();
        digestCalculator = new();

        apiClient = new ExecutionContextApiClient(apiCaller.Object, digestCalculator.Object);
    }

    [Test]
    public async ValueTask GetTokenFromPublicAsync_CallsCallerWithProperApiMethod()
    {
        await apiClient.GetTokenFromPublicAsync(
            TestRandom.GetString(),
            TestRandom.Next(),
            CancellationToken.None);

        apiCaller.Verify(caller => caller
                    .PostWithRetryAsync(
                        ExecutionContextApiMethod.FromPublic,
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Test]
    public async ValueTask GetTokenFromPublicAsync_ReturnsResultOfCallerMethod()
    {
        var expected = TestRandom.GetString();
        apiCaller.Setup(caller => caller
                .PostWithRetryAsync(
                    It.IsAny<ExecutionContextApiMethod>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        var actual = await apiClient.GetTokenFromPublicAsync(
            TestRandom.GetString(),
            TestRandom.Next(),
            CancellationToken.None);

        actual.Should().Be(expected);
    }

    [Test]
    public async ValueTask GetTokenFromUserContextAsync_CallsCallerWithProperApiMethod()
    {
        await apiClient.GetTokenFromUserContextAsync(
            TestRandom.Next(),
            TestRandom.Next(),
            CancellationToken.None);

        apiCaller.Verify(caller => caller
                    .PostWithRetryAsync(
                        ExecutionContextApiMethod.FromUserContext,
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Test]
    public async ValueTask GetTokenFromUserContextAsync_ReturnsResultOfCallerMethod()
    {
        var expected = TestRandom.GetString();
        apiCaller.Setup(caller => caller
                .PostWithRetryAsync(
                    It.IsAny<ExecutionContextApiMethod>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        var actual = await apiClient.GetTokenFromUserContextAsync(
            TestRandom.Next(),
            TestRandom.Next(),
            CancellationToken.None);

        actual.Should().Be(expected);
    }

    [Test]
    public async ValueTask GetTokenFromApiKeyAsync_CallsCallerWithProperApiMethod()
    {
        await apiClient.GetTokenFromApiKeyAsync(
            TestRandom.GetString(),
            CancellationToken.None);

        apiCaller.Verify(caller => caller
                .PostWithRetryAsync(
                    ExecutionContextApiMethod.FromApiKey,
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async ValueTask GetTokenFromApiKeyAsync_ReturnsResultOfCallerMethod()
    {
        var expected = TestRandom.GetString();
        apiCaller.Setup(caller => caller
                .PostWithRetryAsync(
                    It.IsAny<ExecutionContextApiMethod>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        var actual = await apiClient.GetTokenFromApiKeyAsync(
            TestRandom.GetString(),
            CancellationToken.None);

        actual.Should().Be(expected);
    }

    [Test]
    public async ValueTask GetUnidentifiedTokenAsync_CallsCallerWithProperApiMethod()
    {
        await apiClient.GetUnidentifiedTokenAsync(CancellationToken.None);

        apiCaller.Verify(caller => caller
                .PostWithRetryAsync(
                    ExecutionContextApiMethod.Unidentified,
                    It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async ValueTask GetUnidentifiedTokenAsync_ReturnsResultOfCallerMethod()
    {
        var expected = TestRandom.GetString();
        apiCaller.Setup(caller => caller
                .PostWithRetryAsync(
                    It.IsAny<ExecutionContextApiMethod>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        var actual = await apiClient.GetUnidentifiedTokenAsync(CancellationToken.None);

        actual.Should().Be(expected);
    }
}
