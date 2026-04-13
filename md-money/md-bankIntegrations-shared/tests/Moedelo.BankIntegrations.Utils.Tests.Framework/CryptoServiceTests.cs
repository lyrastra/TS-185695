using Moedelo.BankIntegrations.Utils.Framework.Services;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace Moedelo.BankIntegrations.Utils.Tests.Framework
{
    [TestFixture]
    public class CryptoServiceTests
    {
        private const string IntegrationCryptoDecryptWord = "IntegrationCryptoDecryptWord";
        private const string IntegrationCryptoPassword = "IntegrationCryptoPassword";

        private Mock<ISettingRepository> settingRepositoryMock;
        private CryptoService cryptoService;

        [SetUp]
        public void SetUp()
        {
            settingRepositoryMock = new Mock<ISettingRepository>();
            settingRepositoryMock.Setup(m => m.Get("IntegrationCryptoDecryptWord")).Returns(new SettingValue("IntegrationCryptoDecryptWord", v => IntegrationCryptoDecryptWord));
            settingRepositoryMock.Setup(m => m.Get("IntegrationCryptoPassword")).Returns(new SettingValue("IntegrationCryptoPassword", v => IntegrationCryptoPassword));
        }

        [TestCase("hello world! test encrypt text!", 92)]
        [TestCase("", 0)]
        [TestCase("   ", 3)]
        public void EncryptText_ShouldLengthBeEncryptText(string text, int encryptTextLength)
        {
            // ARRANGE
            cryptoService = new CryptoService(settingRepositoryMock.Object);

            // ACT
            var result = cryptoService.EncryptText(text);

            // ASSERT
            result.Length.Should().Be(encryptTextLength);
        }

        [TestCase("IntegrationCryptoDecryptWordqIY3r/QX4PAZTA56dw78sIGWdV+2AP5GOtZfxMAmU/SqspRFM5uNZVMhtZqtVdAk", "hello world! test encrypt text!")]
        [TestCase("qIY3r/QX4PAZTA56dw78sIGWdV+2AP5GOtZfxMAmU/SqspRFM5uNZVMhtZqtVdAk", "qIY3r/QX4PAZTA56dw78sIGWdV+2AP5GOtZfxMAmU/SqspRFM5uNZVMhtZqtVdAk")]
        [TestCase("hello world! test encrypt text!", "hello world! test encrypt text!")]
        [TestCase("", "")]
        [TestCase("   ", "   ")]
        public void DecryptText_ShouldDecryptText(string encryptText, string text)
        {
            // ARRANGE
            cryptoService = new CryptoService(settingRepositoryMock.Object);

            // ACT
            var result = cryptoService.DecryptText(encryptText);

            // ASSERT
            result.Should().BeEquivalentTo(text);
        }
    }
}
