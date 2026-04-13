using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class OkpoAttributeTest
    {
        private OkpoAttribute attr;

        [SetUp]
        public void SetUp()
        {
            attr = new OkpoAttribute();
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
        [TestCase("8")]
        [TestCase("28")]
        [TestCase("228")]
        public void TestValidationFails_ForTooShortValues(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("12345678901")]
        [TestCase("123456789012")]
        [TestCase("1234567890123")]
        public void TestValidationFails_ForTooLongValues(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("123456A8")]
        [TestCase("12F4567890")]
        public void TestValidationFails_ForValuesWithNonDigitsInside(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("00083262")]
        [TestCase("24399828")]
        [TestCase("33928881")]
        [TestCase("14163007")]
        [TestCase("18523617")]
        public void TestValidationPasses_ForValidValuesWithLength8(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase("00083262")]
        [TestCase("09807684")]
        public void TestValidationPasses_ForValidValuesWithoutLeadingZeroLength8(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase(83262)]
        [TestCase(9807684)]
        public void TestValidationPasses_ForValidIntegerValuesLength8(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase("0132191458")]
        [TestCase("0136105769")]
        [TestCase("0162220243")]
        public void TestValidationPasses_ForValidValuesWithLength10(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase("136492673")]
        [TestCase("148543122")]
        [TestCase("136105769")]
        [TestCase("132191458")]
        [TestCase("162220243")]
        public void TestValidationPasses_ForValidValuesWithoutLeadingZeroLength10(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase(136492673)]
        [TestCase(148543122)]
        [TestCase(136105769)]
        [TestCase(132191458)]
        [TestCase(162220243)]
        public void TestValidationPasses_ForValidIntegerValuesLength10(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase("00083269")]
        [TestCase("24399829")]
        [TestCase("33928889")]
        [TestCase("14163009")]
        [TestCase("18523619")]
        public void TestValidationFails_ForInvalidValuesWithLength8(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("0193273371")]
        [TestCase("0132191451")]
        [TestCase("0162220241")]
        public void TestValidationFails_ForInvalidValuesWithLength10(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }
    }
}