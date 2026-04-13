using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moedelo.CommonV2.Utils.ServerUrl;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moq;
using NUnit.Framework;
using AssetNUnit = NUnit.Framework.Assert;

namespace UtilsTests
{
    [TestFixture]
    public class ServerUriHelperTest
    {
        private Mock<ISettingRepository> settingRepository;
        private IServerUriService serverUriService;

        [SetUp]
        public void Setup()
        {
            settingRepository = new Mock<ISettingRepository>();
            serverUriService = new ServerUriService(settingRepository.Object);
        }

        [TestMethod]
        [TestCase("http://localhost:4303", "http://localhost:5646")]
        [TestCase("https://www.moedelo.org", "https://oauth.moedelo.org")]
        [TestCase("https://www67.moedelo.org", "https://oauth67.moedelo.org")]
        [TestCase("https://public.moedelo.org", "https://oauth.moedelo.org")]
        [TestCase("https://www.sberbank-mbo1.ru", "https://oauth.sberbank-mbo1.ru")]
        [TestCase("https://delo.moedelo.org", "https://oauth-delo.moedelo.org")]
        [TestCase("https://box15.mdtest.org", "https://oauth-box15.mdtest.org")]
        [TestCase("https://sberbank-mbo1.mdtest.org", "https://oauth-sberbank-mbo1.mdtest.org")]
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
        [TestCase("https://sso-rnkb.moedelo.org", "https://oauth-rnkb.moedelo.org")]
        public void GetAuthServerUrl_ReturnsProperUrl_IfProperUrlPassed(string uri, string expected)
        {
            Uri uriObject = new Uri(uri);
            string result = serverUriService.GetAuthServerUrl(uriObject);
            AssetNUnit.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCase("http://localhost:5646", "http://localhost:4303")]
        [TestCase("https://moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://www.moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://www73.moedelo.org", "https://www73.moedelo.org")]
        [TestCase("https://sso.moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://sso73.moedelo.org", "https://www73.moedelo.org")]
        [TestCase("https://oauth.moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://oauth73.moedelo.org", "https://www73.moedelo.org")]
        [TestCase("https://rnkb.moedelo.org", "https://rnkb.moedelo.org")]
        [TestCase("https://rnkb73.moedelo.org", "https://rnkb73.moedelo.org")]
        [TestCase("https://sso-rnkb.moedelo.org", "https://rnkb.moedelo.org")]
        [TestCase("https://sso73-rnkb.moedelo.org", "https://rnkb73.moedelo.org")]
        [TestCase("https://oauth-rnkb.moedelo.org", "https://rnkb.moedelo.org")]
        [TestCase("https://oauth73-rnkb.moedelo.org", "https://rnkb73.moedelo.org")]

