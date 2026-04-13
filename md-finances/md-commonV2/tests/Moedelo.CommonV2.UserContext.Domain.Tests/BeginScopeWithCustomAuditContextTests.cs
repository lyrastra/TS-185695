using FluentAssertions;
using Moedelo.InfrastructureV2.Domain.Extensions;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moq;
using NUnit.Framework;

namespace Moedelo.CommonV2.UserContext.Domain.Tests;

[TestFixture]
[Parallelizable(ParallelScope.None)]
public class BeginScopeWithCustomAuditContextTests
{
    private AppAutoDiInstaller diInstaller = null!;
    private readonly Mock<ILogger> loggerMock = new();

    [SetUp]
    public void SetUp()
    {
        diInstaller = new AppAutoDiInstaller(loggerMock.Object);
        diInstaller.Initialize();
    }

    [Test(Description = "Замена экземпляра типа IAuditContext в скоупе")]
    public void BeginScopeWithCustomAuditContext_ReturnsNewAuditContext()
    {
        const int firmId = 123;
        const int userId = 456;

        using var scope = diInstaller.BeginScopeWithCustomAuditContext(firmId, userId);

        diInstaller.GetInstance<IAuditContext>()
            .Should().BeEquivalentTo(new { FirmId = firmId, UserId = userId });
    }
    
    [Test(Description = "Замена экземпляра типа IAuditContext в следующем скоупе")]
    public void BeginScopeWithCustomAuditContext_ReturnsNewAuditContextInNotFirstScope()
    {
        const int firmId = 123;
        const int userId = 456;

        using (diInstaller.BeginScope())
        {
            var _ = diInstaller.GetInstance<IAuditContext>();
        }

        using(diInstaller.BeginScopeWithCustomAuditContext(firmId, userId))
        {
            var scoped = diInstaller.GetInstance<IAuditContext>();
            scoped.Should().BeEquivalentTo(new { FirmId = firmId, UserId = userId });
        }
    }

    [Test(Description = "Замена экземпляра типа IAuditContext не появляется в следующем скоупе")]
    public void BeginScopeWithCustomAuditContext_ReturnsNewAuditContextInNextScope()
    {
        const int firmId = 123;
        const int userId = 456;

        using (diInstaller.BeginScopeWithCustomAuditContext(firmId, userId))
        {
            // touch new instance
            var _ = diInstaller.GetInstance<IAuditContext>();
        }

        const int newFirmId = firmId + 10;
        const int newUserId = userId + 10;
        
        using var scope = diInstaller.BeginScopeWithCustomAuditContext(newFirmId, newUserId);

        diInstaller.GetInstance<IAuditContext>()
            .Should().BeEquivalentTo(new { FirmId = newFirmId, UserId = newUserId });
    }
}
