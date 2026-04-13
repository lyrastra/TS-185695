using Moedelo.Common.Utils.ServerUrl;
using NUnit.Framework;

namespace Moedelo.Common.Utils.Tests
{
    [TestFixture]
    public class ServerUriServiceTests
    {
        private IServerUriService serverUriService;

        [SetUp]
        public void Setup()
        {
            serverUriService = new ServerUriService();
        }

        [TestCase("https://localhost:4303", "http://localhost:4303")]
        [TestCase("http://localhost:4303", "http://localhost:4303")]
        [TestCase("http://localhost:55556", "http://localhost:4303")]
        [TestCase("http://localhost:5646", "http://localhost:4303")]
        [TestCase("http://wl-sber.localhost:4303", "http://wl-sber.localhost:4303")]
        [TestCase("http://www.moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://www.moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://sso.moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://oauth.moedelo.org", "https://www.moedelo.org")]
        [TestCase("https://sso-rnkb.moedelo.org", "https://rnkb.moedelo.org")]
        [TestCase("https://oauth-rnkb.moedelo.org", "https://rnkb.moedelo.org")]
        public void GetBaseUrl(string url, string expected)
        {
            var uri = new Uri(url);
            string result = serverUriService.GetBaseUrl(uri);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("https://localhost:4303", "http://localhost:55556")]
        [TestCase("http://localhost:4303", "http://localhost:55556")]
        [TestCase("http://localhost:55556", "http://localhost:55556")]
        [TestCase("http://localhost:5646", "http://localhost:55556")]
        [TestCase("http://wl-sber.localhost:4303", "http://wl-sber.localhost:55556")]
        [TestCase("http://www.moedelo.org", "https://sso.moedelo.org")]
        [TestCase("https://www.moedelo.org", "https://sso.moedelo.org")]
        [TestCase("https://sso.moedelo.org", "https://sso.moedelo.org")]
        [TestCase("https://oauth.moedelo.org", "https://sso.moedelo.org")]
        [TestCase("https://sso-rnkb.moedelo.org", "https://sso-rnkb.moedelo.org")]
        [TestCase("https://oauth-rnkb.moedelo.org", "https://sso-rnkb.moedelo.org")]
        public void GetSsoUrl(string url, string expected)
        {
            var uri = new Uri(url);
            string result = serverUriService.GetSsoUrl(uri);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("https://localhost:4303", "http://localhost:5646")]
        [TestCase("http://localhost:4303", "http://localhost:5646")]
        [TestCase("http://localhost:55556", "http://localhost:5646")]
        [TestCase("http://localhost:5646", "http://localhost:5646")]
        [TestCase("http://wl-sber.localhost:4303", "http://wl-sber.localhost:5646")]
        [TestCase("http://www.moedelo.org", "https://oauth.moedelo.org")]
        [TestCase("https://www.moedelo.org", "https://oauth.moedelo.org")]
        [TestCase("https://sso.moedelo.org", "https://oauth.moedelo.org")]
        [TestCase("https://oauth.moedelo.org", "https://oauth.moedelo.org")]
        [TestCase("https://sso-rnkb.moedelo.org", "https://oauth-rnkb.moedelo.org")]
        [TestCase("https://oauth-rnkb.moedelo.org", "https://oauth-rnkb.moedelo.org")]
        public void GetAuthServerUrl(string url, string expected)
        {
            var uri = new Uri(url);
            string result = serverUriService.GetAuthServerUrl(uri);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}