        [TestCase("https://sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
        [TestCase("https://www.sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
        [TestCase("https://sso.sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
        [TestCase("https://oauth.sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]

        [TestCase("https://stage.mdtest.org", "https://stage.mdtest.org")]
        [TestCase("https://sso-stage.mdtest.org", "https://stage.mdtest.org")]
        [TestCase("https://oauth-stage.mdtest.org", "https://stage.mdtest.org")]
        [TestCase("https://wl-rnkb-stage.mdtest.org", "https://wl-rnkb-stage.mdtest.org")]
        [TestCase("https://sso-wl-rnkb-stage.mdtest.org", "https://wl-rnkb-stage.mdtest.org")]
        [TestCase("https://oauth-wl-rnkb-stage.mdtest.org", "https://wl-rnkb-stage.mdtest.org")]

        [TestCase("https://sberbank-mbo1.mdtest.org", "https://sberbank-mbo1.mdtest.org")]
        [TestCase("https://sso-sberbank-mbo1.mdtest.org", "https://sberbank-mbo1.mdtest.org")]
        [TestCase("https://oauth-sberbank-mbo1.mdtest.org", "https://sberbank-mbo1.mdtest.org")]

        [TestCase("https://box15.mdtest.org", "https://box15.mdtest.org")]
        [TestCase("https://sso-box15.mdtest.org", "https://box15.mdtest.org")]
        [TestCase("https://oauth-box15.mdtest.org", "https://box15.mdtest.org")]
        [TestCase("https://wl-rnkb-box56.mdtest.org", "https://wl-rnkb-box56.mdtest.org")]
        [TestCase("https://sso-wl-rnkb-box56.mdtest.org", "https://wl-rnkb-box56.mdtest.org")]
        [TestCase("https://oauth-wl-rnkb-box56.mdtest.org", "https://wl-rnkb-box56.mdtest.org")]
        public void GetBaseServerDomain_ReturnsProperDomain_IfProperUrlPassed(string uri, string expected)
        {
            Uri uriObject = new Uri(uri);
            string result = serverUriService.GetBaseUrl(uriObject);
            AssetNUnit.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCase("http://localhost:4303", "http://localhost:8351")]
        [TestCase("https://www.moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://www.sberbank-mbo1.ru", "https://www.sberbank-mbo1.ru")]
        [TestCase("https://delo.moedelo.org", "https://delo.moedelo.org")]
        [TestCase("https://box15.mdtest.org", "https://box15.mdtest.org")]
        [TestCase("https://sberbank-mbo1.mdtest.org", "https://sberbank-mbo1.mdtest.org")]
        [TestCase("https://stage.mdtest.org", "https://stage.mdtest.org")]
        [TestCase("https://wl-delo.mdtest.org", "https://wl-delo.mdtest.org")]
        public void GetPromoBaseUrl_ReturnProperUrl_IfProperUrlPassed(string uri, string expected)
        {
            Uri uriObject = new Uri(uri);
            string result = serverUriService.GetPromoBaseUrl(uriObject);
            AssetNUnit.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCase("https://moedelo.org", "https://sso.moedelo.org")]
        [TestCase("https://www.moedelo.org", "https://sso.moedelo.org")]
        [TestCase("https://www73.moedelo.org", "https://sso73.moedelo.org")]
        [TestCase("https://sso.moedelo.org", "https://sso.moedelo.org")]
        [TestCase("https://sso73.moedelo.org", "https://sso73.moedelo.org")]
        [TestCase("https://oauth.moedelo.org", "https://sso.moedelo.org")]
        [TestCase("https://oauth73.moedelo.org", "https://sso73.moedelo.org")]
        [TestCase("https://rnkb.moedelo.org", "https://sso-rnkb.moedelo.org")]
        [TestCase("https://rnkb73.moedelo.org", "https://sso-rnkb73.moedelo.org")]
        [TestCase("https://sso-rnkb.moedelo.org", "https://sso-rnkb.moedelo.org")]
        [TestCase("https://sso73-rnkb.moedelo.org", "https://sso-rnkb73.moedelo.org")]
        [TestCase("https://oauth-rnkb.moedelo.org", "https://sso-rnkb.moedelo.org")]
        [TestCase("https://oauth73-rnkb.moedelo.org", "https://sso-rnkb73.moedelo.org")]

        [TestCase("https://sberbank-mbo1.ru", "https://sso.sberbank-mbo1.ru")]
        [TestCase("https://www.sberbank-mbo1.ru", "https://sso.sberbank-mbo1.ru")]
        [TestCase("https://sso.sberbank-mbo1.ru", "https://sso.sberbank-mbo1.ru")]
        [TestCase("https://oauth.sberbank-mbo1.ru", "https://sso.sberbank-mbo1.ru")]

        [TestCase("https://stage.mdtest.org", "https://sso-stage.mdtest.org")]
        [TestCase("https://sso-stage.mdtest.org", "https://sso-stage.mdtest.org")]
        [TestCase("https://oauth-stage.mdtest.org", "https://sso-stage.mdtest.org")]
        [TestCase("https://wl-rnkb-stage.mdtest.org", "https://sso-wl-rnkb-stage.mdtest.org")]
        [TestCase("https://sso-wl-rnkb-stage.mdtest.org", "https://sso-wl-rnkb-stage.mdtest.org")]
        [TestCase("https://oauth-wl-rnkb-stage.mdtest.org", "https://sso-wl-rnkb-stage.mdtest.org")]

        [TestCase("https://sberbank-mbo1.mdtest.org", "https://sso-sberbank-mbo1.mdtest.org")]
        [TestCase("https://sso-sberbank-mbo1.mdtest.org", "https://sso-sberbank-mbo1.mdtest.org")]
        [TestCase("https://oauth-sberbank-mbo1.mdtest.org", "https://sso-sberbank-mbo1.mdtest.org")]

        [TestCase("https://box15.mdtest.org", "https://sso-box15.mdtest.org")]
        [TestCase("https://sso-box15.mdtest.org", "https://sso-box15.mdtest.org")]
        [TestCase("https://oauth-box15.mdtest.org", "https://sso-box15.mdtest.org")]
        [TestCase("https://wl-rnkb-box56.mdtest.org", "https://sso-wl-rnkb-box56.mdtest.org")]
        [TestCase("https://sso-wl-rnkb-box56.mdtest.org", "https://sso-wl-rnkb-box56.mdtest.org")]
        [TestCase("https://oauth-wl-rnkb-box56.mdtest.org", "https://sso-wl-rnkb-box56.mdtest.org")]
        public void GetSsoUrl_ReturnsProperUrl_IfProperUrlPassed(string uri, string expected)
        {
            Uri uriObject = new Uri(uri);
            string result = serverUriService.GetSsoUrl(uriObject);
            AssetNUnit.AreEqual(expected, result);
        }      
    }
}
