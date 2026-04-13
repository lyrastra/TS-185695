using System.Threading.Tasks;
using FluentAssertions;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moq;
using NUnit.Framework;

namespace Moedelo.CommonV2.UserContext.Domain.Tests;

[TestFixture]
public class DiResolverExtensionsTests
{
    private AppAutoDiInstaller diInstaller = null!;
    private readonly Mock<ILogger> loggerMock = new ();

    [SetUp]
    public void SetUp()
    {
        diInstaller = new AppAutoDiInstaller(loggerMock.Object);
        diInstaller.Initialize();
    }

    [Test]
    public async Task ExecuteInCustomUserContextAsync_CanBeInstantiated()
    {
        const int firmId = 789;
        const int userId = 567;

        var executedWihFirmId = 0;
        var executedWihUserId = 0;
        await diInstaller.ExecuteInCustomUserContextAsync<IUserContext>(firmId, userId,
            userContext =>
            {
                executedWihFirmId = userContext.FirmId;
                executedWihUserId = userContext.UserId;

                return Task.CompletedTask;
            }, continueOnCapturedContext: false);

        executedWihFirmId.Should().Be(firmId);
        executedWihUserId.Should().Be(userId);
    }
    
    [Test]
    public async Task ExecuteInCustomUserContextAsync_CleanCustomValuesOutOfScope()
    {
        const int firmId = 789;
        const int userId = 567;

        await diInstaller.ExecuteInCustomUserContextAsync<IUserContext>(firmId, userId,
            _ => Task.CompletedTask, continueOnCapturedContext: false);

        using var scope = diInstaller.BeginScope();

        var expected = new
        {
            FirmId = (int?)null,
            UserId = (int?)null
        };
        var customAuditContext = diInstaller.GetInstance<ICustomAuditContext>();

        customAuditContext.Should().BeEquivalentTo(expected);
    }
}