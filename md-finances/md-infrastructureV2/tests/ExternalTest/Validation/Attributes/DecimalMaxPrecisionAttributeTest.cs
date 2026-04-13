using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class DecimalMaxPrecisionAttributeTest
    {
        private const int precision = 4;
        private DecimalMaxPrecisionAttribute attr;

        [SetUp]
        public void SetUp()
        {
            attr = new DecimalMaxPrecisionAttribute(precision);
        }

        [Test]
        public void IsValid_ForNullValue_ReturnsTrue()
        {
            attr.IsValid(null).Should().BeTrue();
        }

        [Test]
        [TestCase(123.345665)]
        [TestCase(123.34566)]
        [TestCase("123.345665")]
        [TestCase("123.34566")]
        [TestCase("123,345665")]
        [TestCase("123,34566")]
        public void IsValid_ForTooPrecisionedValue_ReturnsFalse(object value)
        {
            attr.IsValid(value).Should().BeFalse();
        }

        [Test]
        [TestCase(123)]
        [TestCase(123.3)]
        [TestCase(123.32)]
        [TestCase(123.3343)]
        [TestCase("123")]
        [TestCase("123.1")]
        [TestCase("123.32")]
        [TestCase("123.323")]
        [TestCase("123.3456")]
        [TestCase("123,1")]
        [TestCase("123,32")]
        [TestCase("123,323")]
        [TestCase("123,3456")]
        public void IsValid_ForCorrectValue_ReturnsTrue(object value)
        {
            attr.IsValid(value).Should().BeTrue();
        }
    }
}