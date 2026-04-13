using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class InnAttributeTest
    {
        private InnAttribute attr;

        [SetUp]
        public void SetUp()
        {
            attr = new InnAttribute();
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
        [TestCase("dsfdf")]
        [TestCase("123456789")]
        [TestCase("12345678901")]
        public void TestValidationFails_ForValuesWithInvalidLength(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("77360500A3")]
        [TestCase("773PUTIN5568")]
        public void TestValidationFails_ForValuesWithNonDigitsInside(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("7708503727")]
        [TestCase("7736050003")]
        public void TestValidationPasses_ForValidValuesWithLength10(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase("773600605568")]
        [TestCase("594402065937")]
        public void TestValidationPasses_ForValidValuesWithLength12(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }

        [Test]
        [TestCase("7508503727")]
        [TestCase("7536050003")]
        public void TestValidationFails_ForInvalidValuesWithLength10(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase("773600305568")]
        [TestCase("594402265937")]
        public void TestValidationFails_ForInvalidValuesWithLength12(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }
    }
}