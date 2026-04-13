using System;
using Moedelo.CommonV2.WhiteLabel.Services;
using NUnit.Framework;

namespace Moedelo.CommonV2.WhiteLabel.Tests.Services
{
    public class WlTests
    {
        [TestCase("https://oauth.moedelo.org", null)]
        [TestCase("https://oauth67.moedelo.org", null)]
        [TestCase("https://www.moedelo.org", null)]
        [TestCase("https://www68.moedelo.org", null)]
        [TestCase("https://stage.mdtest.org", null)]
        [TestCase("https://oauth-stage.mdtest.org", null)]
        [TestCase("https://sso-stage.mdtest.org", null)]
        [TestCase("https://oauth-box15.mdtest.org", null)]
        [TestCase("https://sso-box15.mdtest.org", null)]
        [TestCase("https://box15.mdtest.org", null)]
        [TestCase("http://localhost:4303", null)]

        [TestCase("http://wl-akbars.localhost:4303", "akbars")]
        [TestCase("http://wl-sber.localhost:4303", "sber")]
        [TestCase("http://wl-sbersolutions.localhost:4303", "sber")]
        [TestCase("http://wl-sber-box15.mdtest.org", "sber")]
        [TestCase("http://wl-delo-box15.mdtest.org", "delo")]
        [TestCase("http://wl-sber-stage.mdtest.org", "sber")]
        [TestCase("http://wl-sbersolutions-stage.mdtest.org", "sber")]
        [TestCase("http://wl-alpha-stage.mdtest.org", "alpha")]
        [TestCase("http://oauth-wl-sber-stage.mdtest.org", "sber")]
        [TestCase("http://sso-wl-sber-stage.mdtest.org", "sber")]
        [TestCase("http://oauth-wl-sbersolutions-stage.mdtest.org", "sber")]
        [TestCase("http://sso-wl-sbersolutions-stage.mdtest.org", "sber")]
        [TestCase("https://sberbank-mbo1.mdtest.org", "sber")]
        [TestCase("https://oauth-sberbank-mbo1.mdtest.org", "sber")]
        [TestCase("https://www.sberbank-mbo1.ru", "sber")]
        [TestCase("https://oauth.sberbank-mbo1.ru", "sber")]
        [TestCase("https://sso.sberbank-mbo1.ru", "sber")]
        [TestCase("https://mbo-sber-solutions.mdtest.org", "sber")]
        [TestCase("https://oauth-wl-sbersolutions-stage.mdtest.org", "sber")]
        [TestCase("https://www.mbo-sber-solutions.ru", "sber")]
        [TestCase("https://oauth.mbo-sber-solutions.ru", "sber")]
        [TestCase("https://sso.mbo-sber-solutions.ru", "sber")]
        [TestCase("https://delo.moedelo.org", "delo")]
        [TestCase("https://oauth-delo.moedelo.org", "delo")]
        [TestCase("https://sso-delo.moedelo.org", "delo")]

        [TestCase("http://wl-gis.localhost:4303", "gis")]
        [TestCase("http://gis.moedelo.org", "gis")]
        [TestCase("http://wl-gis-box18.mdtest.org", "gis")]
        [TestCase("http://wl-gis-stage.mdtest.org", "gis")]

        // WbBank: прод buh.wb-bank.ru + поддомены, box, stage, localhost
        [TestCase("https://buh.wb-bank.ru", "wbbank")]
        [TestCase("https://oauth-buh.wb-bank.ru", "wbbank")]
        [TestCase("https://sso-buh.wb-bank.ru", "wbbank")]
        [TestCase("https://restapi-buh.wb-bank.ru", "wbbank")]
        [TestCase("https://public-buh.wb-bank.ru", "wbbank")]
        [TestCase("https://stats-buh.wb-bank.ru", "wbbank")]
        [TestCase("http://wl-wbbank-box54.mdtest.org", "wbbank")]
        [TestCase("http://wl-wbbank-stage.mdtest.org", "wbbank")]
        [TestCase("http://wl-wbbank.localhost:4303", "wbbank")]
        public void GetNameByHost_ReturnsOldWl_IfIsNewWebpackServiceDisabled(string uri, string expected)
        {
            var whiteLabelService = new WhiteLabelService();

            var host = new Uri(uri).Host;
            string result = whiteLabelService.GetNameByHost(host);
            Assert.AreEqual(expected, result);
        }
    }
}