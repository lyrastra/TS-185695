using FluentAssertions;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using NUnit.Framework;

namespace Moedelo.Infrastructure.AspNetCore.Xss.Tests
{
    [TestFixture]
    public class XssValidationAttributeTest
    {
        readonly ValidateXssAttribute attribute = new ValidateXssAttribute();

        [Test]
        public void ShouldPassWhenInputIsValidString()
        {
            const string input = "some valid string";

            var actual = attribute.IsValid(input);

            actual.Should().BeTrue();
        }

        [Test]
        public void ShouldFallWhenInputIsNotValidString()
        {
            const string input = "<script>alert('xss')</script>";

            var actual = attribute.IsValid(input);

            actual.Should().BeFalse();
        }
    }
}
