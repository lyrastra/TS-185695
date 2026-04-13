using FluentAssertions;
using Moedelo.Common.ExecutionContext.Client.Tests.Extensions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moq;
using NUnit.Framework.Internal;

namespace Moedelo.Common.ExecutionContext.Client.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
public class ExecutionContextApiCallerTests
{
    private IExecutionContextApiCaller apiCaller = null!;
    private Mock<IHttpRequestExecuter> httpRequestExecutor = null!;
    private Mock<IAuditHeadersGetter> auditHeadersGetter = null!;
    private Mock<IExecutionContextApiClientSettings> apiClientSettings = null!;

    private Randomizer TestRandom => TestContext.CurrentContext.Random;
    private ExecutionContextApiMethod RandomApiMethod => TestRandom.NextEnum<ExecutionContextApiMethod>();

    [SetUp]
    public void Setup()
    {
        httpRequestExecutor = new();
        auditHeadersGetter = new();
        apiClientSettings = new();
        apiClientSettings.SetupGet(settings => settings.RetryDelay).Returns(TimeSpan.Zero);
        apiClientSettings.Setup(settings => settings.GetApiMethodUri(It.IsAny<ExecutionContextApiMethod>()))
            .Returns(TestRandom.GetString());

        apiCaller = new ExecutionContextApiCaller(
            httpRequestExecutor.Object,
            auditHeadersGetter.Object,
            apiClientSettings.Object);
    }

    #region PostWithRetryAsync without body

    [Test]
    public async ValueTask PostWithRetryAsync_CallsGetHeaders()
    {
        await apiCaller.PostWithRetryAsync(RandomApiMethod, CancellationToken.None);

        auditHeadersGetter.Verify(getter => getter.GetHeaders(), Times.AtLeastOnce);
    }

    [Test]
    public async ValueTask PostWithRetryAsync_CallsGetHeadersOnce_EvenRetryIsEnabledAndRequired(
        [Values(2, 3, 4)] int retryCount)
    {
        apiClientSettings.SetupGet(settings => settings.RetryCount).Returns(retryCount);
        httpRequestExecutor
            .Setup(executor => executor
                .PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<KeyValuePair<string, string>>>(),
                    It.IsAny<HttpQuerySetting>(),
                    It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("UnitTest!"));

        await apiCaller
            .PostWithRetryAsync(RandomApiMethod, CancellationToken.None)
            .IgnoreExceptions();

        auditHeadersGetter.Verify(getter => getter.GetHeaders(), Times.Once);
    }

    [Test]
    public async ValueTask PostWithRetryAsync_CallsExecutorMultipleTimes_IfRetryIsEnabledAndRequired(
        [Values(2, 3, 4)] int retryCount)
    {
        apiClientSettings.SetupGet(settings => settings.RetryCount).Returns(retryCount);
        httpRequestExecutor
            .Setup(executor => executor
                .PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<KeyValuePair<string, string>>>(),
                    It.IsAny<HttpQuerySetting>(),
                    It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("UnitTest!"));

        await apiCaller
            .PostWithRetryAsync(RandomApiMethod, CancellationToken.None)
            .IgnoreExceptions();

        httpRequestExecutor.Verify(executor => executor
                .PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<KeyValuePair<string, string>>>(),
                    It.IsAny<HttpQuerySetting>(),
                    It.IsAny<CancellationToken>()),
            Times.Exactly(retryCount));
    }

    [Test(Description = "не повторяет попытки, если токен отозван")]
    public async ValueTask PostWithRetryAsync_CallsExecutorOnce_IfTokenIsCancelled(
        [Values(2, 3, 4)] int retryCount)
    {
        using var cancellation = new CancellationTokenSource();

        apiClientSettings.SetupGet(settings => settings.RetryCount).Returns(retryCount);
        httpRequestExecutor
            .Setup(executor => executor
                .PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<KeyValuePair<string, string>>>(),
                    It.IsAny<HttpQuerySetting>(),
                    It.IsAny<CancellationToken>()))
            .Callback<string, IReadOnlyCollection<KeyValuePair<string, string>>, HttpQuerySetting, CancellationToken>(
                (uri, headers, setting, cancellationToken) =>
                {
                    // ReSharper disable once AccessToDisposedClosure
                    cancellation.Cancel();
                    throw new Exception("UnitTest!!!");
                });

        await apiCaller
            .PostWithRetryAsync(RandomApiMethod, cancellation.Token)
            .IgnoreExceptions();

        httpRequestExecutor.Verify(executor => executor
                .PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<KeyValuePair<string, string>>>(),
                    It.IsAny<HttpQuerySetting>(),
                    It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async ValueTask PostWithRetryAsync_ResolvesUriViaSettings()
    {
        var apiMethod = RandomApiMethod;
        await apiCaller.PostWithRetryAsync(apiMethod, CancellationToken.None);

        apiClientSettings.Verify(settings => settings.GetApiMethodUri(apiMethod),
            Times.Once);
    }

    [Test]
    public async ValueTask PostWithRetryAsync_CallsPostWithExpectedParams()
    {
        var apiMethod = RandomApiMethod;
        var expectedUri = TestRandom.GetString();
        var expectedHeaders = new[]
        {
            new KeyValuePair<string, string>(TestRandom.GetString(), TestRandom.GetString())
        };

        apiClientSettings.Setup(settings => settings.GetApiMethodUri(apiMethod))
            .Returns(expectedUri);
        auditHeadersGetter.Setup(getter => getter.GetHeaders())
            .Returns(expectedHeaders);

        await apiCaller.PostWithRetryAsync(apiMethod, CancellationToken.None);

        httpRequestExecutor.Verify(executor => executor.PostAsync(
            expectedUri, expectedHeaders, null, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async ValueTask PostWithRetryAsync_ReturnsPostAsyncResult()
    {
        var apiMethod = RandomApiMethod;
        var expected = TestRandom.GetString();

        httpRequestExecutor
            .Setup(executor => executor
                .PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<KeyValuePair<string, string>>>(),
                    It.IsAny<HttpQuerySetting>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var actual = await apiCaller.PostWithRetryAsync(apiMethod, CancellationToken.None);

        actual.Should().Be(expected);
    }

    #endregion
}
