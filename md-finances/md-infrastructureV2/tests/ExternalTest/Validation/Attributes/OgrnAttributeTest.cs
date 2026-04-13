using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class OgrnAttributeTest
    {
        private OgrnAttribute attr;

        [SetUp]
        public void SetUp()
        {
            attr = new OgrnAttribute();
        }

        [Test]
        public void TestValidationPasses_ForNullValue()
        {
            attr.IsValid(null).Should().BeTrue();
        }

        [Test]
        public void TestValidationPasses_ForEmptyString()
        {
            attr.IsValid(string.Empty).Should().BeTrue();
        }

        [Test]
        [TestCase(1)]
        [TestCase("432344")]
        [TestCase("1234567890")]
        [TestCase("123456789011")]
        public void TestValidationFails_ForValuesWithInvalidLength(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("10377398772A5")]
        [TestCase("A04500116000157")]
        public void TestValidationFails_ForValuesWithNonDigitsInside(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("1037739877295")]
        [TestCase("1027700167110")]
        [TestCase("1117746358608")]
        public void TestValidationPasses_ForValidValuesWithLength13(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase("304500116000221")]
        [TestCase("304500116000157")]
        public void TestValidationPasses_ForValidValuesWithLength15(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase("1037739877294")]
        [TestCase("1027700167130")]
        public void TestValidationFails_ForInvalidValuesWithLength13(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("304500116000222")]
        [TestCase("304500116000158")]
        public void TestValidationFails_ForInvalidValuesWithLength15(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }
    }
}