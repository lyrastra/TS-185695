using System;
using FluentAssertions;
using Moedelo.CommonV2.Utils.ServerUrl;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moq;
using NUnit.Framework;

namespace Moedelo.CommonV2.Utils.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class ServerUriHelperTest
{
    private Mock<ISettingRepository> settingRepository;
    private IServerUriService serverUriService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        settingRepository = new Mock<ISettingRepository>();
        serverUriService = new ServerUriService(settingRepository.Object);
    }

    [Test]
    [TestCase("http://localhost:4303", "http://localhost:5646")]
    [TestCase("https://www.moedelo.org", "https://oauth.moedelo.org")]
    [TestCase("https://www67.moedelo.org", "https://oauth67.moedelo.org")]
    [TestCase("https://public.moedelo.org", "https://oauth.moedelo.org")]
    [TestCase("https://www.sberbank-mbo1.ru", "https://oauth.sberbank-mbo1.ru")]
    [TestCase("https://delo.moedelo.org", "https://oauth-delo.moedelo.org")]
    [TestCase("https://box15.mdtest.org", "https://oauth-box15.mdtest.org")]
    [TestCase("https://sberbank-mbo1.mdtest.org", "https://oauth-sberbank-mbo1.mdtest.org")]
    [TestCase("https://wl-sbersolutions-stage.mdtest.org", "https://oauth-wl-sbersolutions-stage.mdtest.org")]
    [TestCase("https://stage.mdtest.org", "https://oauth-stage.mdtest.org")]
    [TestCase("https://wl-delo.mdtest.org", "https://oauth-wl-delo.mdtest.org")]
    [TestCase("https://public-box37.mdtest.org", "https://oauth-box37.mdtest.org")]
    [TestCase("https://public-stage.mdtest.org", "https://oauth-stage.mdtest.org")]
    [TestCase("https://private.moedelo.org", "https://oauth.moedelo.org")]
    [TestCase("http://private.moedelo.org", "https://oauth.moedelo.org")]
    [TestCase("http://org.moedelo.private", "https://oauth.moedelo.org")]
    [TestCase("http://private-stage.mdtest.org", "https://oauth-stage.mdtest.org")]
    [TestCase("http://org.mdtest.private-stage", "https://oauth-stage.mdtest.org")]
    [TestCase("http://private-box17.mdtest.org", "https://oauth-box17.mdtest.org")]
    [TestCase("http://org.mdtest.private-box17", "https://oauth-box17.mdtest.org")]
    [TestCase("http://restapi-stage.mdtest.org", "https://oauth-stage.mdtest.org")]
    [TestCase("http://restApi-box17.mdtest.org", "https://oauth-box17.mdtest.org")]
    public void GetAuthServerUrl_ReturnsProperUrl_IfProperUrlPassed(string uri, string expected)
    {
        var uriObject = new Uri(uri);
        var result = serverUriService.GetAuthServerUrl(uriObject);
        result.Should().Be(expected);
    }

    [Test]
    [TestCase("http://localhost:5646", "http://localhost:4303")]
    [TestCase("https://moedelo.org", "https://www.moedelo.org")]
    [TestCase("https://www.moedelo.org", "https://www.moedelo.org")]
    [TestCase("https://www73.moedelo.org", "https://www73.moedelo.org")]
    [TestCase("https://sso.moedelo.org", "https://www.moedelo.org")]
    [TestCase("https://sso73.moedelo.org", "https://www73.moedelo.org")]
    [TestCase("https://oauth.moedelo.org", "https://www.moedelo.org")]
    [TestCase("https://oauth73.moedelo.org", "https://www73.moedelo.org")]

    [TestCase("https://sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
    [TestCase("https://www.sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
    [TestCase("https://sso.sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
    [TestCase("https://oauth.sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
        
    [TestCase("https://mbo-sber-solutions.ru", "https://www.mbo-sber-solutions.ru")]
    [TestCase("https://www.mbo-sber-solutions.ru", "https://www.mbo-sber-solutions.ru")]
    [TestCase("https://sso.mbo-sber-solutions.ru", "https://www.mbo-sber-solutions.ru")]
    [TestCase("https://oauth.mbo-sber-solutions.ru", "https://www.mbo-sber-solutions.ru")]

    [TestCase("https://stage.mdtest.org", "https://stage.mdtest.org")]
    [TestCase("https://sso-stage.mdtest.org", "https://stage.mdtest.org")]
    [TestCase("https://oauth-stage.mdtest.org", "https://stage.mdtest.org")]

    [TestCase("https://sberbank-mbo1.mdtest.org", "https://sberbank-mbo1.mdtest.org")]
    [TestCase("https://sso-sberbank-mbo1.mdtest.org", "https://sberbank-mbo1.mdtest.org")]
    [TestCase("https://oauth-sberbank-mbo1.mdtest.org", "https://sberbank-mbo1.mdtest.org")]
        
    [TestCase("https://wl-sbersolutions-stage.mdtest.org", "https://wl-sbersolutions-stage.mdtest.org")]
    [TestCase("https://sso-wl-sbersolutions-stage.mdtest.org", "https://wl-sbersolutions-stage.mdtest.org")]
    [TestCase("https://oauth-wl-sbersolutions-stage.mdtest.org", "https://wl-sbersolutions-stage.mdtest.org")]

    [TestCase("https://box15.mdtest.org", "https://box15.mdtest.org")]
    [TestCase("https://sso-box15.mdtest.org", "https://box15.mdtest.org")]
    [TestCase("https://oauth-box15.mdtest.org", "https://box15.mdtest.org")]
    public void GetBaseServerDomain_ReturnsProperDomain_IfProperUrlPassed(string uri, string expected)
    {
        var uriObject = new Uri(uri);
        var result = serverUriService.GetBaseUrl(uriObject);

        result.Should().Be(expected);
    }

    [Test]
    [TestCase("http://localhost:4303", "http://localhost:8351")]
    [TestCase("https://www.moedelo.org", "https://www.moedelo.org")]
    [TestCase("https://www.sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
    [TestCase("https://www.mbo-sber-solutions.ru", "https://www.mbo-sber-solutions.ru")]
    [TestCase("https://delo.moedelo.org", "https://delo.moedelo.org")]
    [TestCase("https://box15.mdtest.org", "https://box15.mdtest.org")]
    [TestCase("https://sberbank-mbo1.mdtest.org", "https://sberbank-mbo1.mdtest.org")]
    [TestCase("https://stage.mdtest.org", "https://stage.mdtest.org")]
    [TestCase("https://wl-delo.mdtest.org", "https://wl-delo.mdtest.org")]
    public void GetPromoBaseUrl_ReturnProperUrl_IfProperUrlPassed(string uri, string expected)
    {
        var uriObject = new Uri(uri);
        var result = serverUriService.GetPromoBaseUrl(uriObject);

        result.Should().Be(expected);
    }

    [Test]
    [TestCase("https://moedelo.org", "https://sso.moedelo.org")]
    [TestCase("https://www.moedelo.org", "https://sso.moedelo.org")]
    [TestCase("https://www73.moedelo.org", "https://sso73.moedelo.org")]
    [TestCase("https://sso.moedelo.org", "https://sso.moedelo.org")]
    [TestCase("https://sso73.moedelo.org", "https://sso73.moedelo.org")]
    [TestCase("https://oauth.moedelo.org", "https://sso.moedelo.org")]
    [TestCase("https://oauth73.moedelo.org", "https://sso73.moedelo.org")]

    [TestCase("https://sberbank-mbo1.ru", "https://sso.sberbank-mbo1.ru")]
    [TestCase("https://www.sberbank-mbo1.ru", "https://sso.sberbank-mbo1.ru")]
    [TestCase("https://sso.sberbank-mbo1.ru", "https://sso.sberbank-mbo1.ru")]
    [TestCase("https://oauth.sberbank-mbo1.ru", "https://sso.sberbank-mbo1.ru")]
        
    [TestCase("https://mbo-sber-solutions.ru", "https://sso.mbo-sber-solutions.ru")]
    [TestCase("https://www.mbo-sber-solutions.ru", "https://sso.mbo-sber-solutions.ru")]
    [TestCase("https://sso.mbo-sber-solutions.ru", "https://sso.mbo-sber-solutions.ru")]
    [TestCase("https://oauth.mbo-sber-solutions.ru", "https://sso.mbo-sber-solutions.ru")]

    [TestCase("https://stage.mdtest.org", "https://sso-stage.mdtest.org")]
    [TestCase("https://sso-stage.mdtest.org", "https://sso-stage.mdtest.org")]
    [TestCase("https://oauth-stage.mdtest.org", "https://sso-stage.mdtest.org")]

    [TestCase("https://sberbank-mbo1.mdtest.org", "https://sso-sberbank-mbo1.mdtest.org")]
    [TestCase("https://sso-sberbank-mbo1.mdtest.org", "https://sso-sberbank-mbo1.mdtest.org")]
    [TestCase("https://oauth-sberbank-mbo1.mdtest.org", "https://sso-sberbank-mbo1.mdtest.org")]
        
    [TestCase("https://wl-sbersolutions-stage.mdtest.org", "https://sso-wl-sbersolutions-stage.mdtest.org")]
    [TestCase("https://sso-wl-sbersolutions-stage.mdtest.org", "https://sso-wl-sbersolutions-stage.mdtest.org")]
    [TestCase("https://oauth-wl-sbersolutions-stage.mdtest.org", "https://sso-wl-sbersolutions-stage.mdtest.org")]

    [TestCase("https://box15.mdtest.org", "https://sso-box15.mdtest.org")]
    [TestCase("https://sso-box15.mdtest.org", "https://sso-box15.mdtest.org")]
    [TestCase("https://oauth-box15.mdtest.org", "https://sso-box15.mdtest.org")]
    public void GetSsoUrl_ReturnsProperUrl_IfProperUrlPassed(string uri, string expected)
    {
        var uriObject = new Uri(uri);
        var result = serverUriService.GetSsoUrl(uriObject);

        result.Should().Be(expected);
    }      
}