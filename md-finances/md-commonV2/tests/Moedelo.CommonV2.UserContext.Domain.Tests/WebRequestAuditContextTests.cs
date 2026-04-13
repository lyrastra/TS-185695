using System.Security.Claims;
using System.Web;
using FluentAssertions;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moq;
using NUnit.Framework;

namespace Moedelo.CommonV2.UserContext.Domain.Tests;

[TestFixture]
[Parallelizable(ParallelScope.None)]
public class WebRequestAuditContextTests
{
    private AppAutoDiInstaller diInstaller = null!;
    private readonly Mock<ILogger> loggerMock = new();

    [SetUp]
    public void SetUp()
    {
        diInstaller = new AppAutoDiInstaller(loggerMock.Object);
        diInstaller.Initialize();
    }

    [Test(Description = "Берёт значения из пользовательских claims из HttpContext.Current")]
    public void Test1()
    {
        var firmId = TestContext.CurrentContext.Random.Next();
        var userId = TestContext.CurrentContext.Random.Next();

        HttpContext.Current = new HttpContext(new HttpRequest(null, "http://moedelo.org", null), new HttpResponse(null))
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(MoedeloClaimsTypes.FirmId, firmId.ToString()),
                new Claim(MoedeloClaimsTypes.UserId, userId.ToString())
            }))
        };
        
        using var scope = diInstaller.BeginScope();

        diInstaller.GetInstance<IWebRequestAuditContext>()
            .Should().BeEquivalentTo(new { FirmId = firmId, UserId = userId });
    }
}
