using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;

// ReSharper disable ExpressionIsAlwaysNull

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class LessThenAttributeTest
    {
        private readonly ValidationResult errorResult = new ValidationResult("error");

        [Test]
        [TestCase(59.5, 59.5)]
        [TestCase(66.25, 59.5)]
        public void IsValid_CheckedPropValGreatherOrEqualThenOtherValue_ReturnError(decimal current, decimal other)
        {
            decimal? checkedPropVal = current;
            decimal? otherPropVal = other;
            var expected = errorResult;

            var attr = new LessThenAttribute("Prop1") {ErrorMessage = errorResult.ErrorMessage};
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_CheckedPropValLessThanOtherValue_ReturnSuccess()
        {
            decimal? checkedPropVal = 16.25m;
            decimal? otherPropVal = 76.4m;
            var expected = ValidationResult.Success;

            var attr = new LessThenAttribute("Prop1") { ErrorMessage = errorResult.ErrorMessage };
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(89)]
        [TestCase(0)]
        [TestCase(-7500)]
        [TestCase(null)]
        public void IsValid_CheckedPropValIsNull_ReturnSuccess(decimal? otherVal)
        {
            decimal? checkedPropVal = null;
            decimal? otherPropVal = otherVal;
            var expected = ValidationResult.Success;

            var attr = new LessThenAttribute("Prop1") { ErrorMessage = errorResult.ErrorMessage };
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_CheckedPropValLessThanOtherPropValueAndOtherPropTypeIsInt_ReturnSuccess()
        {
            decimal? checkedPropVal = 3.55m;
            int? otherPropVal = 15;
            var expected = ValidationResult.Success;

            var attr = new LessThenAttribute("Prop1") { ErrorMessage = errorResult.ErrorMessage };
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(89)]
        [TestCase(0)]
        [TestCase(-7500)]
        public void IsValid_OtherPropValIsNullAndChechedPropHasValue_ReturnSuccess(decimal curValue)
        {
            decimal? checkedPropVal = curValue;
            decimal? otherPropVal = null;
            var expected = ValidationResult.Success;

            var attr = new LessThenAttribute("Prop1") { ErrorMessage = errorResult.ErrorMessage };
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}