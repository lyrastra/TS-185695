using Moedelo.Common.Utils.ServerUrl;
using NUnit.Framework;

namespace Moedelo.Common.Utils.Tests
{
    [TestFixture]
    public class DomainServiceTests
    {
        [TestCase("moedelo.org", "www.moedelo.org")]
        [TestCase("www.moedelo.org", "www.moedelo.org")]
        [TestCase("www73.moedelo.org", "www73.moedelo.org")]
        [TestCase("sso.moedelo.org", "www.moedelo.org")]
        [TestCase("sso73.moedelo.org", "www73.moedelo.org")]
        [TestCase("oauth.moedelo.org", "www.moedelo.org")]
        [TestCase("oauth73.moedelo.org", "www73.moedelo.org")]
        [TestCase("rnkb.moedelo.org", "rnkb.moedelo.org")]
        [TestCase("rnkb73.moedelo.org", "rnkb73.moedelo.org")]
        [TestCase("sso-rnkb.moedelo.org", "rnkb.moedelo.org")]
        [TestCase("sso73-rnkb.moedelo.org", "rnkb73.moedelo.org")]
        [TestCase("oauth-rnkb.moedelo.org", "rnkb.moedelo.org")]
        [TestCase("oauth73-rnkb.moedelo.org", "rnkb73.moedelo.org")]

        [TestCase("sberbank-mbo1.ru", "www.sberbank-mbo1.ru")]
        [TestCase("www.sberbank-mbo1.ru", "www.sberbank-mbo1.ru")]
        [TestCase("sso.sberbank-mbo1.ru", "www.sberbank-mbo1.ru")]
        [TestCase("oauth.sberbank-mbo1.ru", "www.sberbank-mbo1.ru")]

        [TestCase("stage.mdtest.org", "stage.mdtest.org")]
        [TestCase("sso-stage.mdtest.org", "stage.mdtest.org")]
        [TestCase("oauth-stage.mdtest.org", "stage.mdtest.org")]
        [TestCase("wl-rnkb-stage.mdtest.org", "wl-rnkb-stage.mdtest.org")]
        [TestCase("sso-wl-rnkb-stage.mdtest.org", "wl-rnkb-stage.mdtest.org")]
        [TestCase("oauth-wl-rnkb-stage.mdtest.org", "wl-rnkb-stage.mdtest.org")]

        [TestCase("sberbank-mbo1.mdtest.org", "sberbank-mbo1.mdtest.org")]
        [TestCase("sso-sberbank-mbo1.mdtest.org", "sberbank-mbo1.mdtest.org")]
        [TestCase("oauth-sberbank-mbo1.mdtest.org", "sberbank-mbo1.mdtest.org")]

        [TestCase("box15.mdtest.org", "box15.mdtest.org")]
        [TestCase("sso-box15.mdtest.org", "box15.mdtest.org")]
        [TestCase("oauth-box15.mdtest.org", "box15.mdtest.org")]
        [TestCase("wl-rnkb-box56.mdtest.org", "wl-rnkb-box56.mdtest.org")]
        [TestCase("sso-wl-rnkb-box56.mdtest.org", "wl-rnkb-box56.mdtest.org")]
        [TestCase("oauth-wl-rnkb-box56.mdtest.org", "wl-rnkb-box56.mdtest.org")]
        public void GetBaseUrl_ReturnsBaseUrl(string url, string expected)
        {
            string result = DomainService.GetBaseDomain(url);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("moedelo.org", "sso.moedelo.org")]
        [TestCase("www.moedelo.org", "sso.moedelo.org")]
        [TestCase("www73.moedelo.org", "sso73.moedelo.org")]
        [TestCase("sso.moedelo.org", "sso.moedelo.org")]
        [TestCase("sso73.moedelo.org", "sso73.moedelo.org")]
        [TestCase("oauth.moedelo.org", "sso.moedelo.org")]
        [TestCase("oauth73.moedelo.org", "sso73.moedelo.org")]
        [TestCase("rnkb.moedelo.org", "sso-rnkb.moedelo.org")]
        [TestCase("rnkb73.moedelo.org", "sso-rnkb73.moedelo.org")]
        [TestCase("sso-rnkb.moedelo.org", "sso-rnkb.moedelo.org")]
        [TestCase("sso73-rnkb.moedelo.org", "sso-rnkb73.moedelo.org")]
        [TestCase("oauth-rnkb.moedelo.org", "sso-rnkb.moedelo.org")]
        [TestCase("oauth73-rnkb.moedelo.org", "sso-rnkb73.moedelo.org")]

        [TestCase("sberbank-mbo1.ru", "sso.sberbank-mbo1.ru")]
        [TestCase("www.sberbank-mbo1.ru", "sso.sberbank-mbo1.ru")]
        [TestCase("sso.sberbank-mbo1.ru", "sso.sberbank-mbo1.ru")]
        [TestCase("oauth.sberbank-mbo1.ru", "sso.sberbank-mbo1.ru")]

