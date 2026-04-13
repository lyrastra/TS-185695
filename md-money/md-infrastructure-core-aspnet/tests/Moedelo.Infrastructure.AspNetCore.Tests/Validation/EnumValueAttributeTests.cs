using FluentAssertions;
using Moedelo.Infrastructure.AspNetCore.Validation;
using NUnit.Framework;

namespace Moedelo.Infrastructure.AspNetCore.Tests.Validation
{
    [TestFixture]

    public class EnumValueAttributeTests
    {
        [Test]
        public void ShouldReturnFalseWhenInputIsNullAndNotAllowNull()
        {
            var attribute = new EnumValueAttribute();
            var actual = attribute.IsValid((TestEnum?)null);
            actual.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnFalseWhenInputValueIsNotDefineInEnum()
        {
            var attribute = new EnumValueAttribute();
            var actual = attribute.IsValid((TestEnum)15);
            actual.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrueWhenInputValueIsDefineInEnum()
        {
            var attribute = new EnumValueAttribute();
            var actual = attribute.IsValid(TestEnum.Ok);
            actual.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnTrueWhenInputIsNullAndAllowNull()
        {
            var attribute = new EnumValueAttribute() { AllowNull = true };
            var actual = attribute.IsValid((TestEnum?)null);
            actual.Should().BeTrue();
        }
    }

    enum TestEnum { Ok };
}
