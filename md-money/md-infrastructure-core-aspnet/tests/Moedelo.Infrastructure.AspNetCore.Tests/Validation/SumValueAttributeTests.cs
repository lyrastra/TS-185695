using FluentAssertions;
using Moedelo.Infrastructure.AspNetCore.Validation;
using NUnit.Framework;

namespace Moedelo.Infrastructure.AspNetCore.Tests.Validation
{
    [TestFixture]

    public class SumValueAttributeTests
    {
        [Test]
        public void ShouldReturnFalseWhenInputIsZero()
        {
            var attribute = new SumValueAttribute();
            var actual = attribute.IsValid(0);
            actual.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnFalseWhenInputIsLessThanMin()
        {
            var attribute = new SumValueAttribute();
            var actual = attribute.IsValid(SumValueAttribute.MinValue - 1);
            actual.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnFalseWhenInputIsGreatThanMax()
        {
            var attribute = new SumValueAttribute();
            var actual = attribute.IsValid(SumValueAttribute.MaxValue + 1);
            actual.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnFalseWhenInputIsLessThanCustomMin()
        {
            var attribute = new SumValueAttribute(minValue: 10);
            var actual = attribute.IsValid(9);
            actual.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnFalseWhenInputIsGreatThanCustomMax()
        {
            var attribute = new SumValueAttribute(maxValue: 100);
            var actual = attribute.IsValid(101);
            actual.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrueWhenInputIsBetweenMinAndMax()
        {
            var attribute = new SumValueAttribute();
            var actual = attribute.IsValid(1000);
            actual.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnTrueWhenInputBetweenCustomMinAndMax()
        {
            var attribute = new SumValueAttribute(-100, 0);
            var actual = attribute.IsValid(-10);
            actual.Should().BeTrue();
        }
    }
}