        [TestCase("stage.mdtest.org", "sso-stage.mdtest.org")]
        [TestCase("sso-stage.mdtest.org", "sso-stage.mdtest.org")]
        [TestCase("oauth-stage.mdtest.org", "sso-stage.mdtest.org")]
        [TestCase("wl-rnkb-stage.mdtest.org", "sso-wl-rnkb-stage.mdtest.org")]
        [TestCase("sso-wl-rnkb-stage.mdtest.org", "sso-wl-rnkb-stage.mdtest.org")]
        [TestCase("oauth-wl-rnkb-stage.mdtest.org", "sso-wl-rnkb-stage.mdtest.org")]

        [TestCase("sberbank-mbo1.mdtest.org", "sso-sberbank-mbo1.mdtest.org")]
        [TestCase("sso-sberbank-mbo1.mdtest.org", "sso-sberbank-mbo1.mdtest.org")]
        [TestCase("oauth-sberbank-mbo1.mdtest.org", "sso-sberbank-mbo1.mdtest.org")]

        [TestCase("box15.mdtest.org", "sso-box15.mdtest.org")]
        [TestCase("sso-box15.mdtest.org", "sso-box15.mdtest.org")]
        [TestCase("oauth-box15.mdtest.org", "sso-box15.mdtest.org")]
        [TestCase("wl-rnkb-box56.mdtest.org", "sso-wl-rnkb-box56.mdtest.org")]
        [TestCase("sso-wl-rnkb-box56.mdtest.org", "sso-wl-rnkb-box56.mdtest.org")]
        [TestCase("oauth-wl-rnkb-box56.mdtest.org", "sso-wl-rnkb-box56.mdtest.org")]
        public void GetSsoUrl(string url, string expected)
        {
            string result = DomainService.GetSsoDomain(url);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("moedelo.org", "oauth.moedelo.org")]
        [TestCase("www.moedelo.org", "oauth.moedelo.org")]
        [TestCase("www73.moedelo.org", "oauth73.moedelo.org")]
        [TestCase("sso.moedelo.org", "oauth.moedelo.org")]
        [TestCase("sso73.moedelo.org", "oauth73.moedelo.org")]
        [TestCase("oauth.moedelo.org", "oauth.moedelo.org")]
        [TestCase("oauth73.moedelo.org", "oauth73.moedelo.org")]
        [TestCase("rnkb.moedelo.org", "oauth-rnkb.moedelo.org")]
        [TestCase("rnkb73.moedelo.org", "oauth-rnkb73.moedelo.org")]
        [TestCase("sso-rnkb.moedelo.org", "oauth-rnkb.moedelo.org")]
        [TestCase("sso73-rnkb.moedelo.org", "oauth-rnkb73.moedelo.org")]
        [TestCase("oauth-rnkb.moedelo.org", "oauth-rnkb.moedelo.org")]
        [TestCase("oauth73-rnkb.moedelo.org", "oauth-rnkb73.moedelo.org")]

        [TestCase("sberbank-mbo1.ru", "oauth.sberbank-mbo1.ru")]
        [TestCase("www.sberbank-mbo1.ru", "oauth.sberbank-mbo1.ru")]
        [TestCase("sso.sberbank-mbo1.ru", "oauth.sberbank-mbo1.ru")]
        [TestCase("oauth.sberbank-mbo1.ru", "oauth.sberbank-mbo1.ru")]

        [TestCase("stage.mdtest.org", "oauth-stage.mdtest.org")]
        [TestCase("sso-stage.mdtest.org", "oauth-stage.mdtest.org")]
        [TestCase("oauth-stage.mdtest.org", "oauth-stage.mdtest.org")]
        [TestCase("wl-rnkb-stage.mdtest.org", "oauth-wl-rnkb-stage.mdtest.org")]
        [TestCase("sso-wl-rnkb-stage.mdtest.org", "oauth-wl-rnkb-stage.mdtest.org")]
        [TestCase("oauth-wl-rnkb-stage.mdtest.org", "oauth-wl-rnkb-stage.mdtest.org")]

        [TestCase("sberbank-mbo1.mdtest.org", "oauth-sberbank-mbo1.mdtest.org")]
        [TestCase("sso-sberbank-mbo1.mdtest.org", "oauth-sberbank-mbo1.mdtest.org")]
        [TestCase("oauth-sberbank-mbo1.mdtest.org", "oauth-sberbank-mbo1.mdtest.org")]

        [TestCase("box15.mdtest.org", "oauth-box15.mdtest.org")]
        [TestCase("sso-box15.mdtest.org", "oauth-box15.mdtest.org")]
        [TestCase("oauth-box15.mdtest.org", "oauth-box15.mdtest.org")]
        [TestCase("wl-rnkb-box56.mdtest.org", "oauth-wl-rnkb-box56.mdtest.org")]
        [TestCase("sso-wl-rnkb-box56.mdtest.org", "oauth-wl-rnkb-box56.mdtest.org")]
        [TestCase("oauth-wl-rnkb-box56.mdtest.org", "oauth-wl-rnkb-box56.mdtest.org")]
        public void GetOauthUrl(string url, string expected)
        {
            string result = DomainService.GetOauthDomain(url);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
