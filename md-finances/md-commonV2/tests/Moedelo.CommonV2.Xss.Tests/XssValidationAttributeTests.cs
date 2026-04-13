using FluentAssertions;
using Moedelo.CommonV2.Xss.DataAnnotations;
using NUnit.Framework;

[TestFixture]
public class XssValidationAttributeTests
{
    readonly XssValidationAttribute attribute = new XssValidationAttribute();


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