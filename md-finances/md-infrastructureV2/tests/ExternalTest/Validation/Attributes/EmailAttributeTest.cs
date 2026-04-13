using System.ComponentModel.DataAnnotations;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class EmailAttributeTest
    {
        private EmailAttribute attr;
        
        [SetUp]
        public void SetUp()
        {
            attr = new EmailAttribute();
        }
        
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("тест", false)]
        [TestCase("тест@gmail.com", false)]
        [TestCase("test", false)]
        [TestCase("test-@gmail.com", false)]
        [TestCase("test@gmail.com", true)]
        [TestCase("test123@gmail.com", true)]
        [TestCase("test_123@gmail.com", true)]
        public void TestEmailAttribute(string input, bool shouldBeValid)
        {
            var validationContext = new ValidationContext(new EmailTestType {Email = input});
            var validationResult = attr.GetValidationResult(input, validationContext);
            Assert.AreEqual(shouldBeValid, validationResult is null);
        }
        private class EmailTestType
        {
            [Email]
            public string Email { get; set; }
        }
    }
}